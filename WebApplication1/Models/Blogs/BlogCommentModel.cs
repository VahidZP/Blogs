using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.Users;

namespace WebApplication1.Models.Blogs
{
    public class BlogCommentModel : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        [MaxLength(150)]
        public string Subject { get; set; }
        [Required]
        [MaxLength(150)]
        public string Massege { get; set; }
        public bool IsAdminApprove { get; set; }

        #region Relations

        public int BlogId { get; set; }
        public BlogModel Blog { get; set; }


        public int? UserId { get; set; }
        public UserModel User { get; set; }
            
        #endregion
    }
}
