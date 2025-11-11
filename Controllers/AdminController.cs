using Microsoft.AspNetCore.Mvc;

public class AdminController : Controller
{
    private readonly UserService _userService;
    private readonly AnnouncementService _announcementService;
    private readonly FeedbackService _feedbackService;
    private readonly BookingService _bookingService;

    public AdminController(UserService userService, AnnouncementService announcementService, FeedbackService feedbackService, BookingService bookingService)
    {
        _userService = userService;
        _announcementService = announcementService;
        _feedbackService = feedbackService;
        _bookingService = bookingService;
    }

    private bool IsAdmin() =>
        HttpContext.Session.GetString("Role")?.Equals("Admin", StringComparison.OrdinalIgnoreCase) == true;

    public async Task<IActionResult> Index(int page = 1, int limit = 20)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        var skip = (page - 1) * limit;
        var bookings = await _bookingService.GetAllAsync(skip, limit);
        var totalCount = await _bookingService.GetCountAsync();
        var totalPages = (int)Math.Ceiling((double)totalCount / limit);

        if (page < 1 || page > totalPages) page = 1;

        var users = await _userService.GetAllAsync();
        var announcements = await _announcementService.GetAllAsync();
        var feedback = await _feedbackService.GetAllAsync();

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;
        ViewBag.Limit = limit;

        var vm = new AdminDashboardViewModel
        {
            Users = users,
            Announcements = announcements,
            Feedback = feedback,
            Bookings = bookings,
            CurrentPage = page,
            TotalPages = totalPages,
            Limit = limit
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUser(string id)
    {
        if (!IsAdmin()) return Unauthorized();
        await _userService.DeleteAsync(id);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> PromoteUser(string id)
    {
        if (!IsAdmin()) return Unauthorized();
        await _userService.UpdateRoleAsync(id, "Admin");
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> AddAnnouncement(string title, string content)
    {
        if (!IsAdmin()) return Unauthorized();
        await _announcementService.CreateAsync(new Announcement { Title = title, Content = content, Date = DateTime.Now });
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteBooking(string id)
    {
        if (!IsAdmin()) return Unauthorized();
        await _bookingService.DeleteAsync(id);
        return RedirectToAction("Index");
    }
    [HttpPost]
    public async Task<IActionResult> DeleteAnnouncement(string id)
    {
        if (!IsAdmin()) return Unauthorized();
        await _announcementService.DeleteAsync(id);
        return RedirectToAction("Index");
    }
}
