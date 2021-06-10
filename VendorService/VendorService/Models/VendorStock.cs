using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VendorService.Models
{
    public class VendorStock
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int VendorId { get; set; }
        public int HandInStocks { get; set; }
        public DateTime ReplinshmentDate { get; set; }
       public Vendor Vendor { get; set; }
    }
}
