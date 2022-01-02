using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApplication1.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    using WebApplication1.Extensions;
    
    public class BlogsController : Controller
    {
        private readonly BlogContext context;

        public BlogsController(BlogContext context)
        {
            this.context = context;
        }

        [Route("blog/{groupUrl?}")]
        public IActionResult Index(string groupUrl = "")
        {
            if (string.IsNullOrWhiteSpace(groupUrl))
            {
                ViewBag.GroupName = "Archive";
                return View(this.context.Blogs.Include(b => b.Group));
            }

            var group = this.context.BlogGroups.SingleOrDefault(g => g.UniqeUrl == groupUrl);
            if (group == null)
                return NotFound();

            ViewBag.GroupName = group.Name;
            return View(this.context.Blogs.Where(b => b.Group.UniqeUrl == groupUrl)
                 .Include(b => b.Group));
        }

        [Route("blog/{groupUrl}/{blogUrl}")]
        public IActionResult Show(string groupUrl, string blogUrl)
        {
            var blog = this.context.Blogs
                .Include(c => c.Group)
                .Include(c => c.User)
                .Include(c => c.BlogComments)
                .SingleOrDefault(b => b.UniqeUrl == blogUrl);
            if (blog == null)
                return NotFound();

            return View(blog.ToShowViewModel());
        }
    }
}
