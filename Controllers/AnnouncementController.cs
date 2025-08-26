using Microsoft.AspNetCore.Mvc;

namespace SCP.Controllers
{
    public class AnnouncementsController : Controller
    {
        public IActionResult Index()
        {
            var announcements = new List<Announcement>
            {
                new Announcement { Title = "Recycling Day", Description = "New recycling policy effective next week.", DatePosted = DateTime.Today },
                new Announcement { Title = "Community BBQ", Description = "Free BBQ at the park this Saturday!", DatePosted = DateTime.Today.AddDays(-2) }
            };

            return View(announcements);
        }
    }
}
