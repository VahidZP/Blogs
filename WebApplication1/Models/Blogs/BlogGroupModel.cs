using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Blogs
{
    public class BlogGroupModel : BaseEntity
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string UniqeUrl { get; set; }
        
        #region Relations

        public ICollection<BlogModel> Blogs { get; set; }

        #endregion
    }
}
