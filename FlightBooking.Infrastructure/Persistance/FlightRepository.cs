using System;
using System.Threading.Tasks;
using FlightBooking.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using FlightBooking.Domain.Entities;
using FlightBooking.Infrastructure.Interfaces;

namespace FlightBooking.Infrastructure.Persistance
{
    public class FlightRepository : IFlightRepository
    {
        private readonly string _dataLocationPath;
        private readonly IFileManager<Flight> _fileManager;

        public FlightRepository(IFileManager<Flight> fileManager)
        {
            _dataLocationPath = string.Format("{0}/data.json", Environment.CurrentDirectory);
            _fileManager = fileManager;
        }

        public async Task ScheduleFlightsAsync(List<Flight> flights)
        {
            var list = await _fileManager.ReadFromJsonAsync(_dataLocationPath);
            if (list == null || list.Count == 0)
            {
                await _fileManager.WriteToJsonAsync(_dataLocationPath, flights);
            }
        }

        public async Task CreateFlightsAsync(List<Flight> flights)
        {
            await _fileManager.WriteToJsonAsync(_dataLocationPath, flights);
        }

        public async Task UpdateFlightAsync(Flight flight)
        {
            var listOfFlights = await _fileManager.ReadFromJsonAsync(_dataLocationPath);
            var newList = listOfFlights.Where(x => x.Id != flight.Id).ToList();
            newList.Add(flight);
            await _fileManager.WriteToJsonAsync(_dataLocationPath, newList);
        }

        public async Task<IEnumerable<Flight>> GetScheduledFlightsAsync()
        {
            return await _fileManager.ReadFromJsonAsync(_dataLocationPath);
        }

        public async Task<Flight> GetScheduledFlightAsync(int flightId)
        {
            var list = await _fileManager.ReadFromJsonAsync(_dataLocationPath);
            return list.Where(x => x.Id == flightId).FirstOrDefault();
        }
    }
}
