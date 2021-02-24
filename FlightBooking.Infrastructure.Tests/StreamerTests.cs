using System;
using System.IO;
using Xunit;

namespace FlightBooking.Infrastructure.UnitTests
{
    public class StreamTests
    {
        [Fact]
        public void Should_GetValid_StreamReaderType()
        {
            // Arrange                 
            var sut = new Streamer();
            string path = $"{Environment.CurrentDirectory}" + "\\test.txt";

            // Act
            var result = sut.StreamReader(path);

            // Assert
            Assert.IsType<StreamReader>(result);
        }

        [Fact]
        public void StreamReader_Should_ThrowArgumentNullException()
        {
            // Arrange                 
            var sut = new Streamer();

            // Assert
            Assert.Throws<ArgumentNullException>(() => sut.StreamReader(""));
        }

        [Fact]
        public void Should_GetValid_StreamWriterType()
        {
            // Arrange
            var sut = new Streamer();
            string path = $"{Environment.CurrentDirectory}" + "\\test2.txt";


            // Act
            var result = sut.StreamWriter(path);

            // Assert
            Assert.IsType<StreamWriter>(result);
        }

        [Fact]
        public void StreamWriter_Should_ThrowArgumentNullException()
        {
            // Arrange                 
            var sut = new Streamer();

            // Assert
            Assert.Throws<ArgumentNullException>(() => sut.StreamWriter(""));
        }

    }
}
