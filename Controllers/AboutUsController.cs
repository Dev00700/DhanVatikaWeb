using Microsoft.AspNetCore.Mvc;

namespace DhanVatikaWeb.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
