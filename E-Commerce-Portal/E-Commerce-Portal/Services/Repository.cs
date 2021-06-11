using E_Commerce_Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Portal.Services
{
    public class Repository:IRepository
    {
        private static List<Product> products = new List<Product> {
            new Product() {Id = 1, Price = 20000, Name = "Iphone", Description="Some example text.", Image_Name="1.jfif", Rating=2 },
            new Product() {Id = 2, Price = 2000, Name = "Bracelet", Description="Some example text.", Image_Name="1.jfif", Rating=3 }
        };

        private static List<Vendor> vendors = new List<Vendor> {
            new Vendor() {VendorId = 1, VendorName = "Ekart", DeliveryCharge = 50, Rating=2 },
            new Vendor() {VendorId = 2, VendorName = "Logic", DeliveryCharge = 40, Rating=3 }
        };

        private static List<Cart> carts = new List<Cart>();

        public List<Product> GetProducts()
        {
            return products;
        }

        public List<Cart> GetCarts()
        {
            return carts;
        }

        public List<Vendor> GetVendors()
        {
            return vendors;
        }

        public void AddCart(Cart cart)
        {
            carts.Add(cart);
        }

    }
}
