using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendorService.Models
{
    public class VendorStock
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int VendorId { get; set; }
        public int HandInStocks { get; set; }
        public string ReplinshmentDate { get; set; }
       public Vendor Vendor { get; set; }
    }
}
