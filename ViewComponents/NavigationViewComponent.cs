using Microsoft.AspNetCore.Mvc;
using SCP.Services;

namespace SCP.ViewComponents
{
    public class NavigationViewComponent : ViewComponent
    {
        private readonly NavigationService _nav;
        public NavigationViewComponent(NavigationService nav) => _nav = nav;

        public IViewComponentResult Invoke()
        {
            var items = _nav.GetMenu();
            return View(items);
        }
    }
}
