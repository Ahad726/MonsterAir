using Autofac;
using MonsterAir.Core.Entities;
using MonsterAir.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonsterAir.Models.JourneyHistoryModels
{
    public class JourneyAddModel
    {
        private IJourneyService _journeyService;
        private IFlightService _flightService;
        public JourneyAddModel()
        {
            _journeyService = Startup.AutofacContainer.Resolve<IJourneyService>();
            _flightService = Startup.AutofacContainer.Resolve<IFlightService>();
        }

        public async Task AddUserJourneyHistory(string userId, int FlightId)
        {
            var flight = await _flightService.GetFlightByIdAsync(FlightId);

            _journeyService.AddFlightJourney(
                new JourneyHistory
                {
                    UserId = userId,
                    FlightCode = flight.FlightCode,
                    FlightName = flight.FlightName,
                    Source = flight.Source,
                    Destination = flight.Destination,
                    TakeOfDate = flight.TakeOfDate,
                    Time = flight.Time,
                    AirportName = flight.AirportName,
                    Price = flight.Price,
                    Status = JourneyStatus.Upcoming
                }
                );

        }

    }
}
