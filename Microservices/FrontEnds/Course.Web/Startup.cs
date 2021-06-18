using Course.Shared.Services.Abstract;
using Course.Shared.Services.Concrede;
using Course.Web.ClientsInfo;
using Course.Web.Extension;
using Course.Web.Handlers;
using Course.Web.Helpers;
using Course.Web.Services.Abstract;
using Course.Web.Services.Concrede;
using Course.Web.Settings;
using Course.Web.Validators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Web
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
                   
            #region appsettings.json Configuration

            //Match ClientSettings class with ClientSettings in appsettings.json
            services.Configure<ClientSettings>(Configuration.GetSection("ClientSettings"));

            //Match ServiceApiSettings class with ServiceApiSettings in appsettings.json
            services.Configure<ServiceApiSettings>(Configuration.GetSection("ServiceApiSettings"));
            #endregion
            #region dependency injection
            services.AddScoped<ResourceOwnerPasswordTokenHandler>();
            services.AddScoped<ClientCredentialTokenHandler>();
            services.AddScoped<ISharedIdentityService, SharedIdentityService>();
            services.AddSingleton<PhotoHelper>();
            #endregion
          

            //Cookie oriented authentication
            
            services.AddHttpContextAccessor();       
            
            //for a client credential ClientAccessTokenCache class
            services.AddAccessTokenManagement();

            //in Extension Class
            services.AddHttpClientServices(Configuration);
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opts =>
            {
                opts.LoginPath = "/Auth/SignIn";
                opts.ExpireTimeSpan = TimeSpan.FromDays(60);
                opts.SlidingExpiration = true;
                opts.Cookie.Name = "webcookie";
            });

            //fluent validation
            services.AddControllersWithViews().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CourseCreateModelValidator>());

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
