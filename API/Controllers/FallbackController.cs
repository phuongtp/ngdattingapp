using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    // This support the Client...
    public class FallbackController: Controller
    {
        public ActionResult Index()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"), "text/HTML");
        }
    }
}