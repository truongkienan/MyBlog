using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : BaseController
    {
        [ServiceFilter(typeof(CategoryFilter))]
        [HttpGet("/{category?}/{topic?}")]
        public IActionResult Index(string category, string topic)
        {
            if (!string.IsNullOrWhiteSpace(category) && !string.IsNullOrWhiteSpace(topic))
            {
                CategoryAndTopic obj = provider.Topic.GetTopicByUrl(category, topic);
                if (obj != null)
                {
                    //return View("Topic", provider.Topic.GetTopicByUrl(category, topic));
                    return View("Topic", obj);
                }
                return Redirect("/404");
            }
            if (!string.IsNullOrWhiteSpace(category))
            {
                return View(provider.Topic.GetTopicsByCategoryWithColumn(category));
            }
            return View(provider.Topic.GetByActive());
        }

        [HttpGet("/404")]
        public IActionResult Error404()
        {
            ViewData["Title"] = "Page not found";
            return View();
        }
        [HttpGet("/500")]
        public IActionResult Error500()
        {
            ViewData["Title"] = "Internal Server Error";
            return View();
        }        
    }
}