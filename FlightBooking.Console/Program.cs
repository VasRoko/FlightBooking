using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace FlightBooking.Console
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var services = Startup.ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();
            await serviceProvider.GetService<Main>().Run();
        }
    }
}
