using MediatR;
using System.Threading.Tasks;
using FlightBooking.Console.Utilities;
using FlightBooking.Console.Interfaces;
using FlightBooking.Application.Flights.Commands.CreatePassengers;

namespace FlightBooking.Console.Commands
{
    public class AddGeneralCommand : ICommand
    {
        private readonly IMediator _mediator;

        public AddGeneralCommand(IMediator mediator)
        {
            _mediator = mediator;
        }
        public string Name => "add general";

        public async Task<string> InvokeResult(string value)
        {
            return await _mediator.Send(new CreateGeneralCommand()
            {
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
