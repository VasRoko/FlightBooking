using FlightBooking.Application.Flights.Commands.CreatePassengers;
using FlightBooking.Console.Commands;
using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FlightBooking.Console.UnitTests.CommandTests
{
    public class AddAirlineCommandTests
    {

        [Fact]
        public void Should_Get_ValidCommandName()
        {
            // Arrange
            var mediatRMock = new Mock<IMediator>();
            var sut = new AddAirlineCommand(mediatRMock.Object);

            // Act
            // Assert
            Assert.Equal("add airline", sut.Name);
        }

        [Fact]
        public void Should_ReturnTrue_IsMatchResut()
        {
            // Arrange
            var mediatRMock = new Mock<IMediator>();
            var sut = new AddAirlineCommand(mediatRMock.Object);
            // Act
            var result = sut.IsMatch("add airline");
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Should_ReturnFalse_IsMatchResut()
        {
            // Arrange
            var mediatRMock = new Mock<IMediator>();
            var sut = new AddAirlineCommand(mediatRMock.Object);
            // Act
            var result = sut.IsMatch("");
            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task InvokeResult_ShuouldBeValidResponse()
        {
            // Arrange
            var mediatRMock = new Mock<IMediator>();
            mediatRMock.Setup(x => x.Send(It.IsAny<CreateAirlineCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync("test");
            var sut = new AddAirlineCommand(mediatRMock.Object);

            // Act
            var result = await sut.InvokeResult("add airline vas 222");

            // Assert
            Assert.Equal("test", result);
        }
    }
}
