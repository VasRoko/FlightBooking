using FlightBooking.Application.Common.Interfaces;
using FlightBooking.Application.Flights.Queries;
using FlightBooking.Application.ScheduledFlight.Queries;
using FlightBooking.Domain.Entities;
using FlightBooking.Domain.Entities.Enums;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FlightBooking.Application.Tests.FlightsTests
{
    public class GetSummaryQueryHandlerTests
    {
        [Fact]
        public async Task CreateAirlineCommandHandler_Should_ChatchThrownAnyExeception()
        {

            var repositoryMock = new Mock<IFlightRepository>();
            var bussinesMock = new Mock<IBussinesRuleEvaluator>();

            repositoryMock.Setup(x => x.GetScheduledFlightAsync(It.IsAny<int>())).ThrowsAsync(new Exception("Test"));
            var sut = new GetSummaryQueryHandler(repositoryMock.Object, bussinesMock.Object);

            // Act
            await Assert.ThrowsAsync<Exception>(() => sut.Handle(new GetSummaryQuery(), CancellationToken.None));
        }

        [Fact]
        public async Task CreateAirlineCommandHandler_Should_Handle_FLIGHTMAYNOTPROCEED_GivenValidRequest()
        {
            // Arrange
            var repositoryMock = new Mock<IFlightRepository>();
            var bussinesMock = new Mock<IBussinesRuleEvaluator>();

            repositoryMock.Setup(x => x.GetScheduledFlightAsync(It.IsAny<int>())).ReturnsAsync(new Flight()
            {
                Id = 1,
                Passengers = new List<Passenger>()
                {
                    new Passenger()
                    {
                        Type = PassengerType.GENERAL,
                        Name = "Test",
                        Age = 23,
                    }
                },
                FlightRoute = new FlightRoute("London", "Paris")
                {
                    BaseCost = 50,
                    BasePrice = 100,
                    LoyaltyPointsGained = 5,
                    MinimumTakeOffPercentage = 0.7
                },
                Plane = new Plane
                {
                    Id = 123,
                    Name = "Antonov AN-2",
                    NumberOfSeats = 12
                },
            });
            repositoryMock.Setup(x => x.GetScheduledFlightsAsync()).ReturnsAsync(new List<Flight>()
                {
                    new Flight()
                    {
                        Id = 2,
                        Passengers = new List<Passenger>(),
                        FlightRoute = new FlightRoute("test", "test"),
                        Plane = new Plane {
                            Id = 124,
                            Name = "Bombardier Q400",
                            NumberOfSeats = 12
                        }
                    },
                    new Flight()
                    {
                        Id = 3,
                        Passengers = new List<Passenger>(),
                        FlightRoute = new FlightRoute("test", "test"),
                        Plane = new Plane {
                            Id = 125,
                            Name = "ATR 640",
                            NumberOfSeats = 12
                        }
                    }
                });

            var sut = new GetSummaryQueryHandler(repositoryMock.Object, bussinesMock.Object);

            // Act
            var result = await sut.Handle(new GetSummaryQuery(), CancellationToken.None);

            // Assert
            Assert.Contains("General sales: 1", result);
            Assert.Contains("Discounted sales: 0", result);
            Assert.Contains("Loyalty member sales: 0", result);
            Assert.Contains("Airline employee comps: 0", result);
            Assert.Contains("FLIGHT MAY NOT PROCEED", result);
        }


        [Fact]
        public async Task CreateAirlineCommandHandler_Should_Handle_FLIGHTMAYPROCEED_GivenValidRequest()
        {
            // Arrange
            var repositoryMock = new Mock<IFlightRepository>();
            var bussinesMock = new Mock<IBussinesRuleEvaluator>();


            repositoryMock.Setup(x => x.GetScheduledFlightAsync(It.IsAny<int>())).ReturnsAsync(new Flight()
            {
                Id = 1,
                Passengers = new List<Passenger>()
                {
                    new Passenger()
                    {
                        Type = PassengerType.GENERAL,
                        Name = "Test1",
                        Age = 12,
                    },
                    new Passenger()
                    {
                        Type = PassengerType.DISCOUNTED,
                        Name = "Test2",
                        Age = 13,
                    },
                    new Passenger()
                    {
                        Type = PassengerType.AIRLINEEMPLOYEE,
                        Name = "Test3",
                        Age = 15,
                    },
                    new Passenger()
                    {
                        Type = PassengerType.LOYALTYMEMBER,
                        Name = "Test4",
                        Age = 16,
                    }
                },
                FlightRoute = new FlightRoute("London", "Paris")
                {
                    BaseCost = 50,
                    BasePrice = 100,
                    LoyaltyPointsGained = 5,
                    MinimumTakeOffPercentage = 0
                },
                Plane = new Plane
                {
                    Id = 123,
                    Name = "Antonov AN-2",
                    NumberOfSeats = 4
                },
            });
            bussinesMock.Setup(x => x.Evaluate(It.IsAny<FlightInfo>(), It.IsAny<List<IBussinesRule>>())).Returns(true);
            var sut = new GetSummaryQueryHandler(repositoryMock.Object, bussinesMock.Object);

            // Act
            var result = await sut.Handle(new GetSummaryQuery(), CancellationToken.None);

            // Assert
            Assert.Contains("General sales: 1", result);
            Assert.Contains("Discounted sales: 1", result);
            Assert.Contains("Loyalty member sales: 1", result);
            Assert.Contains("Airline employee comps: 1", result);
            Assert.Contains("THIS FLIGHT MAY PROCEED", result);
        }
    }
}
