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
        [HttpPost]
        public IActionResult Register(string name, string surname, DateTime dateOfBirth, string email, Gender gender, Role role, string username, string password)
        {
            if (_dbContext.User.Any(u => u.Username == username || u.Email == email))
            {
                return BadRequest("Username or e-mail is already in use.");
            }

            // Kreira novog korisnika
            var newUser = new User(name, surname, dateOfBirth, email, gender, role, username, password);
            _dbContext.User.Add(newUser);
            _dbContext.SaveChanges();

            // Automatski prijavi korisnika nakon registracije
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

            // Postavljanje sesije nakon uspješne prijave
            HttpContext.Session.SetInt32("UserId", user.Id);

            return RedirectToAction("Index", "Home");
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
}
