using FlightBooking.Infrastructure.Interfaces;
using System;
using System.IO;

namespace FlightBooking.Infrastructure
{
    public class Streamer : IStreamer
    {
        public StreamReader StreamReader(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            return new StreamReader(path);
   
        }

        public StreamWriter StreamWriter(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            return new StreamWriter(path);
        }
    }
}
