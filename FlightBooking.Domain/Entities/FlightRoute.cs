namespace FlightBooking.Domain.Entities
{
    public class FlightRoute
    {
        public FlightRoute(string origin, string destination)
        {
            Origin = origin;
            Destination = destination;
        }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string Title => Origin + " to " + Destination;
        public double BasePrice { get; set; }
        public double BaseCost { get; set; }
        public int LoyaltyPointsGained { get; set; }
        public double MinimumTakeOffPercentage { get; set; }
    }
}
