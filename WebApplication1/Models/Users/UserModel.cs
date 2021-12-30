using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

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
    }
}
