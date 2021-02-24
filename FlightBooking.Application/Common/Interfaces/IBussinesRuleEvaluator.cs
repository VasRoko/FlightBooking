using FlightBooking.Domain.Entities;
using System.Collections.Generic;

namespace FlightBooking.Application.Common.Interfaces
{
    public interface IBussinesRuleEvaluator
    {
        public bool Evaluate(FlightInfo flightInfo, List<IBussinesRule> bussinesRules);
    }
}
