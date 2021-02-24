using Moq;
using Xunit;
using System.Threading.Tasks;
using FlightBooking.Application.Flights.Commands.CreatePassengers;
using FlightBooking.Application.Common.Interfaces;
using System.Threading;
using System;
using FlightBooking.Domain.Entities;
using FlightBooking.Domain.Entities.Enums;
using System.Collections.Generic;
using System.Linq;

namespace FlightBooking.Application.Tests.FlightsTests
{
    public class CreateAirlineCommandHandlerTests
    {
        [Fact]
        public async Task CreateAirlineCommandHandler_Should_ChatchThrownAnyExeception()
        {

            var repositoryMock = new Mock<IFlightRepository>();
            repositoryMock.Setup(x => x.GetScheduledFlightAsync(It.IsAny<int>())).ThrowsAsync(new Exception("Test"));
            var sut = new CreateAirlineCommandHandler(repositoryMock.Object);

            // Act
            await Assert.ThrowsAsync<Exception>(() => sut.Handle(new CreateAirlineCommand(), CancellationToken.None));
        }

        [Fact]
        public async Task CreateAirlineCommandHandler_Should_HandleGivenValidRequest()
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
            var sut = new CreateAirlineCommandHandler(repositoryMock.Object);

            var request = new CreateAirlineCommand() { Name = "Test", Age = 1 };
            var result = await sut.Handle(request, CancellationToken.None);

            // Act
            repositoryMock.Verify(x => x.UpdateFlightAsync(It.IsAny<Flight>()), Times.Once());

            Assert.Equal(matchObj.Passengers.First().Name, request.Name);
            Assert.Equal(matchObj.Passengers.First().Age, request.Age);
            Assert.Equal(matchObj.Passengers.First().Type.ToString(), PassengerType.AIRLINEEMPLOYEE.ToString());
            Assert.Contains(PassengerType.AIRLINEEMPLOYEE.ToString(), result);
            Assert.Contains("1", result);
        }
    }
}
