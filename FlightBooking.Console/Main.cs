using MediatR;
using System;
using System.Threading.Tasks;
using FlightBooking.Console.Interfaces;
using FlightBooking.Application.Flights.Commands;

namespace FlightBooking.Console
{
    public class Main
    {
        private readonly ICommandHandler _commandHandler;
        private readonly IMediator _mediatR;

        public Main(ICommandHandler commandHandler, IMediator mediatR)
        {
            _commandHandler = commandHandler;
            _mediatR = mediatR;
        }

        public async Task Run()
        {
            // set plane data! 
            await _mediatR.Send(new CreateFlightCommand());

            System.Console.WriteLine("Please enter command.");
            string command = System.Console.ReadLine().ToLower() ?? "";
            do
            {
                try
                {
                    string result = await _commandHandler.Handle(command);
                    System.Console.WriteLine(result);
                    System.Console.WriteLine("Please enter another command.");
                    command = System.Console.ReadLine().ToLower() ?? "";
                }
                catch (IndexOutOfRangeException)
                {
                    System.Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("Invalid Parmas");
                    System.Console.ResetColor();
                    System.Console.WriteLine("Please re-enter different command.");
                    System.Console.WriteLine("Example: add general User 23");
                    command = System.Console.ReadLine().ToLower() ?? "";
                }
                catch (Exception ex)
                {
                    System.Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("UNKNOWN INPUT");
                    System.Console.ResetColor();
                    System.Console.WriteLine("Please enter another command.");
                    command = System.Console.ReadLine().ToLower() ?? "";
                }
            } while (command != "exit");
        }
    }
}
