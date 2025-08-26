using Microsoft.AspNetCore.Mvc;

namespace SCP.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
