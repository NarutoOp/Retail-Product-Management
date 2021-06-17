using Microsoft.AspNetCore.WebUtilities;
using ProductMicroservices.Context;
using ProductMicroservices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;


namespace ProductMicroservices.Repository
{
    public class ProductRepository: IProductRepository
    {
       
        private readonly ProductContext _dbContext;

        public ProductRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Product> GetAllProduct()
        {
            return _dbContext.Products.ToList();
        }

        public void AddProductRating(int Id, int rating)
        {
            Product P = new Product();
             P= _dbContext.Products.FirstOrDefault(x => x.Id == Id);
           
              P.Rating = rating;
            _dbContext.Products.Update(P);
            _dbContext.SaveChanges();
          

        }

        public IEnumerable<Product> SearchProductByID(int Id)
        {

            IEnumerable<Product> p = _dbContext.Products.Where(p=>p.Id == Id);
            return p;
        }

        public IEnumerable<Product> SearchProductByName(string name)
        {
            IEnumerable<Product> P = _dbContext.Products.Where(p => p.Name.Contains(name));
            return P;
        }

       
    }
}
