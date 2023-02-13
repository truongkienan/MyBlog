using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApp.Controllers;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers
{
    [Area("dashboard"), Authorize(Roles = "admin")]
    public class RoleController : BaseController
    {
        [HttpGet("/dashboard/role")]
        [ServiceFilter(typeof(CategoryFilter))]
        public IActionResult Index()
        {
            return View(provider.Role.GetRoles());
        }

        [HttpPost("/dashboard/role/create")]
        public JsonResult Create(Role obj)
        {
            if (ModelState.IsValid)
            {
                return Json(provider.Role.Add(obj));
            }
            return Json(obj);
        }
    }
}
