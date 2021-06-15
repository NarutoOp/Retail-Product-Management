using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProceedToBuy.Models
{
    public class ProceedToBuyContext:DbContext
    {
        public ProceedToBuyContext(DbContextOptions<ProceedToBuyContext> option):base(option)
        {

        }
<<<<<<< Updated upstream
        public DbSet<Vendor> Vendors { get; set; }
=======
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>().HasData(
                  new Cart() { CartId =1 , CustomerId=201, ProductId= 101, Zipcode = 641008 ,Quantity= 5,DeliveryDate=Convert.ToDateTime("2016 - 05 - 15 00:00:00.000") }
              );
        }
>>>>>>> Stashed changes
        public DbSet<Cart> Carts { get; set; }
        public DbSet<VendorWishlist> VendorWishlists { get; set; }
        
    }
}
