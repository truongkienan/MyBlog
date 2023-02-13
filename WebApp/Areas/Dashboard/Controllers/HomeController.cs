using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers
{
    [Area("dashboard"), Authorize]
    public class HomeController : BaseController
    {
        [HttpGet("/dashboard")]
        [ServiceFilter(typeof(CategoryFilter))]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("/dashboard/statistictopics")]
        public JsonResult GetStatisticTopics()
        {
            return Json(provider.Statistic.GetStatisticTopics());
        }
    }
}
