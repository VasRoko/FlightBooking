using FlightBooking.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightBooking.Application.Common.Interfaces
{
    public interface IFlightRepository
    {
        /// <summary>
        /// Seeds flights into a repo. Only for test purpose! 
        /// This would be Add() or AddRange() method in a real world app.
        /// </summary>
        /// <param name="flights"></param>
        /// <returns></returns>
        public Task ScheduleFlightsAsync(List<Flight> flights);

        /// <summary>
        /// Updates any flight params
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        public Task UpdateFlightAsync(Flight flight);

        /// <summary>
        /// Get specific fly if FlightId is known.
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns></returns>
        public Task<Flight> GetScheduledFlightAsync(int flightId);

        /// <summary>
        /// Gets list of scheduled flights.
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Flight>> GetScheduledFlightsAsync();

    }
}
