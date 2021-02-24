using System;
using System.Threading.Tasks;
using FlightBooking.Console.Interfaces;


namespace FlightBooking.Console.Commands
{
    public class ExitCommand : ICommand
    {
        public string Name => "exit";

        public Task<string> InvokeResult(string value)
        {
            Environment.Exit(1);
            return Task.FromResult((value).ToString());
        }


        public bool IsMatch(string cmdName)
        {
            return cmdName.Contains(Name);
        }
    }
}
