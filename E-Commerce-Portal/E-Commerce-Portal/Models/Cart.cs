using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Portal.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Zipcode { get; set; }
        public int Quantity { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int VendorId { get; set; }

    }
}
