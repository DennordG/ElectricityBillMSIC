using System.Threading;
using System.Threading.Tasks;
using ElectricityBillMSIC.Application;
using ElectricityBillMSIC.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ElectricityBillMSIC
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var cts = new CancellationTokenSource();

            await host.Services.GetRequiredService<ConsoleService>().RunAsync(cts.Token);
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                       .ConfigureServices(services =>
                       {
                           services.AddSingleton<ConsoleService>().AddCore();
                       });
        }
    }
}
