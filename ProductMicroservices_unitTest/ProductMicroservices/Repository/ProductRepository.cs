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
            if (_dbContext.Products.ToList() != null)
            {
                return _dbContext.Products.ToList();
            }
            return null;
        }

        public int AddProductRating(int Id, int rating)
        {
            Product P = new Product();
             P= _dbContext.Products.FirstOrDefault(x => x.Id == Id);
            if (P == null)
            {
                return 0;
            }
              P.Rating = rating;
            _dbContext.Products.Update(P);
            _dbContext.SaveChanges();
            return 1;

        }

        public Product SearchProductByID(int Id)
        {

            Product p = (Product)_dbContext.Products.Where(p=>p.Id == Id);
            return p;
        }

        public Product SearchProductByName(string name)
        {
            Product P = (Product)_dbContext.Products.Where(p => p.Name == name);
            return P;
        }

       
    }
}
