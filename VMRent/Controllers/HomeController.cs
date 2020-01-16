using Microsoft.AspNetCore.Mvc;

namespace VMRent.Controllers
{
    public class HomeController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}