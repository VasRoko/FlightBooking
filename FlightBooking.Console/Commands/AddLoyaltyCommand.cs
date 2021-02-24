using MediatR;
using System.Threading.Tasks;
using FlightBooking.Console.Interfaces;
using FlightBooking.Application.Flights.Commands.CreatePassengers;
using FlightBooking.Console.Utilities;

namespace FlightBooking.Console.Commands
{
    public class AddLoyaltyCommand : ICommand
    {
        public string Name => "add loyalty";

        private readonly IMediator _mediator;
        public AddLoyaltyCommand(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<string> InvokeResult(string value)
        {
            return await _mediator.Send(new CreateLoyaltyCommand()
            {
                Name = value.GetPassengerName(),
                Age = value.GetPassengerAge(),
                LoyaltyPoints = value.GetPassengerLoyaltyPoints(),
                IsUsingLoyaltyPoints = value.IsUsingLoyaltyPoints(),
            });
        }

        public bool IsMatch(string cmdName)
        {
            return cmdName.Contains(Name);
        }
    }
}
