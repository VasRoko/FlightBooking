using FlightBooking.Application.Flights.Commands.CreatePassengers;
using FlightBooking.Console.Commands;
using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FlightBooking.Console.UnitTests.CommandTests
{
    public class AddLoyaltyCommandTests
    {
        [Fact]
        public void Should_Get_ValidCommandName()
        {
            // Arrange
            var mediatRMock = new Mock<IMediator>();
            var sut = new AddLoyaltyCommand(mediatRMock.Object);

            // Act // Assert
            Assert.Equal("add loyalty", sut.Name);
        }

        [Fact]
        public void Should_ReturnTrue_IsMatchResut()
        {
            // Arrange
            var mediatRMock = new Mock<IMediator>();
            var sut = new AddLoyaltyCommand(mediatRMock.Object);
            // Act
            var result = sut.IsMatch("add loyalty");
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Should_ReturnFalse_IsMatchResut()
        {
            // Arrange
            var mediatRMock = new Mock<IMediator>();
            var sut = new PrintSummaryCommand(mediatRMock.Object);
            // Act
            var result = sut.IsMatch("");
            // Assert
            Assert.False(result);
        }
    }
}
