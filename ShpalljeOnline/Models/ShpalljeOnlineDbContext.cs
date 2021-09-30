using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ShpalljeOnline.Migrations;

namespace ShpalljeOnline.Models
{
    public class ShpalljeOnlineDbContext : DbContext
    {

        public ShpalljeOnlineDbContext() :base("ShpalljeOnlineConnectionString")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ShpalljeOnlineDbContext, Configuration>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Info> Infos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}