using FlightBooking.Application.Common.Interfaces;
using FlightBooking.Infrastructure.Interfaces;
using FlightBooking.Infrastructure.Persistance;
using Microsoft.Extensions.DependencyInjection;

namespace FlightBooking.Infrastructure
{
    public static class DependencyConfig
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddScoped<IFlightRepository, FlightRepository>();
            services.AddScoped(typeof(IFileManager<>), typeof(FileManager<>));
            services.AddScoped<IStreamer, Streamer>();
            return services;
        }
    }
}
