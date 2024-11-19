using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WayFarer.Model;
using WayFarer.Model.Enum;
using Microsoft.EntityFrameworkCore;
using WayFarer.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Internal;

namespace WayFarer.Controllers
{
    public class AdminController : Controller
    {
        private readonly WayFarerDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(WayFarerDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
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

                _dbContext.SaveChanges();
                return RedirectToAction("ManageCities");
            }

            return View(model);
        }

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

            var attraction = _dbContext.Attraction.Include(c => c.City).SingleOrDefault(c => c.Id == model.Id);

            attraction.Name = model.Name;
            attraction.Price = model.Price;
            attraction.Category = model.Category;
            attraction.CityId = model.CityId;

            _dbContext.Attraction.Update(attraction);
            _dbContext.SaveChanges();
            return RedirectToAction("ManageAttractions");
        }

   
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


        [HttpGet]
        public IActionResult FilterTableUsers(string roleFilter, string sortOption, string isActiveFilter)
        {
            var users = _dbContext.User.AsQueryable();

            if (!string.IsNullOrEmpty(roleFilter) && Enum.TryParse<Role>(roleFilter, out var roleEnum))
            {
                users = users.Where(u => u.Role == roleEnum);
            }

            // Check if the isActiveFilter value is correctly parsed
            if (!string.IsNullOrEmpty(isActiveFilter) && bool.TryParse(isActiveFilter, out var isActive))
            {
                users = users.Where(u => u.IsActive == isActive);
            }
            else
            {
                Console.WriteLine("isActiveFilter is null or invalid"); // Add this for debugging
            }

            users = sortOption switch
            {
                "NameAsc" => users.OrderBy(u => u.Name),
                "NameDesc" => users.OrderByDescending(u => u.Name),
                "RoleAsc" => users.OrderBy(u => u.Role),
                "RoleDesc" => users.OrderByDescending(u => u.Role),
                _ => users
            };

            return PartialView("_FilterTableUsers", users.ToList());
        }



        public IActionResult ManageUsers(string roleFilter, string sortOption)
        {
            var users = _dbContext.User.OrderBy(u => u.Name).AsQueryable();

         
            return View(users.ToList());
        }

        [HttpPost]
        public IActionResult ManageUsersToggleStatus(int userId)
        {
            var user = _dbContext.User.Find(userId);
            if (user != null)
            {
                user.IsActive = !user.IsActive;
                _dbContext.SaveChanges();
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult ManageUsersChangeRole(int userId, Role newRole)
        {
            var user = _dbContext.User.Find(userId);
            if (user != null)
            {
                user.Role = newRole;
                _dbContext.SaveChanges();
            }
            return Ok();
        }



        public IActionResult UploadCityPhoto(int id, IFormFile file)
        {
            var destination = Path.Combine(_webHostEnvironment.WebRootPath, file.FileName);
            Stream fileStream = new FileStream(path: destination, mode: FileMode.Create);
            file.CopyTo(fileStream);

            City city = _dbContext.City.Where(f => f.Id == id).FirstOrDefault();

            

            city.Image = "/" + file.FileName;

            _dbContext.City.Update(city);
            _dbContext.SaveChanges();

            return Json(new { success = true });
        }

        public IActionResult UploadAttractionPhoto(int id, IFormFile file)
        {
            var destination = Path.Combine(_webHostEnvironment.WebRootPath, file.FileName);
            Stream fileStream = new FileStream(path: destination, mode: FileMode.Create);
            file.CopyTo(fileStream);

            Attraction attraction = _dbContext.Attraction.Where(f => f.Id == id).FirstOrDefault();



            attraction.Image = "/" + file.FileName;

            _dbContext.Attraction.Update(attraction);
            _dbContext.SaveChanges();

            return Json(new { success = true });
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
