using FlightBooking.Application.Common.Interfaces;
using FlightBooking.Application.Flights.Commands;
using FlightBooking.Domain.Entities;
using MediatR;
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
    public class CreateFlightCommandHandlerTests
    {
        [Fact]
        public async Task CreateFlightCommandHandler_Should_ChatchThrownAnyExeception()
        {

            var repositoryMock = new Mock<IFlightRepository>();
            repositoryMock.Setup(x => x.ScheduleFlightsAsync(It.IsAny<List<Flight>>())).ThrowsAsync(new Exception("Test"));
            var sut = new CreateFlightCommandHandler(repositoryMock.Object);

            // Act
            await Assert.ThrowsAsync<Exception>(() => sut.Handle(new CreateFlightCommand(), CancellationToken.None));
        }

        [Fact]
        public async Task Test1()
        {
            // Arrange
            var flights = new List<Flight>()
            {
                new Flight()
                {
                    Id = 1,
                    Passengers = new List<Passenger>(),
                    FlightRoute = new FlightRoute("London", "Paris"),
                    Plane = new Plane(),
                }
            };

            var repositoryMock = new Mock<IFlightRepository>();

            List<Flight> matchObj = new List<Flight>();
            repositoryMock.Setup(x => x.ScheduleFlightsAsync(It.IsAny<List<Flight>>())).Callback<List<Flight>>((obj) => matchObj.AddRange(obj));
            var sut = new CreateFlightCommandHandler(repositoryMock.Object);

            // Act
            var request = new CreateFlightCommand() {};
            var result = await sut.Handle(request, CancellationToken.None);

            // Assert
            repositoryMock.Verify(x => x.ScheduleFlightsAsync(It.IsAny<List<Flight>>()), Times.Once());
            Assert.Equal(flights.First().Id, matchObj.First().Id);
            Assert.Empty(matchObj.First().Passengers);
            Assert.Equal(flights.First().FlightRoute.Origin, matchObj.First().FlightRoute.Origin);
            Assert.Equal(flights.First().FlightRoute.Destination, matchObj.First().FlightRoute.Destination);
            Assert.IsType<Plane>(matchObj.First().Plane);

            Assert.Equal(Unit.Value, result);
        }
    }
}
