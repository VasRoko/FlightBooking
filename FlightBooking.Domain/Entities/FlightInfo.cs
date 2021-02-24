
namespace FlightBooking.Domain.Entities
{
    public class FlightInfo
    {
        public double CostOfFlight { get; set; }
        public double ProfitFromFlight { get; set; }
        public double MiniMumTakeOffPercentage { get; set; }
        public int TotalLoyaltyPointsAccrued  { get; set; }
        public int TotalLoyaltyPointsRedeemed { get; set; }
        public int TotalExpectedBaggage  { get; set; }
        public int SeatsTaken  { get; set; }
        public int AvailableSeats { get; set; }

    }
}
