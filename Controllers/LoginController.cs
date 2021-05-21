using Microsoft.AspNetCore.Mvc;
using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterWebApp.Data;
using TwitterWebApp.Models;
using System.Security.Cryptography;
using System.Text;

namespace TwitterWebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly TwitterDbContext _db;

        public LoginController(TwitterDbContext db)
        {
            _db = db;
        }

        public string getSalt()
        {
            const string salt = "x301Xdx{2PO";
            return salt;
        }

        public IActionResult Index(string msg, string uname)
        {
            if(msg != null)
            {
                ViewBag.cssClass = "";
                ViewBag.msg = msg;
                ViewBag.uname = uname;
                return View();
            }

            ViewBag.cssClass = "hide";
            ViewBag.msg = "";
            ViewBag.uname = "";
            return View();
        }

        public IActionResult Register()
        {
            ViewBag.err = "hide";
            ViewBag.passErr = "hide";
            return View();
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        string passHash(string pass)
        {
            SHA512 hashSvc = SHA512.Create();

            byte[] hash = hashSvc.ComputeHash(Encoding.UTF8.GetBytes(pass));

            string result = BitConverter.ToString(hash).Replace("-", String.Empty);
            return result.ToLower();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            var msg = "";
            var passMsg = "";
            var hashedPass = "";
            string salt = getSalt();

            if (ModelState.IsValid)
            {
                if (!CheckUsernameAvailability(user))
                {
                    msg = "Username is already taken";
                    ViewBag.msg = msg;
                    ViewBag.err = "";
                    return View();
                }

                if (!CheckPasswordLength(user))
                {
                    passMsg = "Password is too long! Can't be longer then 20 characters.";
                    ViewBag.passMsg = passMsg;
                    ViewBag.passErr = "";
                    return View();
                }

                hashedPass = passHash(salt + user.Password);
                user.Password = hashedPass;
                _db.User.Add(user);

                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            msg = "Unvalid data";
            ViewBag.msg = msg;
            ViewBag.err = "";
            return View();
        }

        public bool CheckUsernameAvailability(User user)
        {
            IEnumerable<User> userList = _db.User;

            foreach (User usr in userList)
            {
                if (usr.Username == user.Username)
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckPasswordLength(User user)
        {
            if(user.Password.Count() > 20)
            {
                return false;
            }
            return true;
        }

        [HttpPost]
        public IActionResult Check(string Username, string Password)
        {
            IEnumerable<User> userList = _db.User;
            bool userExists = false;
            bool correctPass = false;

            if (!String.IsNullOrEmpty(Username))
            {
                if(checkIfUserExists(Username, userList))
                {
                    userExists = true;
                    correctPass = checkCorrectPassword(Password, Username, userList);
                }
            }

            if(userExists && correctPass)
            {
                Response.Cookies.Append("user", Username);
                return RedirectToAction("Index", "Home");
            }

            if(!userExists)
            {
                return RedirectToAction("Index", new { msg = "User doesn't exist.", uname = Username});
            }

            if(!correctPass)
            {
                return RedirectToAction("Index", new { msg = "Password is incorrect.", uname = Username });
            }

            return RedirectToAction("Index", "Login");
        }

        public bool checkIfUserExists(string uname, IEnumerable list)
        {
            foreach(User user in list)
            {
                if(user.Username == uname)
                {
                    return true;
                }
            }

            return false;
        }

        public bool checkCorrectPassword(string pass, string uname, IEnumerable list)
        {
            string correctPass = "";

            foreach(User usr in list)
            {
                if(usr.Username == uname)
                {
                    correctPass = usr.Password;
                }
            }

            string inputPass = passHash(getSalt() + pass);

            if(inputPass == correctPass)
            {
                return true;
            }

            return false;
        }
    }
}
