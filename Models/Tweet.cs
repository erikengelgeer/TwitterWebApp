using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterWebApp.Models
{
    public class Tweet
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        [DataType(DataType.Date)]
        public DateTime TimeStamp { get; set; }
        public int Likes { get; set; }
        public int Favorites { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        public string Username { get; set; }
        public List<Tweet> Tweets { get; }
    }
}
