using FlightBooking.Application.Common.Interfaces;
using FlightBooking.Domain.Entities;
using FlightBooking.Infrastructure.Interfaces;
using FlightBooking.Infrastructure.Persistance;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FlightBooking.Infrastructure.UnitTests
{
    public class FlightRepositoryTests
    {
        [Fact]
        public async Task Verify_CreateFlightsAsync_Invoked()
        {
            // Arrange
            var fileManagerMock = new Mock<IFileManager<Flight>>();
            fileManagerMock.Setup(x => x.WriteToJsonAsync(It.IsAny<string>(), It.IsAny<List<Flight>>())).Verifiable();

            var sut = new FlightRepository(fileManagerMock.Object);
            // Act
            await sut.CreateFlightsAsync(new List<Flight>());
            
            // Assert
            Mock.Verify();
        }

        [Fact]
        public async Task Should_Invoke_UpdateFlightAsync()
        {
            // Arrange
           var fileManagerMock = new Mock<IFileManager<Flight>>();
            fileManagerMock.Setup(x => x.ReadFromJsonAsync(It.IsAny<string>())).ReturnsAsync(new List<Flight>() { 
                new Flight() { Id = 1}
            });
            var flightToUpdate = new Flight()
            {
                Id = 1,
                Passengers = new List<Passenger>(),
                FlightRoute = new FlightRoute("London", "Parris")
            };

            fileManagerMock.Setup(x => x.WriteToJsonAsync(It.IsAny<string>(), It.IsAny<List<Flight>>())).Verifiable();

            var verifiableObjs = new List<Flight>();
            string verifiablePath = "";
            fileManagerMock.Setup(x => x.WriteToJsonAsync(It.IsAny<string>(), It.IsAny<List<Flight>>()))
                .Callback<string, List<Flight>>((str, obj) => { 
                    verifiablePath = str; 
                    verifiableObjs.AddRange(obj); 
                });

            var sut = new FlightRepository(fileManagerMock.Object);
            // Act
            await sut.UpdateFlightAsync(flightToUpdate);

            // Assert
            Mock.Verify();
            Assert.NotEqual(string.Empty, verifiablePath);
            Assert.NotEmpty(verifiableObjs);
            Assert.Equal(flightToUpdate.Id, verifiableObjs.First().Id);

        }

        [Fact]
        public async Task Should_GetScheduledFlight()
        {
            // Arrange
            var fileManagerMock = new Mock<IFileManager<Flight>>();
            var flight = new Flight()
            {
                Id = 1,
                Passengers = new List<Passenger>(),
                FlightRoute = new FlightRoute("test", "test"),
                Plane = new Plane(),
            };
            var flightList = new List<Flight>();
            flightList.Add(flight);
            fileManagerMock.Setup(x => x.ReadFromJsonAsync(It.IsAny<string>())).ReturnsAsync(flightList);
            var sut = new FlightRepository(fileManagerMock.Object);

            // Act
            var result =  await sut.GetScheduledFlightAsync(1);

            // Assert
            Assert.Equal(flight.Id, result.Id);
            Assert.Empty(result.Passengers);
            Assert.IsType<Plane>(result.Plane);
            Assert.Equal(flight.FlightRoute.Origin, result.FlightRoute.Origin);
            Assert.Equal(flight.FlightRoute.Destination, result.FlightRoute.Destination);
        }

        [Fact]
        public async Task Should_GetScheduledFlightList()
        {
            // Arrange
            var fileManagerMock = new Mock<IFileManager<Flight>>();
            var flight = new Flight()
            {
                Id = 1,
                Passengers = new List<Passenger>(),
                FlightRoute = new FlightRoute("test", "test"),
                Plane = new Plane(),
            };
            var flightList = new List<Flight>();
            flightList.Add(flight);
            fileManagerMock.Setup(x => x.ReadFromJsonAsync(It.IsAny<string>())).ReturnsAsync(flightList);

            var sut = new FlightRepository(fileManagerMock.Object);

            // Act
            var result = await sut.GetScheduledFlightsAsync();

            // Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Should_ScheduleFlights_GivenFileManagerIsNotNull()
        {
            // Arrange
            var storeList = new List<Flight>() {
            new Flight()
            {
                Id = 1,
                Passengers = new List<Passenger>(),
                FlightRoute = new FlightRoute("test", "test"),
                Plane = new Plane(),
            }};
            var flightList = new List<Flight>() {
            new Flight()
            {
                Id = 2,
                Passengers = new List<Passenger>(),
                FlightRoute = new FlightRoute("test", "test"),
                Plane = new Plane(),
            }};


            var fileManagerMock = new Mock<IFileManager<Flight>>();
            fileManagerMock.Setup(x => x.ReadFromJsonAsync(It.IsAny<string>())).Verifiable();
            var sut = new FlightRepository(fileManagerMock.Object);

            // Act
            await sut.ScheduleFlightsAsync(flightList);
            fileManagerMock.Verify();
        }

        [Fact]
        public async Task Should_ScheduleFlights_GivenFileManagerIsNull()
        {
            // Arrange
            var flightList = new List<Flight>() {
            new Flight()
            {
                Id = 2,
                Passengers = new List<Passenger>(),
                FlightRoute = new FlightRoute("test", "test"),
                Plane = new Plane(),
            }};

            var fileManagerMock = new Mock<IFileManager<Flight>>();
            fileManagerMock.Setup(x => x.ReadFromJsonAsync(It.IsAny<string>())).ReturnsAsync(new List<Flight>());

            var matchObj = new List<Flight>();
            fileManagerMock.Setup(x => x.WriteToJsonAsync(It.IsAny<string>(), It.IsAny<List<Flight>>())).Callback<string, List<Flight>>((str, obj) => matchObj.AddRange(obj));

            var sut = new FlightRepository(fileManagerMock.Object);

            // Act
            await sut.ScheduleFlightsAsync(flightList);

            Assert.NotEmpty(matchObj);
            Assert.Equal(flightList[0].Id, matchObj[0].Id);
        }
    }
}
