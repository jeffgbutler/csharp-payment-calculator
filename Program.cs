using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace csharp_payment_calculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    var port = System.Environment.GetEnvironmentVariable("PORT");
                    if (port != null) {
                        var url = string.Concat("http://0.0.0.0:", port);
                        webBuilder.UseUrls(url);
                    }
                });
    }
}
