using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp.Controllers;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    public class AuthController : BaseController
    {
        [HttpGet("/auth/login")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost("/auth/login")]
        public async Task<IActionResult> Login(LoginModel obj)
        {
            if (ModelState.IsValid)
            {
                ResponseLogin member = provider.Member.Login(obj);
                if (member != null)
                {
                    List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, member.Username),
                    new Claim(ClaimTypes.NameIdentifier, member.MemberId.ToString()),
                    new Claim(ClaimTypes.Email, member.Email),
                    new Claim("Fullname", member.Fullname)
                };

                    IEnumerable<Role> roles = provider.Role.GetRolesByMember(member.MemberId);
                    if (roles != null)
                    {
                        foreach (Role item in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, item.RoleName));
                        }
                    }

                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    AuthenticationProperties properties = new AuthenticationProperties
                    {
                        IsPersistent = obj.Remember
                    };

                    await HttpContext.SignInAsync(principal, properties);

                    return Redirect("/dashboard");
                }
                TempData["msg"] = "Login Failed";

                return View(obj);
            }
            return View(obj);
        }
        [HttpGet("/auth/register")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost("/auth/register")]
        public IActionResult Register(Member obj)
        {
            int ret = -1;
            if (ModelState.IsValid)
            {
                ret = provider.Member.Add(obj);
                if (ret > 0)
                {
                    return Redirect("/auth/login");
                }
            }

            string[] err = { "Username exists", "Add Member Failed" };

            ModelState.AddModelError(string.Empty, err[ret + 1]);
            return View(obj);
        }
        [HttpGet("/auth/accessdenied")]
        public IActionResult AccessDenied()
        {
            ViewData["Title"] = "Unauthorized";
            return View();
        }

        [HttpPost("/auth/logout"), Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        [HttpGet("/auth/change"), Authorize]
        public IActionResult Change()
        {
            return View();
        }

        [HttpPost("/auth/change"), Authorize]
        public async Task<IActionResult> Change(ChangeModel obj)
        {
            if (ModelState.IsValid)
            {
                obj.Username = User.Identity.Name;
                int ret = provider.Member.Change(obj);
                if (ret > 0)
                {
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    return Redirect("/auth/login");
                }
                TempData["msg"] = "Old Password Failed";
                return View(obj);
            }
            return View(obj);
        }

    }
}
