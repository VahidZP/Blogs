using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using WebApplication1.Models.Users;

namespace WebApplication1.Models.Blogs
{
    public class BlogModel : BaseEntity
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string UniqeUrl { get; set; }
        [Required]
        [MaxLength(200)]
        public string ShortDescription { get; set; }
        [Required]
        public string Description { get; set; }

        public string ImageName { get; set; }

        #region Relations

        public int UserId { get; set; }
        public UserModel User { get; set; }

        public int GroupId { get; set; }
        public BlogGroupModel Group { get; set; }

        public ICollection<BlogCommentModel> BlogComments { get; set; }

        #endregion
    }
}
