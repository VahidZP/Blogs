using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string CreateDate { get; set; }
    }
}
