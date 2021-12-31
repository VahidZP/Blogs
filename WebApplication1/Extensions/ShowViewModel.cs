namespace WebApplication1.Extensions
{
    using WebApplication1.Models.Blogs;
    using WebApplication1.ViewModels.Blog;

    public static class ShowViewModel
    {
        public static BlogShowViewModel ToShowViewModel(this BlogModel blog)
        {
            return new BlogShowViewModel
            {
                GroupName = blog.Group.Name,
                GroupUniqeUrl = blog.Group.UniqeUrl,
                UserName = blog.User.UserName,
                Name = blog.Name,
                UniqeUrl = blog.UniqeUrl,
                ShortDescription = blog.ShortDescription,
                Description = blog.Description,
                ImageName = blog.ImageName,
                Comments = blog.BlogComments.ToString(),
                CreateDate = blog.CreateDate
            };
        }
    }
}
