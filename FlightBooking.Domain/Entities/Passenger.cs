﻿using FlightBooking.Domain.Entities.Enums;

namespace FlightBooking.Domain.Entities
{
    public class Passenger
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int AllowedBags { get; set; }
        public int LoyaltyPoints { get; set; }
        public PassengerType Type { get; set; }
        public bool IsUsingLoyaltyPoints { get; set; }

    }
}
