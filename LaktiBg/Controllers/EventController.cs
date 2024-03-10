using Microsoft.AspNetCore.Mvc;

namespace LaktiBg.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
