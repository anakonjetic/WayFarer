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

            
            var city = _dbContext.City.ToList();
            var itinerary = _dbContext.Itinerary
                .Include(i => i.City)
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
