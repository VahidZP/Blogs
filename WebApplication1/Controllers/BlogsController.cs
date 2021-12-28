using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApplication1.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    public class BlogsController : Controller
    {
        private readonly BlogContext _context;

        public BlogsController(BlogContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Blogs.Include(b => b.Group));
        }

        [Route("blog/{groupUrl}/{blogUrl}")]
        public IActionResult Show(string groupUrl, string blogUrl)
        {
            var blog = _context.Blogs
                .Include(b => b.Group)
                .SingleOrDefault(b => b.UniqeUrl == blogUrl);
            if (blog == null)
                return NotFound();

            return View(blog);
        }
    }
}
