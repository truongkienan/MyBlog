using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers
{
    [Area("dashboard"), Authorize(Roles = "admin")]
    public class MemberController : BaseController
    {
        [HttpGet("/dashboard/member")]
        [ServiceFilter(typeof(CategoryFilter))]
        public IActionResult Index()
        {
            return View(provider.Member.GetMembers());
        }
        public IActionResult Roles(Guid id)
        {
            ViewBag.member = provider.Member.GetMemberById(id);
            return View(provider.Role.GetRolesCheckedByMember(id));
        }
        [HttpPost]
        public IActionResult Roles(MemberInRole obj)
        {
            return Json(provider.MemberInRole.Save(obj));
        }
    }
}
