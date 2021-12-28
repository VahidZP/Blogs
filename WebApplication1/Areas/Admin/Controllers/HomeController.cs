using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        [Route("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
