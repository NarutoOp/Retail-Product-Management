using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationAPI.Models
{
    public class UserContext : DbContext
    {
        public UserContext() { }
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        { }

        public virtual DbSet<UserCredentials> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserCredentials>().HasData(
                     new UserCredentials { Id = 1, Username = "admin", Password = "admin" },
                     new UserCredentials { Id = 2, Username = "user", Password = "user" }

                );
        }


    }
}