using E_Commerce_Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace E_Commerce_Portal.Services
{
    public interface IRepository
    {
        public List<Product> GetProducts();

        public List<Cart> GetCarts();

        public List<Vendor> GetVendors();

        public void AddCart(Cart cart);
    }
}
