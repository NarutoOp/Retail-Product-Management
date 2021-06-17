using E_Commerce_Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Portal.ViewModel
{
    public class ProductListView
    {
        public int? CartId { get; set; }
        public Vendor Vendor { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public int TotalAmount { get; set; }
        public DateTime AddedDate { get; set; }
    }
}