using FlightBooking.Application.ScheduledFlight.Queries;
using FlightBooking.Console.Interfaces;
using MediatR;
using System.Threading.Tasks;

namespace FlightBooking.Console.Commands
{
    public class PrintSummaryCommand : ICommand
    {
        private readonly IMediator _mediator;
        public string Name => "print summary";
        public PrintSummaryCommand(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<string> InvokeResult(string value)
        {
            return await _mediator.Send(new GetSummaryQuery());
        }

        public bool IsMatch(string cmdName)
        {
            return cmdName.Contains(Name);
        }
    }
}
