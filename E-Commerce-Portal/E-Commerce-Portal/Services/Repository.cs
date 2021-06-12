using E_Commerce_Portal.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;

namespace E_Commerce_Portal.Services
{
    public class Repository:IRepository
    {
        private readonly IConfiguration configuration;

        private readonly string productURI;

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger("log");

       /* private static List<Product> products = new List<Product> {
            new Product() {Id = 1, Price = 20000, Name = "Iphone", Description="Some example text.", Image_Name="1.jfif", Rating=2 },
            new Product() {Id = 2, Price = 2000, Name = "Bracelet", Description="Some example text.", Image_Name="1.jfif", Rating=3 }
        };*/

        private static List<Vendor> vendors = new List<Vendor> {
            new Vendor() {VendorId = 1, VendorName = "Ekart", DeliveryCharge = 50, Rating=2 },
            new Vendor() {VendorId = 2, VendorName = "Logic", DeliveryCharge = 40, Rating=3 }
        };

        private static List<Cart> carts = new List<Cart>();

        public Repository(IConfiguration _configuration)
        {
            configuration = _configuration;
            productURI = configuration.GetValue<string>("MyLinkValue:productUri");


        }

        public List<Product> GetProducts(string token)
        {
            _log4net.Info("fetching products from product microservice");

            List<Product> products = null;
            
            string bearer = String.Format("Bearer {0}",token);

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(productURI);
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Add("Authorization",bearer);

                    var response = httpClient.GetAsync("GetAll/");
                    
                        response.Wait();
                        var result = response.Result;

                        if (!result.IsSuccessStatusCode)
                        {
                            _log4net.Info("Product microservice failed");
                            
                            return null;
                        }
                        var ApiResponse = result.Content.ReadAsAsync<List<Product>>();
                        ApiResponse.Wait();
                        /*List<Product> res = JsonConvert.DeserializeObject<List<Product> >(apiResponse);*/
                        
                        products = ApiResponse.Result;
                        /*esponse.Content.*/
                        

                    
                }
                catch (Exception)
                {
                    _log4net.Error("Product Microservice is Down!!");
                }
            }

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
