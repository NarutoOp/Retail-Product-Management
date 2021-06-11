﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Portal.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public string Zipcode { get; set; }
        public DateTime DeliveryDate { get; set; }
        public Vendor vendor { get; set; }
        public int Quantity { get; set; }

    }
}