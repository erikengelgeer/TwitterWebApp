using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterWebApp.Data;
using TwitterWebApp.Models;

namespace TwitterWebApp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly TwitterDbContext _db;

        public ProfileController(TwitterDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            string userCookie = getUserCookie();
            if (userCookie == null)
            {
                return RedirectToAction("Index", "Home");
            }

            IEnumerable<User> userList = _db.User;

            foreach(User us in userList)
            {
                if(us.Username == userCookie)
                {
                    ViewBag.user = us;
                    var dob = (us.DateOfBirth.Date).ToString("dd-MM-yyyy");
                    ViewBag.dob = dob;
                }
            }

            return View();
        }

        public string getUserCookie()
        {
            return Request.Cookies["user"];
        }

        public IActionResult ChangePass()
        {
            ViewBag.cssClass = "hide";
            return View();
        }

        [HttpPost]
        public IActionResult SubmitChange(string oldPass, string Password1, string Password2)
        {
            string userCookie = getUserCookie();
            if (userCookie == null)
            {
                return RedirectToAction("Index", "Home");
            }

            string correctPass = "";

            User user = _db.User.First(x => x.Username == userCookie);
            correctPass = user.Password;


            if (correctPass != oldPass)
            {
                ViewBag.cssClass = "";
                ViewBag.msg = "Old password isn't correct.";
                return RedirectToAction("ChangePass", "Profile");
            }

            if (Password1 == Password2)
            {
                user.Password = Password1;
                _db.SaveChanges();
                return RedirectToAction("Index", "Profile");
            }

            ViewBag.cssClass = "";
            ViewBag.msg = "New passwords don't match.";
            return RedirectToAction("ChangePass", "Profile");
        }
    }
}
