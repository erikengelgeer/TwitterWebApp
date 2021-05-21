using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterWebApp.Controllers
{
    public class TweetController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
