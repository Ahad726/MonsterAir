using MonsterAir.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterAir.Core.Services
{
    public interface IJourneyService
    {
        void AddFlightJourney(JourneyHistory journeyHistory);
        IEnumerable<JourneyHistory> GetUserJourney();
    }
}
