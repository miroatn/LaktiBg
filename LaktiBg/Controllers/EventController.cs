using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LaktiBg.Controllers
{
    public class EventController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
