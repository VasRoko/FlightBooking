using Xunit;
using System.Threading.Tasks;
using Moq;
using MediatR;
using System.Collections.Generic;
using FlightBooking.Console.Interfaces;
using System.Threading;
using FlightBooking.Application.Flights.Commands.CreatePassengers;
using FlightBooking.Application.ScheduledFlight.Queries;
using System.Diagnostics;

namespace FlightBooking.Console.UnitTests
{
    public class CommandHandlerTests
    {
        [Fact]
        public async Task Should_Handle_GivenValid_AddAirlineCommand()
        {
            // Arrange
            var mediatRMock = new Mock<IMediator>();
            mediatRMock.Setup(x => x.Send(It.IsAny<CreateAirlineCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync("airline");
            
            var sut = new CommandHandler(mediatRMock.Object);


            // Act
            var result = await sut.Handle("add airline vas 222");
            // Assert
            Assert.Equal("airline", result);
        }

        [Fact]
        public async Task TaskShould_Handle_GivenValid_DiscountedCommand()
        {
            // Arrange
            var mediatRMock = new Mock<IMediator>();
            mediatRMock.Setup(x => x.Send(It.IsAny<CreateDiscountedCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync("discounted");
            var sut = new CommandHandler(mediatRMock.Object);

            // Act
            var result = await sut.Handle("add discounted vas 222");

            // Assert
            Assert.Equal("discounted", result);
        }

        [Fact]
        public async Task Should_Handle_GivenValid_CreateGeneralCommand()
        {
            // Arrange
            Mock<IMediator> mediatRMock = new Mock<IMediator>();
            mediatRMock.Setup(x => x.Send(It.IsAny<CreateGeneralCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync("general");
            var sut = new CommandHandler(mediatRMock.Object);

            // Act
            var result = await sut.Handle("add general vas 222");

            // Assert
            Assert.Equal("general", result);
        }

        [Fact]
        public async Task Should_Handle_GivenValid_CreateLoyaltyCommand()
        {
            // Arrange
            Mock<IMediator> mediatRMock = new Mock<IMediator>();
            mediatRMock.Setup(x => x.Send(It.IsAny<CreateLoyaltyCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync("loyalty");
            var sut = new CommandHandler(mediatRMock.Object);

            // Act
            var result = await sut.Handle("add loyalty test 45 1250 false");

            // Assert
            Assert.Equal("loyalty", result);
        }

        [Fact]
        public async Task Should_Handle_GivenValid_PrintSummaryCommand()
        {
            // Arrange
            Mock<IMediator> mediatRMock = new Mock<IMediator>();
            mediatRMock.Setup(x => x.Send(It.IsAny<GetSummaryQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync("summary");
            var sut = new CommandHandler(mediatRMock.Object);

            // Act
            var result = await sut.Handle("print summary");

            // Assert
            Assert.Equal("summary", result);
        }
    }
}
