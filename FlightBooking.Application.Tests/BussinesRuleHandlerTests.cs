using FlightBooking.Application.Common;
using FlightBooking.Application.Common.BussinesRules;
using FlightBooking.Application.Common.Interfaces;
using FlightBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FlightBooking.Application.Tests
{
    public class BussinesRuleHandlerTests
    {
        [Fact]
        public void Should_EvaluateFalseIBussinesRule_GivedNoConditions()
        {
            // Arrange 
            var sut = new BussinesRuleEvaluator();
            // Act
            var result = sut.Evaluate(new FlightInfo());
            // Assert
            Assert.False(result); 
        }

        [Fact]
        public void Should_EvaluateTrueIBussinesRule_GivedValidConditions()
        {
            // Arrange 
            var bussinesRules = new List<IBussinesRule>();
            bussinesRules.Add(new MinPercentageBooked(5, 10, 0.40));
            bussinesRules.Add(new SeatsExceeded(5, 10));
            bussinesRules.Add(new RevenueExceed(500));

            var sut = new BussinesRuleEvaluator();
            // Act
            var result = sut.Evaluate(new FlightInfo(), bussinesRules);
            // Assert
            Assert.True(result);
        }
    }
}
