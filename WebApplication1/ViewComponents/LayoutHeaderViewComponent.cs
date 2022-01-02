using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication1.Data.Contexts;

namespace WebApplication1.ViewComponents
{
    public class LayoutHeaderViewComponent : ViewComponent
    {
        private readonly BlogContext context;

        public LayoutHeaderViewComponent(BlogContext context)
        {
            this.context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await this.context.BlogGroups.ToListAsync());
        }    
    }
}
