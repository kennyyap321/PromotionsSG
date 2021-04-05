using Common.DBTableModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PromotionsSG.API.PromotionAPI.Repository;


namespace PromotionsSG.API.Promotion
{
    public class Startup
    {
        private IWebHostEnvironment _webHostEnvironment;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("PromotionsSGdb");
            if (!string.IsNullOrEmpty(connectionString))
            {
                services.AddDbContext<MyDBContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });
                services.AddControllersWithViews();
                services.AddTransient<IPromotionRepository, PromotionRepository>();
            }
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Promotion}/{action=Health}");
            });
        }
    }
}
