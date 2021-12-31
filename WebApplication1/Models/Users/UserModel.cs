using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.Blogs;

namespace WebApplication1.Models.Users
{
    public class UserModel : BaseEntity
    {
        [MaxLength(150)]
        public string UserName { get; set; }
        [MaxLength(200)]
        public string Email { get; set; }
        [Required]
        [MaxLength(150)]
        public string FullName { get; set; }
        [Required]
        [MaxLength(50)]
        public string PassWord { get; set; }

        #region Relations

        public ICollection<BlogModel> Blogs { get; set; }

        #endregion
    }
}
