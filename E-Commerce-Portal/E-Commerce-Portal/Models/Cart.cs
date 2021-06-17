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
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        [RegularExpression(@"^(?!00000)[0-9]{6,6}$", ErrorMessage = "Zipcode is not valid")]
        public int Zipcode { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int Quantity { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int VendorId { get; set; }

    }
}
