using MonsterAir.Core.Entities;
using MonsterAir.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterAir.Core.Services
{
    public class JourneyService : IJourneyService
    {
        private readonly IFlightUnitOfWork _flightUnitOfWork;

        public JourneyService(IFlightUnitOfWork flightUnitOfWork)
        {
            _flightUnitOfWork = flightUnitOfWork;
        }

        public void AddFlightJourney(JourneyHistory journeyHistory)
        {
            _flightUnitOfWork.JourneyRepository.Add(journeyHistory);
            _flightUnitOfWork.Save();
        }

        public IEnumerable<JourneyHistory> GetUserJourney(string userId)
        {
            return _flightUnitOfWork.JourneyRepository.Get(x => x.UserId == userId,m => m.OrderBy(x => x.TakeOfDate));
        }
    }
}
