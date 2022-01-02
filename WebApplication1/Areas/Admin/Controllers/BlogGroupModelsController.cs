using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data.Contexts;
using WebApplication1.Models.Blogs;

namespace WebApplication1.Areas.Admin.Controllers
{
    
    public class BlogGroupModelsController : AdminController
    {
        private readonly BlogContext context;

        public BlogGroupModelsController(BlogContext context)
        {
            this.context = context;
        }

        // GET: Admin/BlogGroupModels
        
        public async Task<IActionResult> Index()
        {
            return View(await this.context.BlogGroups.ToListAsync());
        }

        // GET: Admin/BlogGroupModels/Details/5
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogGroupModel = await this.context.BlogGroups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogGroupModel == null)
            {
                return NotFound();
            }

            return View(blogGroupModel);
        }

        // GET: Admin/BlogGroupModels/Create
        
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/BlogGroupModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,UniqeUrl,Id,CreateDate")] BlogGroupModel blogGroupModel)
        {
            if (ModelState.IsValid)
            {
                blogGroupModel.CreateDate=DateTime.Now.ToString("d");
                this.context.Add(blogGroupModel);
                await this.context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blogGroupModel);
        }

        // GET: Admin/BlogGroupModels/Edit/5
        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogGroupModel = await this.context.BlogGroups.FindAsync(id);
            if (blogGroupModel == null)
            {
                return NotFound();
            }
            return View(blogGroupModel);
        }

        // POST: Admin/BlogGroupModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,UniqeUrl,Id,CreateDate")] BlogGroupModel blogGroupModel)
        {
            if (id != blogGroupModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this.context.Update(blogGroupModel);
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogGroupModelExists(blogGroupModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(blogGroupModel);
        }

        // GET: Admin/BlogGroupModels/Delete/5
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogGroupModel = await this.context.BlogGroups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogGroupModel == null)
            {
                return NotFound();
            }

            return View(blogGroupModel);
        }

        // POST: Admin/BlogGroupModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogGroupModel = await this.context.BlogGroups.FindAsync(id);
            this.context.BlogGroups.Remove(blogGroupModel);
            await this.context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogGroupModelExists(int id)
        {
            return this.context.BlogGroups.Any(e => e.Id == id);
        }
    }
}
