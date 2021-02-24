using Moq;
using Xunit;
using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using FlightBooking.Domain.Entities;
using FlightBooking.Infrastructure.Interfaces;

namespace FlightBooking.Infrastructure.UnitTests
{
    public class FileManagerTests
    {
        [Fact]
        public async Task StreamReader_Should_GetValidFlightListFromStream()
        {
            // Arrange
            var mockStreamer = new Mock<IStreamer>();

            var flightRoute = new FlightRoute("London", "Paris")
            {
                BaseCost = 50,
                BasePrice = 100,
                LoyaltyPointsGained = 5,
                MinimumTakeOffPercentage = 0.7
            };
            var plane = new Plane
            {
                Id = 123,
                Name = "Antonov AN-2",
                NumberOfSeats = 12
            };
            var flights = new List<Flight>()
            {
                new Flight()
                {
                    Id = 1,
                    Passengers = new List<Passenger>(),
                    FlightRoute = flightRoute,
                    Plane = plane,
                }
            };

            var fakeMemoryStream = new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(flights)));
            mockStreamer.Setup(s => s.StreamReader(It.IsAny<string>())).Returns(() => new StreamReader(fakeMemoryStream));

            var sut = new FileManager<Flight>(mockStreamer.Object);

            // Acts
            var result = await sut.ReadFromJsonAsync("test.txt");
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task StreamReader_Should_Catch_Throw_Exception()
        {
            // Arrange
            var mockStreamer = new Mock<IStreamer>();
            mockStreamer.Setup(s => s.StreamReader(It.IsAny<string>())).Throws(new Exception("test"));

            var sut = new FileManager<Flight>(mockStreamer.Object);

            // Acts
            await Assert.ThrowsAnyAsync<Exception>(() => sut.ReadFromJsonAsync("test.txt"));
        }

        [Fact]
        public async Task StreamWriter_Should_WriteToStream_GiveVlidInput()
        {
            // Arrange
            var mockStreamer = new Mock<IStreamer>();

            var flightRoute = new FlightRoute("London", "Paris")
            {
                BaseCost = 50,
                BasePrice = 100,
                LoyaltyPointsGained = 5,
                MinimumTakeOffPercentage = 0.7
            };
            var plane = new Plane
            {
                Id = 123,
                Name = "Antonov AN-2",
                NumberOfSeats = 12
            };
            var flights = new List<Flight>();

            var fakeMemoryStream = new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(flights)));
            mockStreamer.Setup(s => s.StreamWriter(It.IsAny<string>())).Returns(() => new StreamWriter(fakeMemoryStream));

            var sut = new FileManager<Flight>(mockStreamer.Object);
            await sut.WriteToJsonAsync("test.txt", flights);
            mockStreamer.Verify(x => x.StreamWriter(It.IsAny<string>()));
        }

        [Fact]
        public async Task StreamWriter_Should_Catch_Throw_Exception()
        {
            // Arrange
            var mockStreamer = new Mock<IStreamer>();
            mockStreamer.Setup(s => s.StreamWriter(It.IsAny<string>())).Throws(new Exception("test"));

            var sut = new FileManager<Flight>(mockStreamer.Object);

            // Acts
            await Assert.ThrowsAnyAsync<Exception>(() => sut.WriteToJsonAsync("test.txt", new List<Flight>()));
        }
    }
}
