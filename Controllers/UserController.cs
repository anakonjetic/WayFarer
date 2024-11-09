using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WayFarer.Model;
using WayFarer.Model.Enum;
using WayFarer.Repository;

namespace WayFarer.Controllers
{
    public class UserController : Controller
    {

        private readonly WayFarerDbContext _dbContext;

        public UserController(WayFarerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Akcija za registraciju
        [HttpGet]
        public IActionResult Register()
        {
            // Vraća formu za registraciju 
            return View();
        }


        [HttpPost]
        public IActionResult Register(UserRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Return view with validation messages
            }

            if (_dbContext.User.Any(u => u.Username == model.Username || u.Email == model.Email))
            {
                ModelState.AddModelError("", "Username or e-mail is already in use.");
                return View(model);
            }

            // Create a new user based on the form input
            var newUser = new User(
                model.Name,
                model.Surname,
                model.DateOfBirth,
                model.Email,
                model.Gender,
                model.Role, // Defaults to User
                model.Username,
                model.Password,
                true
            );

            _dbContext.User.Add(newUser);
            _dbContext.SaveChanges();

            // Automatically log in the user after registration
            HttpContext.Session.SetInt32("UserId", newUser.Id);

            return RedirectToAction("Index", "Home");
        }

        // Akcija za login
        [HttpGet]
        public IActionResult Login()
        {
            // Vraća formu za prijavu bez ikakvih provera
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _dbContext.User.SingleOrDefault(u => u.Username == username && u.Password == password);

            if (user == null)
            {
                return BadRequest("Wrong username or password.");
            }

            if (!user.IsActive)
            {
                ModelState.AddModelError("", "Your account is deactivated. Please contact support for assistance.");
                return View();
            }

            // Postavljanje sesije nakon uspješne prijave
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserRole", user.Role.ToString());

            if (user.Role == Role.Administrator)
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            
        }

        // Akcija za logout
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            return RedirectToAction("Login");
        }
    }

    public class UserLoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class UserRegisterViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public Role Role { get; set; } = Role.Basic;
    }
}
