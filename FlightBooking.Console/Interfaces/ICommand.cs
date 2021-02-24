using System.Threading.Tasks;

namespace FlightBooking.Console.Interfaces
{
    public interface ICommand
    {
        public string Name { get; }
        public bool IsMatch(string cmdName);
        public Task<string> InvokeResult(string value);
    }
}
