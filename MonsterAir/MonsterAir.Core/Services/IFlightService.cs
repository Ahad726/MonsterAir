using MonsterAir.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterAir.Core.Services
{
    public interface IFlightService
    {
        IEnumerable<Flight> GetFlights(
           int pageIndex,
           int pageSize,
           string searchText,
           out int total,
           out int totalFiltered);
        void AddNewFlight(Flight flight);
        Flight GetFlight(int id);
        IEnumerable<Flight> GetAllFlights();
        void EditFlight(Flight flight);
        void DeleteFlight(int id);
    }
}
