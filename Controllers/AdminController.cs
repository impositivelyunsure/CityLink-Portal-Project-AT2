using Microsoft.AspNetCore.Mvc;

public class AdminController : Controller
{
    private bool IsAdmin() =>
        HttpContext.Session.GetString("Role")?.Equals("Admin", StringComparison.OrdinalIgnoreCase) == true;

    public IActionResult Index()
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Account");
        }

        ViewBag.Username = HttpContext.Session.GetString("Username");
        return View();
    }
}
