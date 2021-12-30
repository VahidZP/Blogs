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

        [Route("blog/{groupUrl?}")]
        public IActionResult Index(string groupUrl = "")
         {
            if (string.IsNullOrWhiteSpace(groupUrl))
            {
                ViewBag.GroupName = "Archive";
                return View(_context.Blogs.Include(b => b.Group));
            }

            var group = this._context.BlogGroups.SingleOrDefault(g => g.UniqeUrl == groupUrl);
            if (group == null)
                return NotFound();

            ViewBag.GroupName = group.Name;
            return View(_context.Blogs.Where(b => b.Group.UniqeUrl == groupUrl)
                 .Include(b => b.Group));
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
