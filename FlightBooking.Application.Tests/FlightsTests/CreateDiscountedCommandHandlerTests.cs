using FlightBooking.Application.Common.Interfaces;
using FlightBooking.Application.Flights.Commands.CreatePassengers;
using FlightBooking.Domain.Entities;
using FlightBooking.Domain.Entities.Enums;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FlightBooking.Application.Tests.FlightsTests
{
    public class CreateDiscountedCommandHandlerTests
    {
        [Fact]
        public async Task CreateDiscounteddHandler_Should_ChatchThrownAnyExeception()
        {

            var repositoryMock = new Mock<IFlightRepository>();
            repositoryMock.Setup(x => x.GetScheduledFlightAsync(It.IsAny<int>())).ThrowsAsync(new Exception("Test"));
            var sut = new CreateDiscountedCommandHandler(repositoryMock.Object);

            // Act
            await Assert.ThrowsAsync<Exception>(() => sut.Handle(new CreateDiscountedCommand(), CancellationToken.None));
        }

        [Fact]
        public async Task CreateDiscountedCommandHandler_Should_HandleGivenValidRequest()
        {
            var flight = new Flight()
            {
                Id = 1,
                Passengers = new List<Passenger>(),
                FlightRoute = new FlightRoute("London", "Paris"),
                Plane = new Plane(),
            };

            var repositoryMock = new Mock<IFlightRepository>();
            repositoryMock.Setup(x => x.GetScheduledFlightAsync(It.IsAny<int>())).ReturnsAsync(flight);

            Flight matchObj = new Flight();
            repositoryMock.Setup(x => x.UpdateFlightAsync(It.IsAny<Flight>())).Callback<Flight>((obj) => matchObj = obj);
            var sut = new CreateDiscountedCommandHandler(repositoryMock.Object);

            var request = new CreateDiscountedCommand() { Name = "Test", Age = 1 };
            var result = await sut.Handle(request, CancellationToken.None);

            // Act
            repositoryMock.Verify(x => x.UpdateFlightAsync(It.IsAny<Flight>()), Times.Once());

            Assert.Equal(matchObj.Passengers.First().Name, request.Name);
            Assert.Equal(matchObj.Passengers.First().Age, request.Age);
            Assert.Equal(matchObj.Passengers.First().Type.ToString(), PassengerType.DISCOUNTED.ToString());
            Assert.Contains("1", result);
            Assert.Contains(PassengerType.DISCOUNTED.ToString(), result);
        }
    }
}
