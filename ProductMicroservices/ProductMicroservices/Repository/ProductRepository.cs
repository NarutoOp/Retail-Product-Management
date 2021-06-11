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

        public void AddProductRating(int Id, int rating)
        {
            Product P = new Product();
             P= _dbContext.Products.FirstOrDefault(x => x.Id == Id);
           
              P.Rating = rating;
            _dbContext.Products.Update(P);
            _dbContext.SaveChanges();
          

        }

        public Product SearchProductByID(int Id)
        {

            Product p = _dbContext.Products.Find(Id);
            return p;
        }

        public Product SearchProductByName(string name)
        {
            Product P = _dbContext.Products.FirstOrDefault(x => x.Name == name);
            return P;
        }

       
    }
}
