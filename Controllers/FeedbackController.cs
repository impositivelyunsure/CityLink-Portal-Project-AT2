using Microsoft.AspNetCore.Mvc;

namespace SCP.Controllers
{
    public class FeedbackController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Submit(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                TempData["Message"] = "Thank you for your feedback!";
                return RedirectToAction("ThankYou");
            }

            return View("Index", feedback);
        }

        public IActionResult ThankYou()
        {
            return View();
        }
    }
}
