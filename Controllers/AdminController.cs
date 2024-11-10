using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WayFarer.Model;
using WayFarer.Model.Enum;
using Microsoft.EntityFrameworkCore;
using WayFarer.Repository;

namespace WayFarer.Controllers
{
    public class AdminController : Controller
    {
        private readonly WayFarerDbContext _dbContext;

        public AdminController(WayFarerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("UserRole") != Role.Administrator.ToString())
            {
                return Unauthorized();
            }

            return View();
        }

        [HttpGet]
        public IActionResult ManageCities()
        {
            var cities = _dbContext.City.ToList();
            return View(cities);
        }

        [HttpGet]
        public IActionResult ManageCitiesAdd()
        {
            var model = new CityViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult ManageCitiesAdd(CityViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var newCity = new City
            {
                Name = model.Name,
                Description = model.Description,
                Image = model.Image
            };

            _dbContext.City.Add(newCity);
            _dbContext.SaveChanges();

            return RedirectToAction("ManageCities");
        }

        [HttpGet]
        public IActionResult ManageCitiesEdit(int id)
        {
            var city = _dbContext.City.SingleOrDefault(c => c.Id == id);
            if (city == null)
            {
                return NotFound();
            }

            var model = new CityViewModel
            {
                Id = city.Id,
                Name = city.Name,
                Description = city.Description,
                Image = city.Image
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult ManageCitiesEdit(CityViewModel model)
        {
            if (ModelState.IsValid)
            {
                var city = _dbContext.City.SingleOrDefault(c => c.Id == model.Id);
                if (city == null)
                {
                    return NotFound();
                }

                city.Name = model.Name;
                city.Description = model.Description;
                city.Image = model.Image;

                _dbContext.SaveChanges();
                return RedirectToAction("ManageCities");
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult ManageCitiesDelete(int id)
        {
            var city = _dbContext.City.Find(id);
            if (city == null)
            {
                return NotFound();
            }

            _dbContext.City.Remove(city);
            _dbContext.SaveChanges();

            return RedirectToAction("ManageCities");
        }

        public IActionResult FilterTableAttractions(Category? categoryFilter, string sortOption, int? cityFilter)
        {
            var attractions = _dbContext.Attraction.Include(c => c.City).AsQueryable();

            if (categoryFilter.HasValue)
            {
                attractions = attractions.Where(a => a.Category == categoryFilter.Value);
            }

            if (cityFilter.HasValue)
            {
                attractions = attractions.Where(a => a.CityId == cityFilter.Value);
            }

            var sortedAttractions = attractions.AsEnumerable();

            sortedAttractions = sortOption switch
            {
                "PriceAsc" => sortedAttractions.OrderBy(a => a.Price),
                "PriceDesc" => sortedAttractions.OrderByDescending(a => a.Price),
                "RatingAsc" => sortedAttractions.OrderBy(a => a.Reviews?.Average(r => r.Rating) ?? 0),
                "RatingDesc" => sortedAttractions.OrderByDescending(a => a.Reviews?.Average(r => r.Rating) ?? 0),
                _ => sortedAttractions
            };

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_FilterTableAttractions", sortedAttractions.ToList());
            }

            return View("ManageAttractions", sortedAttractions.ToList());
        }


        public IActionResult ManageAttractions(Category? categoryFilter, string sortOption, int? cityFilter)
        {
            var attractions = _dbContext.Attraction.Include(c => c.City).AsQueryable();
            ViewBag.Cities = _dbContext.City.ToList();

            if (categoryFilter.HasValue)
            {
                attractions = attractions.Where(a => a.Category == categoryFilter.Value);
            }

            if (cityFilter.HasValue)
            {
                attractions = attractions.Where(a => a.CityId == cityFilter.Value);
            }

            attractions = sortOption switch
            {
                "PriceAsc" => attractions.OrderBy(a => a.Price),
                "PriceDesc" => attractions.OrderByDescending(a => a.Price),
                "RatingAsc" => attractions.OrderBy(a => a.Reviews.Average(r => r.Rating)),
                "RatingDesc" => attractions.OrderByDescending(a => a.Reviews.Average(r => r.Rating)),
                _ => attractions
            };

            return View(attractions.ToList());
        }

        [HttpGet]
        public IActionResult ManageAttractionsAdd()
        {
            var model = new Attraction() { };
            return View("ManageAttractionsAddEdit", model);
        }

        [HttpPost]
        public IActionResult ManageAttractionsAdd(Attraction model)
        {
            if (!ModelState.IsValid) return View("ManageAttractionsAddEdit", model);

            _dbContext.Attraction.Add(model);
            _dbContext.SaveChanges();
            return RedirectToAction("ManageAttractions");
        }

        [HttpGet]
        public IActionResult ManageAttractionsEdit(int id)
        {
            var model = _dbContext.Attraction.Include(c => c.City).FirstOrDefault(a => a.Id == id);
            if (model == null) return NotFound();

            return View("ManageAttractionsAddEdit", model);
        }

        [HttpPost]
        public IActionResult ManageAttractionsEdit(Attraction model)
        {
            if (!ModelState.IsValid) return View("ManageAttractionsAddEdit", model);

            _dbContext.Attraction.Update(model);
            _dbContext.SaveChanges();
            return RedirectToAction("ManageAttractions");
        }

        [HttpPost]
        public IActionResult ManageAttractionsDelete(int id)
        {
            var attraction = _dbContext.Attraction.Find(id);
            if (attraction == null) return NotFound();

            _dbContext.Attraction.Remove(attraction);
            _dbContext.SaveChanges();
            return RedirectToAction("ManageAttractions");
        }

        [HttpGet]
        public IActionResult ManageAttractionsDetails(int id)
        {
            var attraction = _dbContext.Attraction
                .Include(a => a.City)
                .FirstOrDefault(a => a.Id == id);

            if (attraction == null) return NotFound();

            return View(attraction);
        }


        [HttpGet]
        public JsonResult SearchCity(string query)
        {
            var cities = _dbContext.City
                .Where(c => c.Name.Contains(query))
                .Select(c => new { c.Id, c.Name })
                .Take(5)
                .ToList();

            return Json(cities);
        }
    

    }




    public class CityViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string Image { get; set; }
    }
}
