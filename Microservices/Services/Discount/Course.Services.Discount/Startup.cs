using Course.Services.Discount.Services.Abstract;
using Course.Services.Discount.Services.Concrede;
using Course.Shared.Services.Abstract;
using Course.Shared.Services.Concrede;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
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

namespace Course.Services.Discount
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

            #region DI  services
            //DI SharedIdentityService
            services.AddScoped<ISharedIdentityService, SharedIdentityService>();
            //DI Services
            services.AddScoped<IDiscountService, DiscountService>();

            //for use httpcontextaccessor in Course.Shared project -SharedIdentityService 
            //services.AddHttpContextAccessor();

            #endregion

            #region JWT

            //Take token with user claims 
            var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

            //we said don't map sub == identifier
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
            //Json Web Token 
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.Authority = Configuration["IdentityServerUrl"];
                opt.Audience = "resource_discount";
                opt.RequireHttpsMetadata = false; //if we run the project with http we should do this

            });

            //for use httpcontextaccessor in Course.Shared project -SharedIdentityService 
            services.AddHttpContextAccessor();

            //filter for JWT
            services.AddControllers(opt =>
            {
                opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
            });

            #endregion           
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Course.Services.Discount", Version = "v1" });
            });
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Course.Services.Discount v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            //for jwt
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
