using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TwitterWebApp.Data;
using TwitterWebApp.Models;

namespace TwitterWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly TwitterDbContext _db;

        public HomeController(TwitterDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Tweet> feed = Feed();

            string userCookie = getUserCookie();
            if(userCookie != null)
            {
                ViewBag.user = userCookie;
                ViewBag.loggedIn = true;
            }

            if(TempData.ContainsKey("TweetMessage"))
            {
                ViewBag.tweetMsg = TempData["TweetMessage"].ToString();
                ViewBag.tweetClass = "";
            }

            feed.Reverse();
            ViewBag.tweetClass = "hide";
            ViewBag.feed = feed;
            return View();
        }

        public string getUserCookie()
        {
            return Request.Cookies["user"];
        }

        public IActionResult Logout()
        {
            string userCookie = getUserCookie();
            if(userCookie != null)
            {
                Response.Cookies.Delete("user");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Tweet(string content)
        {
            Tweet tweet = new Tweet();
            tweet.Content = content;
            tweet.TimeStamp = DateTime.Now;

            IEnumerable<User> userList = _db.User;

            string loggedUser = getUserCookie();

            foreach(User us in userList)
            {
                if(us.Username == loggedUser)
                {
                    tweet.UserId = us.Id;
                    tweet.Username = us.Username;
                }
            }

            _db.Tweet.Add(tweet);

            if (!ModelState.IsValid)
            {
                TempData["TweetMessage"] = "You tweet could not be added.";
            }

            _db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public List<Tweet> Feed()
        {
            List<Tweet> tweets = new List<Tweet>();

            foreach (Tweet t in _db.Tweet)
            {
                tweets.Add(t);
            }
            return tweets;
        }
    }
}
