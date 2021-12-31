using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ViewModels.Blog
{
    using System.Collections;
    using System.IO;

    using WebApplication1.Values;

    public class BlogShowViewModel
    {
        public string Name { get; set; }
        public string GroupName { get; set; }
        public string UserName { get; set; }
        public string CreateDate { get; set; }
        public string GroupUniqeUrl { get; set; }
        public string UniqeUrl { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }

        public string ImageFullPath => !string.IsNullOrWhiteSpace(ImageName) ?
                                       Path.Combine(PathTools.BlogPath, ImageName) :
                                       PathTools.PathDefault;
        public string Comments { get; set; }
    }
}
