using FlightBooking.Application.Flights.Commands.CreatePassengers;
using FlightBooking.Console.Commands;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FlightBooking.Console.UnitTests.CommandTests
{
    public class AddGeneralCommandTests
    {
        [Fact]
        public void Should_Get_ValidCommandName()
        {
            // Arrange
            var mediatRMock = new Mock<IMediator>();
            var sut = new AddGeneralCommand(mediatRMock.Object);

            // Act
            // Assert
            Assert.Equal("add general", sut.Name);
        }

        [Fact]
        public void Should_ReturnTrue_IsMatchResut()
        {
            // Arrange
            var mediatRMock = new Mock<IMediator>();
            var sut = new AddGeneralCommand(mediatRMock.Object);
            // Act
            var result = sut.IsMatch("add general");
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Should_ReturnFalse_IsMatchResut()
        {
            // Arrange
            var mediatRMock = new Mock<IMediator>();
            var sut = new AddGeneralCommand(mediatRMock.Object);
            // Act
            var result = sut.IsMatch("");
            // Assert
            Assert.False(result);
        }
    }
}
