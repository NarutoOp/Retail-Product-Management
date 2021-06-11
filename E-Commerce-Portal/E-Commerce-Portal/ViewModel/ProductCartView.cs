using E_Commerce_Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Portal.ViewModel
{
    public class ProductCartView
    {
        public Product Products { get; set; }
        public Cart Carts { get; set; }
    }
}
