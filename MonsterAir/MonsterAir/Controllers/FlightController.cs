using Microsoft.AspNetCore.Mvc;
using MonsterAir.Models.FlightModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonsterAir.Controllers
{
    public class FlightController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(FlightModel flight)
        {
            if (!ModelState.IsValid)
            {
                var model = new FlightUpdateModel();
                model.AddNewFlight(flight);
                return RedirectToAction(nameof(FlightController.Index));
            }
            return RedirectToAction(nameof(FlightController.Add));
        }
    }
}
