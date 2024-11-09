using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WayFarer.Model;
using WayFarer.Repository;

namespace WayFarer.Controllers
{
    public class ItineraryController : Controller
    {
        private readonly WayFarerDbContext _dbContext;

        public ItineraryController(WayFarerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Show itinerary details
        public IActionResult Details(int id)
        {
            var itinerary = _dbContext.Itinerary
                .Include(i => i.City)             // Uključivanje povezane `City` za Itinerary
                .ThenInclude(c => c.Attractions)  // Uključivanje Attractions koji pripadaju tom `City`
                .FirstOrDefault(i => i.Id == id); // Dohvaćanje itinerara prema Id

            if (itinerary == null)
            {
                return NotFound();
            }

            // Specify the exact path for the view in the "User" folder
            return View("~/Views/User/ItineraryDetails.cshtml", itinerary);
        }

        // Show itinerary edit form
        public IActionResult Edit(int id)
        {
            var itinerary = _dbContext.Itinerary
                .Include(i => i.City)             // Uključivanje povezane `City` za Itinerary
                .ThenInclude(c => c.Attractions)  // Uključivanje Attractions koji pripadaju tom `City`
                .FirstOrDefault(i => i.Id == id);

            if (itinerary == null)
            {
                return NotFound();
            }

            // Specify the exact path for the view in the "User" folder
            return View("~/Views/User/ItineraryEdit.cshtml", itinerary);
        }

        // Edit itinerary
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
