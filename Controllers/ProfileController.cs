using Microsoft.AspNetCore.Mvc;

namespace SCP.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
