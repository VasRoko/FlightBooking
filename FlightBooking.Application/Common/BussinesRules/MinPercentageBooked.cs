using FlightBooking.Application.Common.Interfaces;

namespace FlightBooking.Application.Common.BussinesRules
{
    public class MinPercentageBooked : IBussinesRule
    {
        private readonly double _seatsTaken;
        private readonly double _numOfSeats;
        private readonly double _miniMumTakeOffPercentage;

        public MinPercentageBooked(int seatsTaken, int numOfSeats, double miniMumTakeOffPercentage)
        {
            _seatsTaken = seatsTaken;
            _numOfSeats = numOfSeats;
            _miniMumTakeOffPercentage = miniMumTakeOffPercentage;
        }
        public bool IsValid()
        {
            return (_seatsTaken / _numOfSeats) > _miniMumTakeOffPercentage;
        }
    }
}
