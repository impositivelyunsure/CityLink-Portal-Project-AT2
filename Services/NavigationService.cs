using System.Xml.Linq;

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
            var items = doc.Descendants("Item")
                           .Select(x => new MenuItem(
                               x.Attribute("title")?.Value ?? "",
                               x.Attribute("url")?.Value ?? "/"
                           ));
            foreach (var item in items) yield return item;
        }
    }
}
