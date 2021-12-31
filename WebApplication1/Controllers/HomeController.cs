using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using WebApplication1.Data.Contexts;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using WebApplication1.Extensions;
    using WebApplication1.ViewModels.Blog;

    public class HomeController : Controller
    {
        private readonly BlogContext _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, BlogContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View(this._context.Blogs.Include(c => c.Group));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
