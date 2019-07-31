using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GlobalizationLocalization
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //lokalizasiya servisini qoşuruq.
            // lokalizasiya üçün tərcümə fayllarının Resources qovluğunda
            //yerləşdirini göstəririk
            services.AddLocalization(x => x.ResourcesPath = "Resources");


            services.Configure<RequestLocalizationOptions>(op =>
            {
                //dəstəklənəcək dillərin siyahısı hazırlayırıq
                var supportedLanguages = new CultureInfo[]
                {
                    new CultureInfo("az-Latn-AZ"),
                    new CultureInfo("ru-RU")
                };

                //hec bir dil göstərilməzsə default olaraq Azərbaycan dilini götür
                op.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(supportedLanguages[0]);

                //vaxt və tarix, pul formatı , rəqəm təsviri üçün
                op.SupportedCultures = supportedLanguages;

                //səhifədəki sözlərin tərcümə olunması üçün
                op.SupportedUICultures = supportedLanguages;



            });


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc()
                .AddViewLocalization(x=>x.ResourcesPath="Resources")
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            //lokalizasiya middleware-sinin əlavəsi
            app.UseRequestLocalization();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
