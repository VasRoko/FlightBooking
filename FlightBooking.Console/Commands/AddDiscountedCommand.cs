using System.Threading.Tasks;
using FlightBooking.Application.Flights.Commands.CreatePassengers;
using FlightBooking.Console.Interfaces;
using FlightBooking.Console.Utilities;
using MediatR;

namespace FlightBooking.Console.Commands
{
    public class AddDiscountedCommand : ICommand
    {
        private readonly IMediator _mediator;

        public string Name => "add discounted";

        public AddDiscountedCommand(IMediator mediator)
        {
            _mediator = mediator;
        }

        public  async Task<string> InvokeResult(string value)
        {
            return await _mediator.Send(new CreateDiscountedCommand() { 
                Name = value.GetPassengerName(),
                Age = value.GetPassengerAge(),
            });
        }

        public bool IsMatch(string cmdName)
        {
            return cmdName.Contains(Name);
        }
    }
}
