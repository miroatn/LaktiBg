using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static LaktiBg.Core.Constants.RoleConstants;

namespace LaktiBg.Areas.Admin.Controllers
{
    [Area("Admin")] 
    [Authorize(Roles = AdminRole)]
    public class AdminBaseController : Controller
    {

    }
}
