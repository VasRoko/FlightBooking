using System.IO;

namespace FlightBooking.Infrastructure.Interfaces
{
    public interface IStreamer
    {
        StreamWriter StreamWriter(string path);
        StreamReader StreamReader(string path);
    }
}
