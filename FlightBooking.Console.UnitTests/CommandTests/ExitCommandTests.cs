using FlightBooking.Console.Commands;
using FlightBooking.Console.Interfaces;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace FlightBooking.Console.UnitTests.CommandTests
{
    public class ExitCommandTests
    {
        [Fact]
        public void Should_Get_ValidCommandName()
        {
            // Arrange
            var sut = new ExitCommand();

            // Act
            // Assert
            Assert.Equal("exit", sut.Name);
        }

        [Fact]
        public void Should_ReturnTrue_IsMatchResut()
        {
            // Arrange
            var sut = new ExitCommand();
            // Act
            var result = sut.IsMatch("exit");
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Should_ReturnFalse_IsMatchResut()
        {
            // Arrange
            var sut = new ExitCommand();
            // Act
            var result = sut.IsMatch("");
            // Assert
            Assert.False(result);
        }
    } 
}
