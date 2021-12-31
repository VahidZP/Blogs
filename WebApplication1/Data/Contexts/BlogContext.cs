using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.Blogs;

namespace WebApplication1.Data.Contexts
{
    using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

    using WebApplication1.Models.Users;

    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        {
        }

        public DbSet<BlogGroupModel> BlogGroups { get; set; }

        public DbSet<BlogModel> Blogs { get; set; }

        public DbSet<BlogCommentModel> BlogComments { get; set; }

        public DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().HasData(new UserModel
            {
                Id = 1,
                UserName = "Vahid",
                FullName = "Vahid Zahedi",
                Email = "Admin@site.com",
                PassWord = "1234",
                CreateDate = "2021/12/21"
            });

            modelBuilder
                .Entity<BlogModel>()
                .HasOne(e => e.User)
                .WithMany(e => e.Blogs)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);

        }
    }
}
