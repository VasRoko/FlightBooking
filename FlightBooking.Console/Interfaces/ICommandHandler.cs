using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightBooking.Console.Interfaces
{
    public interface ICommandHandler
    {
        Task<string> Handle(string enteredText, List<ICommand> commands = null);
    }
}
