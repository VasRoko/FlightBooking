﻿using FlightBooking.Application.Common.Interfaces;
using FlightBooking.Domain.Entities;
using FlightBooking.Domain.Entities.Enums;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlightBooking.Application.Flights.Commands.CreatePassengers
{
    public class CreateAirlineCommandHandler : IRequestHandler<CreateAirlineCommand, string>
    {
        private readonly IFlightRepository _flightRepository;
        public CreateAirlineCommandHandler(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }
        public async Task<string> Handle(CreateAirlineCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // hardcode flight id number! normally Id is provided by some type of selection
                // this is only for demo!
                Flight flight = await _flightRepository.GetScheduledFlightAsync(1);
                var newPassenger = new Passenger()
                {
                    Type = PassengerType.AIRLINEEMPLOYEE,
                    Name = request.Name,
                    Age = request.Age
                };
                flight.Passengers.Add(newPassenger);
                await _flightRepository.UpdateFlightAsync(flight);
                return string.Format("Added new {0} passenger to flight number: {1}", newPassenger.Type, flight.Id);
            }
            catch
            {
                throw;
            }
        }
    }
}
