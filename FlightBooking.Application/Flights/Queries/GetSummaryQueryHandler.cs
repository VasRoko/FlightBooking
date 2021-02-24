using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FlightBooking.Domain.Entities;
using FlightBooking.Domain.Entities.Enums;
using FlightBooking.Application.Common.Interfaces;
using FlightBooking.Application.ScheduledFlight.Queries;

namespace FlightBooking.Application.Flights.Queries
{
    public class GetSummaryQueryHandler : IRequestHandler<GetSummaryQuery, string>
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IBussinesRuleEvaluator _bussinesRuleEvaluator;

        private readonly string _verticalWhiteSpace = Environment.NewLine + Environment.NewLine;
        private readonly string _newLine = Environment.NewLine;
        private const string Indentation = "    ";

        public GetSummaryQueryHandler(IFlightRepository flightRepository, IBussinesRuleEvaluator bussinesRuleEvaluator)
        {
            _flightRepository = flightRepository;
            _bussinesRuleEvaluator = bussinesRuleEvaluator;
        }
        public async Task<string> Handle(GetSummaryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Setup data for rule validation
                var flight = await _flightRepository.GetScheduledFlightAsync(1);
                var flightInfo = new FlightInfo()
                {
                    AvailableSeats = flight.Plane.NumberOfSeats,
                    MiniMumTakeOffPercentage = flight.FlightRoute.MinimumTakeOffPercentage,
                };

                // Calculate Result! 
                var result = _newLine;
                    result += "Flight summary for " + flight.FlightRoute.Origin + " to " + flight.FlightRoute.Destination;

                foreach (var passenger in flight.Passengers)
                {

                    switch (passenger.Type)
                    {
                        case (PassengerType.GENERAL):
                            {
                                flightInfo.ProfitFromFlight += flight.FlightRoute.BasePrice;
                                flightInfo.TotalExpectedBaggage++;
                                break;
                            }
                        case (PassengerType.LOYALTYMEMBER):
                            {
                                if (passenger.IsUsingLoyaltyPoints)
                                {
                                    var loyaltyPointsRedeemed = Convert.ToInt32(Math.Ceiling(flight.FlightRoute.BasePrice));
                                    passenger.LoyaltyPoints -= loyaltyPointsRedeemed;
                                    flightInfo.TotalLoyaltyPointsRedeemed += loyaltyPointsRedeemed;
                                }
                                else
                                {
                                    flightInfo.TotalLoyaltyPointsAccrued += flight.FlightRoute.LoyaltyPointsGained;
                                    flightInfo.ProfitFromFlight += flight.FlightRoute.BasePrice;
                                }
                                flightInfo.TotalExpectedBaggage += 2;
                                break;
                            }
                        case (PassengerType.AIRLINEEMPLOYEE):
                            {
                                flightInfo.TotalExpectedBaggage += 1;
                                break;
                            }
                        case(PassengerType.DISCOUNTED):
                            {
                                break;
                            }
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    flightInfo.CostOfFlight += flight.FlightRoute.BaseCost;
                    flightInfo.SeatsTaken++;
                }

                result += _verticalWhiteSpace;
                result += "Total passengers: " + flightInfo.SeatsTaken;
                result += _newLine;
                result += Indentation + "General sales: " + flight.Passengers.Count(p => p.Type == PassengerType.GENERAL);
                result += _newLine;
                result += Indentation + "Discounted sales: " + flight.Passengers.Count(p => p.Type == PassengerType.DISCOUNTED);
                result += _newLine;
                result += Indentation + "Loyalty member sales: " + flight.Passengers.Count(p => p.Type == PassengerType.LOYALTYMEMBER);
                result += _newLine;
                result += Indentation + "Airline employee comps: " + flight.Passengers.Count(p => p.Type == PassengerType.AIRLINEEMPLOYEE);

                result += _verticalWhiteSpace;
                result += "Total expected baggage: " + flightInfo.TotalExpectedBaggage;
                result += _verticalWhiteSpace;
                result += "Total revenue from flight: " + flightInfo.ProfitFromFlight;
                result += _newLine;
                result += "Total costs from flight: " + flightInfo.CostOfFlight;
                result += _newLine;

                flightInfo.ProfitFromFlight = flightInfo.ProfitFromFlight - flightInfo.CostOfFlight;
                result += (flightInfo.ProfitFromFlight > 0 ? "Flight generating profit of: " : "Flight losing money of: ") + flightInfo.ProfitFromFlight;

                result += _verticalWhiteSpace;
                result += "Total loyalty points given away: " + flightInfo.TotalLoyaltyPointsAccrued + _newLine;
                result += "Total loyalty points redeemed: " + flightInfo.TotalLoyaltyPointsRedeemed + _newLine;
                result += _verticalWhiteSpace;

                if (_bussinesRuleEvaluator.Evaluate(flightInfo, null))
                {
                    result += "THIS FLIGHT MAY PROCEED";
                }
                else
                {
                    var listOffl = await _flightRepository.GetScheduledFlightsAsync();
                    var altFlights = listOffl.Where(x => x.Id != flight.Id).ToList();
                    result += "FLIGHT MAY NOT PROCEED";
                    result += _newLine;
                    result += "Other more suitable aircraft are:";
                    result += _newLine;
                    foreach (var newFlight in altFlights)
                    {
                        result += newFlight.Plane.Name + " could handle this flight.";
                        result += _newLine;
                    }
                }
                return result;
            } 
            catch
            {
                throw;
            }
        }
    }
}
