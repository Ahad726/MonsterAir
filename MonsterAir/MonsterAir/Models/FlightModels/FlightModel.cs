using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MonsterAir.Models.FlightModels
{
    public class FlightModel
    {
        public int FlightId { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="Flight Code is required")]
        [Display(Name ="Flight Code")]
        public string FlightCode { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Flight Name is required")]
        [Display(Name = "Flight Name")]
        public string FlightName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Source is required")]
        [Display(Name = "Flight From")]
        public string Source { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Destination is required")]
        [Display(Name = "Flight To")]
        public string Destination { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Date is required")]
        [Display(Name = "Take off Date")]
        public DateTime? TakeOfDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Airport Name is required")]
        [Display(Name = "Airport")]
        public string AirportName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Price is required")]
        [Display(Name = "Ticket Price")]
        public double? Price { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
