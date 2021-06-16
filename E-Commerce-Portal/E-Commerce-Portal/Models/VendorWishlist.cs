using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Portal.Models
{
    public class VendorWishlist
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateAddedToWishlist { get; set; }
        public int CustomerId { get; set; }
    }
}