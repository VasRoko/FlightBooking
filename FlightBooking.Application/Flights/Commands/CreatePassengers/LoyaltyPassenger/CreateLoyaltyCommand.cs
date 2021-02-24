using MediatR;

namespace FlightBooking.Application.Flights.Commands.CreatePassengers
{
    public class CreateLoyaltyCommand : IRequest<string>
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int LoyaltyPoints { get; set; }
        public bool IsUsingLoyaltyPoints { get; set; }
    }
}
