using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WayFarer.Model;
using WayFarer.Repository;

namespace WayFarer.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private WayFarerDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, WayFarerDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {

            // Provjera je li korisnik prijavljen
            if (!HttpContext.Session.GetInt32("UserId").HasValue)
            {
                return RedirectToAction("Login", "User");
            }

            var userId = HttpContext.Session.GetInt32("UserId").Value;

            var city = _dbContext.City.ToList();
            var itinerary = _dbContext.Itinerary
                .Include(i => i.City)
                .Where(u => u.UserId == userId)
                .ToList();


            var viewModel = new HomePageViewModel
            {
                Itineraries = itinerary,
                Cities = city
            };

            return View(viewModel);
        }
    }

    public class HomePageViewModel
    {
        public List<Itinerary> Itineraries { get; set; }
        public List<City> Cities { get; set; }

    }
}
