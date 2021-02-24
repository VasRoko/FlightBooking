
using System.Collections.Generic;

namespace FlightBooking.Domain.Entities
{
    public class Flight 
    {
        public int Id { get; set; }
        public List<Passenger> Passengers { get; set; }
        public FlightRoute FlightRoute { get; set; }
        public Plane Plane { get; set; }
    }
}
