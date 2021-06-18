using ProductMicroservices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMicroservices.Repository
{
    public interface IProductRepository
    {
        public List<Product> GetAllProduct();
        public  Product SearchProductByID(int Id);
        public Product SearchProductByName(string Name);

        public int AddProductRating(int Id, int rating);
       
       
    }
}
