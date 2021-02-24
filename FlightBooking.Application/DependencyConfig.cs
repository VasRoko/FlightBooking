using MediatR;
using System.Reflection;
using FlightBooking.Application.Common;
using Microsoft.Extensions.DependencyInjection;
using FlightBooking.Application.Common.Interfaces;

namespace FlightBooking.Application
{
    public static class DependencyConfig
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IBussinesRuleEvaluator, BussinesRuleEvaluator>();
            return services;
        }
    }
}
