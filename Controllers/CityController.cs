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

            // Ensure ViewBag.IsInWishlist is set to a valid boolean value
            var userId = HttpContext.Session.GetInt32("UserId").Value;
            var isInWishlist = _dbContext.Wishlist
                .Any(w => w.UserId == userId && w.CityId == id);

            ViewBag.IsInWishlist = isInWishlist;

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

        // Dodavanje ili uklanjanje iz wishliste
        [HttpPost]
        public async Task<IActionResult> ToggleWishlist(int cityId)
        {
            try
            {
                var userId = (int)HttpContext.Session.GetInt32("UserId");

                if (userId == null)
                {
                    return Json(new { success = false, message = "User not logged in." });
                }

                var wishlistEntry = await _dbContext.Wishlist
                    .FirstOrDefaultAsync(w => w.UserId == userId && w.CityId == cityId);

                if (wishlistEntry != null)
                {
                    _dbContext.Wishlist.Remove(wishlistEntry);
                }
                else
                {
                    _dbContext.Wishlist.Add(new Wishlist { UserId = userId, CityId = cityId });
                }

                await _dbContext.SaveChangesAsync();

                // Return JSON response with the updated state
                var isInWishlist = _dbContext.Wishlist
                    .Any(w => w.UserId == userId && w.CityId == cityId);

                return Json(new { success = true, isInWishlist });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult CitiesWishlist()
        {
            var userId = HttpContext.Session.GetInt32("UserId").Value;

            // Get all cities in the user's wishlist
            var citiesInWishlist = _dbContext.Wishlist
                .Where(w => w.UserId == userId)
                .Include(w => w.City) // Include related City data
                .Select(w => w.City) // Select the cities from the wishlist
                .ToList();

            if (citiesInWishlist == null || !citiesInWishlist.Any())
            {
                // If no cities in wishlist, return an empty list and display a message
                ViewBag.Message = "You have no cities in your wishlist yet. Start adding your dream destinations!";
                return View("~/Views/User/CitiesWishlist.cshtml", new List<City>());
            }

            return View("~/Views/User/CitiesWishlist.cshtml", citiesInWishlist);
        }

    }
}
