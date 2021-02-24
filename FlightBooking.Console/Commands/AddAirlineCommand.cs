using MediatR;
using System;
using System.Threading.Tasks;
using FlightBooking.Console.Interfaces;
using FlightBooking.Console.Utilities;
using FlightBooking.Application.Flights.Commands.CreatePassengers;

namespace FlightBooking.Console.Commands
{
    public class AddAirlineCommand : ICommand
    {
        private readonly IMediator _mediator;

        public string Name => "add airline";
        public AddAirlineCommand(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<string> InvokeResult(string value)
        {
            return await _mediator.Send(new CreateAirlineCommand()
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
