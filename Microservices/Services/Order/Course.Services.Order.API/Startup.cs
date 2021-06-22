using Course.Services.Order.Application.Consumers;
using Course.Services.Order.Infrastructure;
using Course.Shared.Services.Abstract;
using Course.Shared.Services.Concrede;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Order.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {

            //Sql Connection
            //  configure.MigrationsAssembly("Course.Services.Order.Infrastructure") meaning: Create Migrations Folder in Infrastructure Layer
            services.AddDbContext<OrderDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),configure=>
               {
                   configure.MigrationsAssembly("Course.Services.Order.Infrastructure");
               }));

            #region JWT
            //for use httpcontextaccessor in Course.Shared project -SharedIdentityService 
            services.AddHttpContextAccessor();

            //Take token with user claims 
            var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

            //we said don't map sub == identifier
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
            //Json Web Token 
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.Authority = Configuration["IdentityServerUrl"];
                opt.Audience = "resource_order";
                opt.RequireHttpsMetadata = false; //if we run the project with http we should do this

            });

            //filter for JWT
            services.AddControllers(opt =>
            {
                opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
            });

            #endregion



            //We used ISharedIdentityService for get JWT User Token.
            //Shared Layer DI implement . We added AddHttpContextAccessor() because SharedIdentityService use this.
            services.AddScoped<ISharedIdentityService, SharedIdentityService>();
            services.AddHttpContextAccessor();

            //masstransit rabbitmq messagebroker configuration. Default port 5672
            services.AddMassTransit(x =>
            {
                x.AddConsumer<CreateOrderMessageCommandConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(Configuration["RabbitMQUrl"], "/", host =>
                    {
                        //host configuration
                        host.Username("guest");
                        host.Password("guest");
                    });
                    cfg.ReceiveEndpoint("create-order-service", e =>
                    {
                        e.ConfigureConsumer<CreateOrderMessageCommandConsumer>(context);
                    });
                });

             
            });
            services.AddMassTransitHostedService();


            //Mediatr Implementation
            services.AddMediatR(typeof(Application.Handlers.CreateOrderCommandHandler).Assembly);

           
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Course.Services.Order.API", Version = "v1" });
            });
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Course.Services.Order.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
