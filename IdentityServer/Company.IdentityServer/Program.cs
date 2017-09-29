using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Company.IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseUrls("http://*:5010")
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .UseIISIntegration()
                .UseKestrel()
                .Build();

            host.Run();
        }
    }
}