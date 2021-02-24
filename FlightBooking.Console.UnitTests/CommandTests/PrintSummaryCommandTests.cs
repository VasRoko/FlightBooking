using Moq;
using Xunit;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using FlightBooking.Console.Commands;
using FlightBooking.Application.ScheduledFlight.Queries;

namespace FlightBooking.Console.UnitTests.CommandTests
{
    public class PrintSummaryCommandTests
    {
        [Fact]
        public void Should_Get_ValidCommandName()
        {
            // Arrange
            var mediatRMock = new Mock<IMediator>();
            var sut = new PrintSummaryCommand(mediatRMock.Object);

            // Act // Assert
            Assert.Equal("print summary", sut.Name);
        }

        [Fact]
        public void Should_ReturnTrue_IsMatchResut()
        {
            // Arrange
            var mediatRMock = new Mock<IMediator>();
            var sut = new PrintSummaryCommand(mediatRMock.Object);
            // Act
            var result = sut.IsMatch("print summary");
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
