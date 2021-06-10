using Autofac;
using MonsterAir.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonsterAir.Models.JourneyHistoryModels
{
    public class JourneyViewModel
    {
        private IJourneyService _journeyService;
        public JourneyViewModel()
        {
            _journeyService = Startup.AutofacContainer.Resolve<IJourneyService>();
        }

        public IEnumerable<HistoryBaseModel> GetUserFlightHistory(string userId)
        {
            var userAllJourneyHistory = _journeyService.GetUserJourney(userId);

            var userJourneyHistoryModel =  new List<HistoryBaseModel>();

            foreach (var history in userAllJourneyHistory)
            {
                var historyModel = new HistoryBaseModel
                {
                    FlightCode = history.FlightCode,
                    FlightName = history.FlightName,
                    Source = history.Source,
                    Destination = history.Destination,
                    TakeOfDate = history.TakeOfDate,
                    Time = history.Time,
                    AirportName = history.AirportName,
                    Price = history.Price
                };

                userJourneyHistoryModel.Add(historyModel);
            }

            return userJourneyHistoryModel;

        }       
    }
}
