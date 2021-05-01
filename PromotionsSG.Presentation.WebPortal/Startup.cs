using Common.AppSettings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PromotionsSG.Presentation.WebPortal.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionsSG.Presentation.WebPortal
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
            services.Configure<APIUrls>(Configuration.GetSection("APIUrls"));
            services.AddControllersWithViews();
            services.AddSession();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpClient<ILoginService, LoginService>();
            services.AddHttpClient<ICustomerProfileService, CustomerProfileService>();
            services.AddHttpClient<IShopProfileService, ShopProfileService>();
            services.AddHttpClient<IPromotionService, PromotionService>();
            services.AddHttpClient<IClaimService, ClaimService>();
            services.AddHttpClient<IFeedbackService, FeedbackService>();
            services.AddHttpClient<IRecommendationService, RecommendationService>();
            services.AddHttpClient<INotificationService, NotificationService>();
            services.AddMvc().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSession();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=CustomerProfileView}/{action=Index}/{id?}");
            });
        }
    }
}
