using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApp.Controllers;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers
{
    [Area("dashboard"), Authorize]
    [ServiceFilter(typeof(CategoryFilter))]
    public class TopicController : BaseController
    {
        [Route("dashboard/topic/{category?}")]

        public IActionResult Index(string category)
        {
            return View(provider.Topic.GetTopicsByCategory(category));
        }

        [HttpPost("/dashboard/topic/update")]
        public int Update(short id, short vl)
        {
            int ret = provider.Topic.UpdatePostion(id, vl);
            return ret;
        }

        [HttpPost("/dashboard/topic/upload")]
        public JsonResult Upload(IFormFile upload)
        {
            string url= $"/images/{Helper.Upload(upload)}";
            return Json(new { url = url });
        }

        [HttpGet("/dashboard/topic/create/{category}")]
        public IActionResult Create(string category)
        {
            Topic obj = new Topic
            {
                CategoryName = provider.Category.GetCategoryByUrl(category).CategoryName,
                CategoryUrl=category
            };

            return View(obj);
        }

        [HttpPost("/dashboard/topic/create/{category}")]
        public IActionResult Create(Topic obj)
        {
            if (ModelState.IsValid)
            {
                int ret = provider.Topic.Add(obj);
                string[] msg = { "Added faild", "Added faild", "Added success" };
                return Redirect(ret, msg, obj);
            }
            return View(obj);
        }

        [HttpGet("/dashboard/topic/edit/{id?}")]
        public IActionResult Edit(short id)
        {
            return View(provider.Topic.GetTopicById(id));
        }

        [HttpPost("/dashboard/topic/edit/{id?}")]
        public IActionResult Edit(Topic obj)
        {
            if (ModelState.IsValid)
            {
                int ret = provider.Topic.Edit(obj);
                string[] err = { "Update failed", "Update failed", "Update success" };
                ModelState.AddModelError(string.Empty, err[ret + 1]);
                return View(obj);
            }
            return View(obj);
        }

        IActionResult Redirect(int ret, string[] msg, Topic obj)
        {
            TempData["msg"] = msg[ret+1];
            if (ret > 0)
            {
                return Redirect($"/dashboard/topic/{obj.CategoryUrl}");
            }
            return View(obj);
        }
    }
}
