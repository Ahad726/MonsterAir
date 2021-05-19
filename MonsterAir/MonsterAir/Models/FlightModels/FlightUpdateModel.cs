using Autofac;
using MonsterAir.Core.Entities;
using MonsterAir.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonsterAir.Models.FlightModels
{
    public class FlightUpdateModel
    {
        private IFlightService _flightService;
        public FlightUpdateModel()
        {
            _flightService = Startup.AutofacContainer.Resolve<IFlightService>();
        }

        public void AddNewFlight(FlightModel flight)
        {
            _flightService.AddNewFlight(new Flight
            {
                FlightCode = flight.FlightCode,
                FlightName = flight.FlightName,
                Source = flight.Source,
                Destination = flight.Destination,
                TakeOfDate = flight.TakeOfDate,
                AirportName = flight.AirportName,
                Price = flight.Price,
                Description = flight.Description,
            });
        }
    }
}
