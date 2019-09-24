using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practice.EFCore.AngelSix.Context
{
    // Test Relationship
    // Test Fields
    // Test overriding methods


    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> ops):base(ops)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {


            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Phone> Phones { get; set; }

    }

    // Note:
    // If you only add only one Navigation Property to User
    // it will automatically create PhoenId foreign key in Phone entity

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }



        public int PhoneId { get; set; }
        public Phone Phone { get; set; }

    }

    public class Phone
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; }
    }



}
