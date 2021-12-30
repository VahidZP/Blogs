using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebApplication1.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using WebApplication1.Data.Contexts;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly BlogContext _context;

        public AccountController(BlogContext context)
        {
            this._context = context;
        }

        [Route("login")]
        public IActionResult login(string RetunUrl)
        {
            ViewBag.RetunUrl = RetunUrl;
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> login(string RetunUrl, AccountViewModels model)
        {
            model.Email = model.Email.ToLower();

            var user = _context.Users.SingleOrDefault(u => u.Email == model.Email && u.PassWord == model.Password);

            if (User == null)
                ModelState.AddModelError(nameof(model.Email), "Email or Password is not correct !!!");


            if (ModelState.IsValid)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.GivenName,user.FullName??""),
                    new Claim(ClaimTypes.Email,user.Email)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var property = new AuthenticationProperties()
                {
                    IsPersistent = model.IsRemember
                };
                await HttpContext.SignInAsync(principal, property);
                if (string.IsNullOrWhiteSpace(RetunUrl))
                    return this.RedirectToAction("Index", "Home", new { area = "" });

                return Redirect(RetunUrl);
            }
            return View(model);
        }

        [Route("logout")]
        public async Task<IActionResult> logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}
