using Microsoft.AspNetCore.Mvc;

namespace SCP.Controllers
{
    public class ProfileController : Controller
    {
        private bool IsLoggedIn() =>
            !string.IsNullOrEmpty(HttpContext.Session.GetString("Username"));

        public IActionResult Index()
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }
    }
}
