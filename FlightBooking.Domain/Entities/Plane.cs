using System;
using System.Collections.Generic;
using System.Text;

namespace FlightBooking.Domain.Entities
{
    public class Plane
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfSeats { get; set; }
    }
}
