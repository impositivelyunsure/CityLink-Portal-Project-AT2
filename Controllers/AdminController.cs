// FILE: Controllers/AdminController.cs
using Microsoft.AspNetCore.Mvc;
using SCP.Services;

public class AdminController : Controller
{
    private readonly UserService _userService;
    private readonly AnnouncementService _announcementService;
    private readonly FeedbackService _feedbackService;

    public AdminController(UserService userService, AnnouncementService announcementService, FeedbackService feedbackService)
    {
        _userService = userService;
        _announcementService = announcementService;
        _feedbackService = feedbackService;
    }

    private bool IsAdmin() =>
        HttpContext.Session.GetString("Role")?.Equals("Admin", StringComparison.OrdinalIgnoreCase) == true;

    public async Task<IActionResult> Index()
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        var users = await _userService.GetAllAsync();
        var announcements = await _announcementService.GetAllAsync();
        var feedback = await _feedbackService.GetAllAsync();

        var vm = new AdminDashboardViewModel
        {
            Users = users,
            Announcements = announcements,
            Feedback = feedback
        };

        return View(vm);
    }

    // ---------------- USERS ----------------
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

    // ---------------- ANNOUNCEMENTS ----------------
    [HttpPost]
    public async Task<IActionResult> AddAnnouncement(string title, string content)
    {
        if (!IsAdmin()) return Unauthorized();
        await _announcementService.CreateAsync(new Announcement { Title = title, Content = content, Date = DateTime.Now });
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
