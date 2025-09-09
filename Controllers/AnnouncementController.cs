using Microsoft.AspNetCore.Mvc;

namespace SCP.Controllers
{
    public class AnnouncementsController : Controller
    {
        private readonly AnnouncementService _announcementService;

        public AnnouncementsController(AnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        public async Task<IActionResult> Index()
        {
            var announcements = await _announcementService.GetAllAsync();
            return View(announcements.OrderByDescending(a => a.Date));
        }
    }
}
