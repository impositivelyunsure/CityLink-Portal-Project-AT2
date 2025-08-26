using Microsoft.AspNetCore.Mvc;

namespace SCP.Controllers
{
    public class ServicesController : Controller
    {
        [HttpGet]
        public IActionResult Book()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Book(ServiceBooking booking)
        {
            if (ModelState.IsValid)
            {
                // Simulate saving the booking
                TempData["Message"] = "Service booking submitted!";
                return RedirectToAction("Confirmation");
            }

            return View(booking);
        }

        public IActionResult Confirmation()
        {
            return View();
        }
    }
}
