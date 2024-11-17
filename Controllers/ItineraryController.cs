using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WayFarer.Controllers.Services;
using WayFarer.Model;
using WayFarer.Repository;

namespace WayFarer.Controllers
{
    public class ItineraryController : Controller
    {
        private readonly WayFarerDbContext _dbContext;
        private readonly WeatherService _weatherService;

        public ItineraryController(WayFarerDbContext dbContext, WeatherService weatherService)
        {
            _dbContext = dbContext;
            _weatherService = weatherService;
        }

        // Show itinerary details
        public async Task<IActionResult> Details(int id)
        {
            var itinerary = _dbContext.Itinerary
                .Include(i => i.City)
                .ThenInclude(c => c.Attractions)
                .FirstOrDefault(i => i.Id == id);

            if (itinerary == null)
            {
                return NotFound();
            }

            var weatherData = await _weatherService.GetWeatherByCityNameAsync(itinerary.City.Name);

            // Pass the weather data to the view
            ViewData["WeatherData"] = weatherData;

            return View("~/Views/User/ItineraryDetails.cshtml", itinerary);
        }

        // Show itinerary create page
        [HttpGet]
        public IActionResult Create()
        {
            var model = new ItineraryFormViewModel
            {
                Cities = _dbContext.City.ToList()
            };

            return View("~/Views/User/ItineraryCreate.cshtml", model);
        }

        // Create itinerary
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ItineraryFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Cities = _dbContext.City.ToList();
                return View("~/Views/User/ItineraryCreate.cshtml", model);
            }

            // get userId from user which is currently logged, in from session
            var userId = HttpContext.Session.GetInt32("UserId");

            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "User");
            }

            var itinerary = new Itinerary
            {
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                TotalPrice = model.TotalPrice,
                CityId = model.CityId,
                UserId = (int)userId
            };

            _dbContext.Itinerary.Add(itinerary);
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        // Edit itinerary
        public IActionResult Edit(int id)
        {
            var itinerary = _dbContext.Itinerary
                .Include(i => i.City)
                .FirstOrDefault(i => i.Id == id);

            if (itinerary == null)
            {
                return NotFound();
            }

            return View("~/Views/User/ItineraryEdit.cshtml", itinerary);
        }

        [HttpPost]
        public IActionResult Edit(int id, Itinerary updatedItinerary)
        {
            if (id != updatedItinerary.Id)
            {
                return BadRequest();
            }

            var itinerary = _dbContext.Itinerary.FirstOrDefault(i => i.Id == id);
            if (itinerary == null)
            {
                return NotFound();
            }

            itinerary.StartDate = updatedItinerary.StartDate;
            itinerary.EndDate = updatedItinerary.EndDate;
            itinerary.TotalPrice = updatedItinerary.TotalPrice;

            _dbContext.SaveChanges();

            return RedirectToAction("Details", new { id = itinerary.Id });
        }

        // Delete itinerary
        public IActionResult Delete(int id)
        {
            var itinerary = _dbContext.Itinerary.FirstOrDefault(i => i.Id == id);

            if (itinerary != null)
            {
                _dbContext.Itinerary.Remove(itinerary);
                _dbContext.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
