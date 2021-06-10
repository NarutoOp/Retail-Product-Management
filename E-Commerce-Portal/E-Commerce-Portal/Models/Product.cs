using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Portal.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public string Image_Name { get; set; }
        public int Rating { get; set; }
    }
}
