using E_Commerce_Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace E_Commerce_Portal.Services
{
    public interface IRepository
    {
        public List<Product> GetProducts(string token);

        public List<Product> GetProductsByName(string token, string name);
<<<<<<< Updated upstream
=======

        public List<Product> GetProductsById(string token, int id);

        public Boolean AddRating(string token, int Id, int rating);

        public List<Cart> GetCarts(string token, int id);
>>>>>>> Stashed changes

        public List<Product> GetProductsById(string token, int id);

        public Boolean AddRating(string token, int Id, int rating);

        public List<Cart> GetCarts();

        public List<Vendor> GetVendors();

        public void AddCart(string token,Cart cart);
    }
}
