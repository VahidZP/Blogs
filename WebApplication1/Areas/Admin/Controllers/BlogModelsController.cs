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
    public class BlogModelsController : AdminController
    {
        private readonly BlogContext _context;

        public BlogModelsController(BlogContext context)
        {
            _context = context;
        }

        // GET: Admin/BlogModels
        public async Task<IActionResult> Index()
        {
            var blogContext = _context.Blogs.Include(b => b.Group);
            return View(await blogContext.ToListAsync());
        }

        // GET: Admin/BlogModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogModel = await _context.Blogs
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
            ViewData["GroupId"] = new SelectList(_context.BlogGroups, "Id", "Name");
            return View();
        }

        // POST: Admin/BlogModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,UniqeUrl,ShortDescription,Description,GroupId,Id,CreateDate")] BlogModel blogModel)
        {
            if (ModelState.IsValid)
            {
                blogModel.CreateDate= DateTime.Now.ToString("d");
                _context.Add(blogModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.BlogGroups, "Id", "Name", blogModel.GroupId);
            return View(blogModel);
        }

        // GET: Admin/BlogModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogModel = await _context.Blogs.FindAsync(id);
            if (blogModel == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.BlogGroups, "Id", "Name", blogModel.GroupId);
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
                    _context.Update(blogModel);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.BlogGroups, "Id", "Name", blogModel.GroupId);
            return View(blogModel);
        }

        // GET: Admin/BlogModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogModel = await _context.Blogs
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
            var blogModel = await _context.Blogs.FindAsync(id);
            _context.Blogs.Remove(blogModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogModelExists(int id)
        {
            return _context.Blogs.Any(e => e.Id == id);
        }
    }
}
