using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication1.Data.Contexts;

namespace WebApplication1.ViewComponents
{
    public class LayoutHeaderViewComponent : ViewComponent
    {
        private readonly BlogContext _context;

        public LayoutHeaderViewComponent(BlogContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _context.BlogGroups.ToListAsync());
        }    
    }
}
