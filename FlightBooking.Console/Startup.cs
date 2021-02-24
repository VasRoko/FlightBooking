using FlightBooking.Application;
using FlightBooking.Console.Interfaces;
using FlightBooking.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FlightBooking.Console
{
    public static class Startup
    {
        public static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            // required to run the application
            services.AddTransient<Main>();

            // Connect Application layer
            services.AddApplicationLayer();
            services.AddInfrastructureLayer();
            services.AddScoped<ICommandHandler, CommandHandler>();
            return services;
        }
    }
}
