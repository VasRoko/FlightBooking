using FlightBooking.Application.Common.Interfaces;

namespace FlightBooking.Application.Common.BussinesRules
{
    public class SeatsExceeded : IBussinesRule
    {
        private readonly int _passengers;
        private readonly int _availableSeats;

        public SeatsExceeded(int passengers, int availableSeats)
        {
            _passengers = passengers;
            _availableSeats = availableSeats;
        }
        public bool IsValid()
        {
            return _passengers < _availableSeats;
        }
    }
}
