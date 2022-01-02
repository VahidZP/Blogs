using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Contexts;
using WebApplication1.Models.Blogs;

namespace WebApplication1.Areas.Admin.Controllers
{
    using System.IO;
    using System.Security.Claims;

    using WebApplication1.Areas.Admin.Models.Blogs;
    using WebApplication1.Extensions;
    using WebApplication1.Values;

    public class BlogModelsController : AdminController
    {
        private readonly BlogContext context;

        public BlogModelsController(BlogContext context)
        {
            this.context = context;
        }

        // GET: Admin/BlogModels
        public async Task<IActionResult> Index()
        {
            var blogContext = this.context.Blogs
                .Where(c => c.UserId == HttpContext.User.GetUserId())
                .Include(b => b.Group);
            return View(await blogContext.ToListAsync());
        }

        // GET: Admin/BlogModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogModel = await this.context.Blogs
                .Include(b => b.Group)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogModel == null)
            {
                return NotFound();
            }

            return View(blogModel);
        }

        // GET: Admin/BlogModels/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(this.context.BlogGroups, "Id", "Name");
            return View();
        }

        // POST: Admin/BlogModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCreateOrEditeModel blogViewModels)
        {
            if (ModelState.IsValid)
            {
                string imageName = null;

                if (blogViewModels.ImageFile.Length > 0)
                {
                    var path = PathTools.BlogPathServer;
                    var fileName = Guid.NewGuid().ToString("N")
                                   + Path.GetExtension(blogViewModels.ImageFile.FileName);
                    var fullPath = Path.Combine(path, fileName);

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await blogViewModels.ImageFile.CopyToAsync(stream);
                    }

                    imageName = fileName;

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                }

                var blogModels = new BlogModel()
                {
                    CreateDate = DateTime.Now.ToString("d"),
                    UserId = Convert.ToInt32(this.HttpContext.User.GetClaim(ClaimTypes.NameIdentifier)),
                    GroupId = blogViewModels.GroupId,
                    Name = blogViewModels.Name,
                    ShortDescription = blogViewModels.ShortDescription,
                    UniqeUrl = blogViewModels.UniqeUrl,
                    Description = blogViewModels.Description,
                    ImageName = imageName,
                };



                this.context.Blogs.Add(blogModels);
                await this.context.SaveChangesAsync();
                var returnUrl = "/admin/bloggroupmodels/";
                return Redirect(returnUrl);
            }

            ViewData["GroupId"] = new SelectList(this.context.BlogGroups, "Id", "Name", blogViewModels.GroupId);
            return View(blogViewModels);
        }

        // GET: Admin/BlogModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogModel = await this.context.Blogs.SingleOrDefaultAsync(c => c.UserId == HttpContext.User.GetUserId() && c.Id == id);
            if (blogModel == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(this.context.BlogGroups, "Id", "Name", blogModel.GroupId);
            return View(blogModel);
        }

        // POST: Admin/BlogModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,UniqeUrl,ShortDescription,Description,GroupId,Id,CreateDate")] BlogModel blogModel)
        {
            if (id != blogModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this.context.Update(blogModel);
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogModelExists(blogModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("/admin/bloggroupmodels/");
            }
            ViewData["GroupId"] = new SelectList(this.context.BlogGroups, "Id", "Name", blogModel.GroupId);
            return View(blogModel);
        }

        // GET: Admin/BlogModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogModel = await this.context.Blogs
                .Include(b => b.Group)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogModel == null)
            {
                return NotFound();
            }

            return View(blogModel);
        }

        // POST: Admin/BlogModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogModel = await this.context.Blogs.FindAsync(id);
            this.context.Blogs.Remove(blogModel);
            await this.context.SaveChangesAsync();
            return Redirect("/admin/bloggroupmodels/");
        }

        private bool BlogModelExists(int id)
        {
            return this.context.Blogs.Any(e => e.Id == id);
        }
    }
}
