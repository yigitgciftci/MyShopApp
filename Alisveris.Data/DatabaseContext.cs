using Alisveris.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisveris.Data
{
    public class DatabaseContext : DbContext
    {
        private IConfiguration _configuration;
        public DatabaseContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;

            if (Database.GetPendingMigrations().Count() > 0)
            {
                Database.Migrate();
            }

            if (Database.CanConnect())
            {
                if (Admins.Any() == false)
                {
                    //string adminUid = _configuration.GetValue<string>("Authentication:AdminUsername");
                    //string adminPwd = _configuration.GetValue<string>("Authentication:AdminPassword");

                    Admin admin = new Admin
                    {
                        Username = "admin",
                        Password = "123456",
                        Role = "admin"
                    };

                    Admins.Add(admin);
                    SaveChanges();
                }
            }            
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<ShoppingCard> ShoppingCards { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }


    }
}
