using FlightBooking.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FlightBooking.Application.Tests
{
    public class DependencyConfigTest
    {
        [Fact]
        public void AddApplicationLayer_Services_ShouldNotBeNull()
        {
            // Act
            var service = new ServiceCollection();
            service.AddApplicationLayer();
            var serviceProvider = service.BuildServiceProvider();

            var mediatrService = serviceProvider.GetService<IMediator>();
            var bussinesRuleEvaluator = serviceProvider.GetService<IBussinesRuleEvaluator>();

            // Assert
            Assert.NotNull(mediatrService);
            Assert.NotNull(bussinesRuleEvaluator);
        }
    }
}
