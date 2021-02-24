using FlightBooking.Console.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FlightBooking.Console.UnitTests
{
    public class StartupTests
    {
        [Fact]
        public void Should_RegisterValid_ICommandHandler()
        {
            // Arrange
            var services = Startup.ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();
            
            // Act
            var commandHandlerService = serviceProvider.GetService<ICommandHandler>();

            // Assert
            Assert.NotNull(commandHandlerService);
        }
    }
}
