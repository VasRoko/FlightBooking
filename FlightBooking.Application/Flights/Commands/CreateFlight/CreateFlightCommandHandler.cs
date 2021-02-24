using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using FlightBooking.Application.Common.Interfaces;
using FlightBooking.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace FlightBooking.Application.Flights.Commands
{
    public class CreateFlightCommandHandler : IRequestHandler<CreateFlightCommand>
    {
        private readonly IFlightRepository _flightRepository;
        public CreateFlightCommandHandler(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public async Task<Unit> Handle(CreateFlightCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // For the purpose of this task values are hardcode however, we are now able to provide and create any type flights 
                var flightRoute = new FlightRoute("London", "Paris")
                {
                    BaseCost = 50,
                    BasePrice = 100,
                    LoyaltyPointsGained = 5,
                    MinimumTakeOffPercentage = 0.7
                };
                var flightRouteReturn = new FlightRoute("Paris", "London")
                {
                    BaseCost = 50,
                    BasePrice = 100,
                    LoyaltyPointsGained = 5,
                    MinimumTakeOffPercentage = 0.7
                };
                var flights = new List<Flight>() 
                { 
                    new Flight()
                    {
                        Id = 1,
                        Passengers = new List<Passenger>(),
                        FlightRoute = flightRoute,
                        Plane = new Plane {
                            Id = 123,
                            Name = "Antonov AN-2",
                            NumberOfSeats = 12
                        },
                    },
                    new Flight()
                    {
                        Id = 2,
                        Passengers = new List<Passenger>(),
                        FlightRoute = flightRouteReturn,
                        Plane = new Plane {
                            Id = 124,
                            Name = "Bombardier Q400",
                            NumberOfSeats = 12
                        }
                    },
                    new Flight()
                    {
                        Id = 3,
                        Passengers = new List<Passenger>(),
                        FlightRoute = flightRouteReturn,
                        Plane = new Plane {
                            Id = 125,
                            Name = "ATR 640",
                            NumberOfSeats = 12
                        }
                    }
                };
                await _flightRepository.ScheduleFlightsAsync(flights);
                return Unit.Value;
            } 
            catch
            {
                throw;
            }
        }
    }
}
