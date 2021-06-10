using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MonsterAir.Models;
using MonsterAir.Models.FlightModels;
using MonsterAir.Models.JourneyHistoryModels;
using MonsterAir.PaymentGateway;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using static MonsterAir.PaymentGateway.SSLCommerz;

namespace MonsterAir.Controllers
{
    [Authorize]
    public class FlightController : Controller
    {
        private readonly string storeID = string.Empty;
        private readonly string storePassword = string.Empty;
        private readonly EnvironmentVariables _configuration;

        private readonly string totalAmount = "10200";
        private readonly UserManager<IdentityUser> _userManager;

        public FlightController(IOptions<EnvironmentVariables> configuration, UserManager<IdentityUser> userManager)
        {
            _configuration = configuration.Value;
            storeID = _configuration.StoreId;
            storePassword = _configuration.StorePassword;
            _userManager = userManager;
        }

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

        [HttpGet]
        public IActionResult Confirm(int id)
        {
            var model = new FlightViewModel();
            FlightModel flightModel = model.Load(id);
            return View(flightModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(FlightModel model)
        {
            string baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/";

            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            NameValueCollection PostData = new()
            {
                { "total_amount", model.Price.ToString() },
                { "currency", "BDT"},
                { "tran_id", GenerateUniqueId() },
                { "success_url", baseUrl + "Flight/PaymentGatewayCallback" },
                { "fail_url", baseUrl + "Flight/PaymentGatewayCallback" },
                { "cancel_url", baseUrl + "Flight/PaymentGatewayCallback" },
                { "cus_name", user.UserName },
                { "cus_email", user.Email },
                { "cus_add1", "Address Line On" },
                { "cus_city", "Dhaka" },
                { "cus_postcode", "1219" },
                { "cus_country", "Bangladesh" },
                { "cus_phone", "8801XXXXXXXXX" },
                { "shipping_method", "NO" },
                { "product_name", "UD" },
                { "product_category", "Service" },
                { "product_profile", "general" },
                {"value_a", model.FlightId.ToString() }, 
                {"value_b",userId }
            };

            var sslcz = new SSLCommerz(storeID, storePassword, true);

            var response = sslcz.InitiateTransaction(PostData);

            return Redirect(response.GatewayPageURL);
        }

        private static string GenerateUniqueId()
        {
            long i = 1;

            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= (b + 1);
            }

            return string.Format("{0:x}", i - DateTime.Now.Ticks).ToUpper();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PaymentGatewayCallbackAsync(SSLCommerzValidatorResponse response)
        {
            if (!string.IsNullOrEmpty(response.status) && response.status == "VALID")
            {
                var userId = _userManager.GetUserId(User);
                SSLCommerz sslcz = new SSLCommerz(storeID, storePassword, true);

                if (sslcz.OrderValidate(response.tran_id, response.amount, response.currency, Request))
                {
                    var journeyModel = new JourneyAddModel();
                    await journeyModel.AddUserJourneyHistory(response.value_b, int.Parse(response.value_a));
                    return View("Success");
                }
            }

            if (!string.IsNullOrEmpty(response.status) && response.status == "FAILED")
            {
                return View("Fail");
            }

            if (!string.IsNullOrEmpty(response.status) && response.status == "CANCELLED")
            {
                return View("Cancel");
            }

            return View("Error");
        }

        [HttpGet]
        public async Task<IActionResult> BookingHistory()
        {
            var user = await _userManager.GetUserAsync(User);
            var viewModel = new JourneyViewModel();
            var allJourney = viewModel.GetUserFlightHistory(user.Id);
            return View(allJourney);
        }  
    }
}
