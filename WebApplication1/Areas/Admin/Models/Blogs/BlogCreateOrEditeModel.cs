using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Areas.Admin.Models.Blogs
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class BlogCreateOrEditeModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string UniqeUrl { get; set; }
        [Required]
        [MaxLength(150)]
        public string ShortDescription { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string ImageName { get; set; }

        public IFormFile ImageFile{ get; set; }

        public int GroupId { get; set; }

        public int UserId { get; set; }

        public string CreatDate { get; set; }
    }
}