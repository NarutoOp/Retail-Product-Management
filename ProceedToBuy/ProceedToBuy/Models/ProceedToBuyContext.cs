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
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<VendorWishlist> VendorWishlists { get; set; }
        
    }
}
