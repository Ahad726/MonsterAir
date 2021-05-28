using Autofac;
using MonsterAir.Core.Entities;
using MonsterAir.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonsterAir.Models.FlightModels
{
    public class FlightViewModel
    {
        private IFlightService _flightService;
        public FlightViewModel()
        {
            _flightService = Startup.AutofacContainer.Resolve<IFlightService>();

        }


        public object GetFlights(DataTablesAjaxRequestModel tableModel)
        {
            int total = 0;
            int totalFiltered = 0;
            var records = _flightService.GetFlights(
                tableModel.PageIndex,
                tableModel.PageSize,
                tableModel.SearchText,
                out total,
                out totalFiltered);

            return new
            {
                recordsTotal = total,
                recordsFiltered = totalFiltered,
                data = (from record in records
                        select new string[]
                        {
                                record.FlightId.ToString(),
                                record.FlightCode,
                                record.FlightName,
                                record.Source,
                                record.Destination,
                                record.AirportName,
                                record.TakeOfDate,
                                record.Time,
                                record.Price.ToString(),
                                record.Description,
                                 record.FlightId.ToString()
                        }
                    ).ToArray()

            };
        }

        public IEnumerable<Flight> GetAllFlights()
        {
            return _flightService.GetAllFlights();
        }

        public Flight GetById(int id)
        {
            return _flightService.GetFlight(id);
        }

        public FlightModel Load(int id)
        {
            var flight = _flightService.GetFlight(id);

            var dateTime = Convert.ToDateTime(flight.TakeOfDate + " " + flight.Time);

            return new FlightModel
            {
                FlightId = flight.FlightId,
                FlightCode = flight.FlightCode,
                FlightName = flight.FlightName,
                Source = flight.Source,
                Destination = flight.Destination,
                AirportName = flight.AirportName,
                TakeOfDate = dateTime,
                Price = flight.Price,
                Description = flight.Description

            };
        }
    }
}
