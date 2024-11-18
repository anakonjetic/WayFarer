using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WayFarer.Model;
using WayFarer.Repository;

namespace WayFarer.Controllers
{
    public class CityController : Controller
    {
        private readonly WayFarerDbContext _dbContext;

        public CityController(WayFarerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // City details
        [HttpGet]
        public IActionResult Details(int id)
        {
            var city = _dbContext.City
                .Where(c => c.Id == id)  
                .Include(c => c.Attractions)    
                .Include(c => c.Reviews)
                .ThenInclude(r => r.User)
                .FirstOrDefault();

            if (city == null)
            {
                return NotFound();
            }

            return View("~/Views/User/CityDetails.cshtml", city);
        }

        // Show the form to create a new review for a city
        public IActionResult CreateReview(int cityId, int itineraryId)
        {
            var city = _dbContext.City.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var review = new Review
            {
                CityId = cityId,
                UserId = (int)HttpContext.Session.GetInt32("UserId"),
            };

            ViewBag.ItineraryId = itineraryId;
            ViewBag.CityName = city.Name;

            return View("~/Views/User/ReviewCreate.cshtml", review);
        }

        // Handle the submission of a new review
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReview(Review review, int itineraryId)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Review.Add(review);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Details", "City", new { id = review.CityId });
            }

            return View("~/Views/User/ReviewCreate.cshtml", review);
        }
    }
}
