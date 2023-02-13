using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApp.Controllers;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers
{
    [Area("dashboard"), Authorize]
    public class CategoryController : BaseController
    {

        [Route("dashboard/category")]
        [ServiceFilter(typeof(CategoryFilter))]
        public IActionResult Index()
        {
            ViewBag.active = "category";
            return View(provider.Category.GetAll());
        }
        [HttpPost("/dashboard/category/update")]
        public int Update(byte id, byte vl)
        {
            int ret = provider.Category.UpdatePostion(id, vl);
            return ret;
        }

        [HttpGet("/dashboard/category/edit/{id?}")]
        [ServiceFilter(typeof(CategoryFilter))]
        public IActionResult Edit(byte id)
        {
            return View(provider.Category.GetCategoryById(id));
        }

        [HttpPost("/dashboard/category/edit/{id?}")]
        [ServiceFilter(typeof(CategoryFilter))]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                int ret = provider.Category.Edit(obj);
                string[] err = { "Update failed", "Update failed", "Update success" };
                TempData["msg"] = err[ret+1];
                return View(obj);
            }
            return View(obj);
        }

        [HttpGet("/dashboard/category/create")]
        [ServiceFilter(typeof(CategoryFilter))]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("/dashboard/category/create")]
        [ServiceFilter(typeof(CategoryFilter))]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                int ret = provider.Category.Add(obj);
                string[] msg = { "Added faild", "Added faild", "Added success" };
                return Redirect(ret, msg, obj);
            }
            return View(obj);
        }
        [HttpPost("/dashboard/category/ShowDefault/{id}")]
        public int ShowDefault(byte id)
        {
            int ret=provider.Category.ShowDefault(id);
            return ret;
        }
        IActionResult Redirect(int ret, string[] msg, Category obj)
        {
            TempData["msg"] = msg[ret + 1];
            if (ret > 0)
            {
                return Redirect($"/dashboard/category");
            }
            return View(obj);
        }
    }
}
