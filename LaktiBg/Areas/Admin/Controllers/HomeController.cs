using Microsoft.AspNetCore.Mvc;

namespace LaktiBg.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        public IActionResult DashBoard()
        {
            return View();
        }
    }
}
