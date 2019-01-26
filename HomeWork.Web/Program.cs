using ElectronNET.API;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace HomeWork.Web
{
    public static class Program
    {
        public static void Main(string[] args) => CreateWebHostBuilder(args).Build().Run();

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseElectron(args).UseStartup<Startup>();
    }
}