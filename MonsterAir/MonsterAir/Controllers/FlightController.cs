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
    //[Authorize]
    public class FlightController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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
        public IActionResult GetFlights()
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = new FlightViewModel();
            var data = model.GetFlights(tableModel);
            return Json(data);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = new FlightViewModel();
            FlightModel flightModel = model.Load(id);
            return View(flightModel);

        }

        [HttpPost]
        public IActionResult Edit(FlightModel model)
        {
            var flightUpModel = new FlightUpdateModel();
            flightUpModel.UpdateFlight(model);
            return RedirectToAction(nameof(FlightController.Index));

        }

        [HttpPost]
        public IActionResult Delete(IEnumerable<int> flightIds)
        {
            var model = new FlightUpdateModel();
            model.DeleteFlights(flightIds);
            return Json(flightIds.Count());

        }
    }
}
