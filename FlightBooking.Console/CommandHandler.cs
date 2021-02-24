using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using FlightBooking.Console.Commands;
using FlightBooking.Console.Interfaces;
using MediatR;

namespace FlightBooking.Console
{
    public class CommandHandler : ICommandHandler
    {
        private readonly IMediator _mediator;

        public CommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<string> Handle(string enteredText, List<ICommand> commands = null)
        {
            // setup list of comand for open closed principle
            if (commands == null)
            {
                commands = new List<ICommand>();
                commands.Add(new AddDiscountedCommand(_mediator));
                commands.Add(new PrintSummaryCommand(_mediator));
                commands.Add(new AddAirlineCommand(_mediator));
                commands.Add(new AddLoyaltyCommand(_mediator));
                commands.Add(new AddGeneralCommand(_mediator));
                commands.Add(new ExitCommand());
            }

            // find valid command and call it's Invoke method
            return await commands.First(x => x.IsMatch(enteredText)).InvokeResult(enteredText);
        }
    }
}
