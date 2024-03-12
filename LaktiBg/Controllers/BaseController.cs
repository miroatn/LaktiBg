using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LaktiBg.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {

    }
}
