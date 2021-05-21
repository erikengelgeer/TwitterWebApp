using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterWebApp.Models;

namespace TwitterWebApp.Data
{
    public class TwitterDbContext : DbContext
    {
        public TwitterDbContext(DbContextOptions<TwitterDbContext> options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<Tweet> Tweet { get; set; }
    }
}
