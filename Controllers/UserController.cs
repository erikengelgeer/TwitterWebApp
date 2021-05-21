using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterWebApp.Data;
using TwitterWebApp.Models;

namespace TwitterWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly TwitterDbContext _db;

        public UserController(TwitterDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult User(string Username)
        {
            IEnumerable<Tweet> tweetList = _db.Tweet;
            List<Tweet> userTweets = new List<Tweet>();

            string userCookie = Request.Cookies["user"];
            if (userCookie != null)
            {
                ViewBag.user = userCookie;
                ViewBag.loggedIn = true;
            }

            tweetList.Reverse();

            foreach(Tweet t in tweetList)
            {
                if(t.Username == Username)
                {
                    userTweets.Add(t);
                }
            }

            userTweets.Reverse();

            ViewBag.feed = userTweets;
            ViewBag.account = Username;

            return View();
        }
    }
}
