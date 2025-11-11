using System.Xml.Linq;
using Microsoft.AspNetCore.Http;

namespace SCP.Services
{
    public class NavigationService
    {
        private readonly IWebHostEnvironment _env;
        public record MenuItem(string Title, string Url);

        public NavigationService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IEnumerable<MenuItem> GetMenu()
        {
            var path = Path.Combine(_env.ContentRootPath, "App_Data", "navigation.xml");
            if (!File.Exists(path)) yield break;

            var doc = XDocument.Load(path);
            var items = doc.Descendants("Item");

            foreach (var item in items)
            {
                var title = item.Attribute("title")?.Value ?? "";
                var url = item.Attribute("url")?.Value ?? "/";

                // Skip profile menu item if user is not logged in
                // if (title == "My Profile" && string.IsNullOrEmpty(_httpContextAccessor.HttpContext?.Session.GetString("Username")))
                // {
                //     continue;
                // }

                yield return new MenuItem(title, url);
            }
        }
    }
}
