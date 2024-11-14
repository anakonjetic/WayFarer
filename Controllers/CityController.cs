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
    }
}
