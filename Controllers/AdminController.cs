using Microsoft.AspNetCore.Mvc;
using WayFarer.Model.Enum;

namespace WayFarer.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("UserRole") != Role.Administrator.ToString())
            {
                return Unauthorized(); 
            }

            return View();
        }
    }
}
