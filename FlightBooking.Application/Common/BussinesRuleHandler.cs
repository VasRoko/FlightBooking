using FlightBooking.Application.Common.BussinesRules;
using FlightBooking.Application.Common.Interfaces;
using FlightBooking.Domain.Entities;
using System.Collections.Generic;

namespace FlightBooking.Application.Common
{
    public class BussinesRuleEvaluator : IBussinesRuleEvaluator
    {
        public bool Evaluate(FlightInfo flightInfo, List<IBussinesRule> bussinesRules = null)
        {
            bool result = false;

            if (bussinesRules == null)
            {
                bussinesRules = new List<IBussinesRule>();
                bussinesRules.Add(new MinPercentageBooked(flightInfo.SeatsTaken, flightInfo.AvailableSeats, flightInfo.MiniMumTakeOffPercentage));
                bussinesRules.Add(new SeatsExceeded(flightInfo.SeatsTaken, flightInfo.AvailableSeats));
                bussinesRules.Add(new RevenueExceed(flightInfo.ProfitFromFlight));
            }

            foreach (var bussinesRule in bussinesRules)
            {
                result = bussinesRule.IsValid();
            }
            return result;
        }
    }
}
