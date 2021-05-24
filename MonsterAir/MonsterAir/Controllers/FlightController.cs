using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonsterAir.Models;
using MonsterAir.Models.FlightModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Add(FlightModel flight)
        {
            if (ModelState.IsValid)
            {
                var model = new FlightUpdateModel();
                model.AddNewFlight(flight);
                return RedirectToAction(nameof(FlightController.Index));
            }
            // If we got this far, something failed, redisplay form
            return View(flight);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetFlights()
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = new FlightViewModel();
            var data = model.GetFlights(tableModel);
            return Json(data);
        }
    }
}
