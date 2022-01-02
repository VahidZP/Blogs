using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebApplication1.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using WebApplication1.Data.Contexts;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly BlogContext context;

        public AccountController(BlogContext context)
        {
            this.context = context;
        }

        [Route("login")]
        public IActionResult Login(string retunUrl)
        {
            ViewBag.RetunUrl = retunUrl;
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string retunUrl, AccountViewModels model)
        {
            model.Email = model.Email.ToLower();

            var user = this.context.Users.SingleOrDefault(u => u.Email == model.Email && u.PassWord == model.Password);

            if (User == null)
                ModelState.AddModelError(nameof(model.Email), "Email or Password is not correct !!!");


            if (ModelState.IsValid)
            {
                if (user != null)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.GivenName, user.FullName ?? ""),
                        new Claim(ClaimTypes.Email, user.Email)
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var property = new AuthenticationProperties()
                    {
                        IsPersistent = model.IsRemember
                    };
                    await HttpContext.SignInAsync(principal, property);
                }

                if (string.IsNullOrWhiteSpace(retunUrl))
                    return this.RedirectToAction("Index", "Home", new { area = "" });

                return Redirect(retunUrl);
            }
            return View(model);
        }

        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}
