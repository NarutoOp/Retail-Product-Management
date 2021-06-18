using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProceedToBuy.Models
{
    public class ProceedToBuyContext:DbContext
    {
        public ProceedToBuyContext()
        {

        }
        public ProceedToBuyContext(DbContextOptions<ProceedToBuyContext> option):base(option)
        {

        }

        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<VendorWishlist> VendorWishlists { get; set; }
        
    }
}
