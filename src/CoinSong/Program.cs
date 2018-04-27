using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace CoinSong.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(c => c.AddJsonFile("appsettings.generated.json", true, true))
                .UseStartup<Startup>()
                .Build();
    }
}