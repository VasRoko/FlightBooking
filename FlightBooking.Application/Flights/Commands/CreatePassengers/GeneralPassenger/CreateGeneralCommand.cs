using MediatR;

namespace FlightBooking.Application.Flights.Commands.CreatePassengers
{
    public class CreateGeneralCommand : IRequest<string>
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
