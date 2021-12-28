using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.Blogs;

namespace WebApplication1.Data.Contexts
{
    using WebApplication1.Models.Users;

    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        {
        }

        public DbSet<BlogGroupModel> BlogGroups { get; set; }

        public DbSet<BlogModel> Blogs { get; set; }

        public DbSet<UserModel> Users { get; set; }
    }
}
