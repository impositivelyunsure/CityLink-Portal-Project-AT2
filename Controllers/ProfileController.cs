using Microsoft.AspNetCore.Mvc;

namespace SCP.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            var user = new UserProfile
            {
                FullName = "Jane Doe",
                Email = "jane@example.com",
                MemberSince = new DateTime(2022, 5, 1)
            };

            return View(user);
        }
    }
}
