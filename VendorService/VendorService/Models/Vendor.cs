using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendorService.Models
{
    public class Vendor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DeliveryCharge { get; set; }
        public int Rating { get; set; }

    }
}
