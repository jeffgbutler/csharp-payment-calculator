using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace csharp_payment_calculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var v1 = CreateHostBuilder(args);
            var v2 = v1.Build();
            v2.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
