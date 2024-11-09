using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WayFarer.Model;
using WayFarer.Repository;

namespace WayFarer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WayFarerDbContext _dbContext;

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

            // Dohvaćanje gradova i itinerara korisnika
            var cities = _dbContext.City.ToList();
            var itineraries = _dbContext.Itinerary
                .Include(i => i.City)
                .Where(i => i.UserId == userId)
                .ToList();

            var viewModel = new HomePageViewModel
            {
                Itineraries = itineraries,
                Cities = cities
            };

            return View(viewModel);
        }
    }

    // ViewModel za prikaz na glavnoj stranici
    public class HomePageViewModel
    {
        public List<Itinerary> Itineraries { get; set; } = new List<Itinerary>();
        public List<City> Cities { get; set; } = new List<City>();
    }
}
