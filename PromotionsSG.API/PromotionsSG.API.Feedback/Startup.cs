using Common.DBTableModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PromotionsSG.API.Feedback.Repository;
using PromotionsSG.API.Feedback.RepositoryInterface;

namespace PromotionsSG.API.Feedback
{
    public class Startup
    {
        private IWebHostEnvironment _webHostEnvironment;

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("PromotionsSGdb");
            if (!string.IsNullOrEmpty(connectionString))
            {
                services.AddDbContext<MyDBContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });
            }
            services.AddControllersWithViews();
            services.AddTransient<IFeedbackRepository, FeedbackRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            //app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    //pattern: "{controller=Home}/{action=Index}/{id?}");
                    pattern: "{controller=Feedback}/{action=Health}");
            });
        }
    }
}
