using Microsoft.EntityFrameworkCore;
using ProductMicroservices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMicroservices.Context
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                    new Product() { Id = 1, Price = 20000, Name = "Iphone", Description = "Some example text.", Image_Name = "1.jfif", Rating = 2 },
                    new Product() { Id = 2, Price = 2000, Name = "Bracelet", Description = "Some example text.", Image_Name = "1.jfif", Rating = 3 }

                );
        }



    }
}
