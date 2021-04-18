using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PromotionsSG.API.Login
{
    public class Program
    {
        //public static readonly string Namespace = typeof(Program).Namespace;
        //public static readonly string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
        .ConfigureLogging(logging =>
        {
            // When you need logging below set the minimum level. Otherwise the logging framework will default to Informational for external providers.
            logging.SetMinimumLevel(LogLevel.Debug);
        })
        .UseStartup<Startup>();

    }
}
