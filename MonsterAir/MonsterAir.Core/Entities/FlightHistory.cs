using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterAir.Core.Entities
{
    public enum FlightStatus
    {
        Upcoming,
        Traveled
    }
    public class FlightHistory 
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FlightCode { get; set; }
        public string FlightName { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string TakeOfDate { get; set; }
        public string Time { get; set; }
        public string AirportName { get; set; }
        public double Price { get; set; }
        public FlightStatus Status { get; set; }
    }
}
