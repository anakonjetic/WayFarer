using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WayFarer.Model;
using WayFarer.Model.Enum;
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
