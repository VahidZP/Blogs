using System.ComponentModel.DataAnnotations;

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

        #region Relations

        public int GroupId { get; set; }
        public BlogGroupModel Group { get; set; }

        #endregion
    }
}
