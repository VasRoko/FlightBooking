using FlightBooking.Application.Flights.Commands.CreatePassengers;
using FlightBooking.Console.Commands;
using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FlightBooking.Console.UnitTests.CommandTests
{
    public class AddDiscountedCommandTests
    {
        [Fact]
        public void Should_Get_ValidCommandName()
        {
            // Arrange
            var mediatRMock = new Mock<IMediator>();
            var sut = new AddDiscountedCommand(mediatRMock.Object);

            // Act
            // Assert
            Assert.Equal("add discounted", sut.Name);
        }

        [Fact]
        public void Should_ReturnTrue_IsMatchResut()
        {
            // Arrange
            var mediatRMock = new Mock<IMediator>();
            var sut = new AddDiscountedCommand(mediatRMock.Object);
            // Act
            var result = sut.IsMatch("add discounted");
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Should_ReturnFalse_IsMatchResut()
        {
            // Arrange
            var mediatRMock = new Mock<IMediator>();
            var sut = new AddDiscountedCommand(mediatRMock.Object);
            // Act
            var result = sut.IsMatch("");
            // Assert
            Assert.False(result);
        }

    }
}
