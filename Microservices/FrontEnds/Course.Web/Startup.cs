using Course.Web.ClientsInfo;
using Course.Web.Handlers;
using Course.Web.Services.Abstract;
using Course.Web.Services.Concrede;
using Course.Web.Settings;
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
            #region DI
            services.AddScoped<ResourceOwnerPasswordTokenHandler>();
            #endregion
            #region Jwt

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                opt =>
                {
                    opt.LoginPath = "/Auth/SignIn";
                    opt.ExpireTimeSpan = TimeSpan.FromDays(60);
                    opt.SlidingExpiration = true;
                    opt.Cookie.Name = "webcookie";
                });

            #endregion
            #region Idendtity Service Configrutation
            services.AddHttpContextAccessor();
            services.AddHttpClient<IIdentityService, IdentityService>();

            var serviceApiSettings = Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

            services.AddHttpClient<IUserService, UserService>(opt =>
            {
                opt.BaseAddress = new Uri(serviceApiSettings.IdentityBaseUri);
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
            #endregion



            services.AddControllersWithViews();

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

            app.UseAuthorization();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
