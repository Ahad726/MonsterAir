using MonsterAir.Core.Entities;
using MonsterAir.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterAir.Core.Services
{
    public class FlightService : IFlightService
    {
        private readonly IFlightUnitOfWork _flightUnitOfWork;

        public FlightService(IFlightUnitOfWork flightUnitOfWork)
        {
            this._flightUnitOfWork = flightUnitOfWork;
        }
        public void AddNewFlight(Flight flight)
        {
            if (flight == null || String.IsNullOrEmpty(flight.FlightName))
                throw new InvalidOperationException("Flight Name is Empty");

            _flightUnitOfWork.FlightRepository.Add(flight);
            _flightUnitOfWork.Save();

        }

        public void DeleteFlight(int id)
        {
            _flightUnitOfWork.FlightRepository.Remove(id);
            _flightUnitOfWork.Save();
        }

        public void EditFlight(Flight flight)
        {
            var oldFlight = _flightUnitOfWork.FlightRepository.GetById(flight.FlightId);

            oldFlight.FlightName = flight.FlightName;
            oldFlight.FlightCode = flight.FlightCode;
            oldFlight.FlightName = flight.FlightName;
            oldFlight.Source = flight.Source;
            oldFlight.Destination = flight.Destination;
            oldFlight.TakeOfDate = flight.TakeOfDate;
            oldFlight.AirportName = flight.AirportName;
            oldFlight.Time = flight.Time;
            oldFlight.Price = flight.Price;
            oldFlight.Description = flight.Description;
            _flightUnitOfWork.Save();
        }  
        public IEnumerable<Flight> GetAllFlights()
        {
            var flights = _flightUnitOfWork.FlightRepository.GetAll();
            return flights;
        }

        public Flight GetFlight(int id)
        {
            var flight = _flightUnitOfWork.FlightRepository.GetById(id);
            return flight;
        }

        public async Task<Flight> GetFlightByIdAsync(int id)
        {
            return await _flightUnitOfWork.FlightRepository.GetByIdAsync(id);
        }

        public IEnumerable<Flight> GetFlights(int pageIndex, int pageSize, string searchText, out int total, out int totalFiltered)
        {
            return _flightUnitOfWork.FlightRepository.Get(
                
                out total,
                out totalFiltered,
                 x => x.FlightName.Contains(searchText) || x.FlightCode.Contains(searchText)
                 || x.AirportName.Contains(searchText) || x.Source.Contains(searchText)
                 || x.Destination.Contains(searchText) || x.TakeOfDate.Contains(searchText)
                 || x.Time.Contains(searchText) || x.Price.ToString().Contains(searchText),
                null,
                "",
                pageIndex,
                pageSize,
                true);
        }
    }
}
