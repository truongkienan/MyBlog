using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class AuthorController : Controller
    {
        [HttpGet("/an.truong")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
