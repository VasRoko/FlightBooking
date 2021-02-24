using FlightBooking.Application.Common.Interfaces;
using FlightBooking.Domain.Entities;
using FlightBooking.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FlightBooking.Infrastructure.UnitTests
{
    public class DependencyConfigTest
    {
        [Fact]
        public void InfrastructureLayer_Services_ShouldNotBeNull()
        {
            // Act
            var service = new ServiceCollection();
            service.AddInfrastructureLayer();
            var serviceProvider = service.BuildServiceProvider();

            var flightRepository = serviceProvider.GetService<IFlightRepository>();
            var fileManager = serviceProvider.GetService<IFileManager<Flight>>();
            var streamer = serviceProvider.GetService<IStreamer>();

            // Assert
            Assert.NotNull(flightRepository);
            Assert.NotNull(fileManager);
            Assert.NotNull(streamer);
        }
    }
}
