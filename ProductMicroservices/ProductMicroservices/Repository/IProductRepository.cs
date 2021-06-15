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
        public IEnumerable< Product> SearchProductByID(int Id);
        public IEnumerable<Product> SearchProductByName(string Name);

        public void AddProductRating(int Id, int rating);
       
       
    }
}
