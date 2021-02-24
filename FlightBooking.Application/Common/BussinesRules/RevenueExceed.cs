using FlightBooking.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightBooking.Application.Common.BussinesRules
{
    public class RevenueExceed : IBussinesRule
    {
        private readonly double _revenue; 
        public RevenueExceed(double revenue)
        {
            _revenue = revenue;
        }

        public bool IsValid()
        {
            return _revenue > 0;
        }
    }
}
