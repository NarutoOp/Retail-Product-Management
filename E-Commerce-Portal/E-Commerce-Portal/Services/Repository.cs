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
using Newtonsoft.Json;
using System.Text;

namespace E_Commerce_Portal.Services
{
    public class Repository:IRepository
    {
        private readonly IConfiguration configuration;

        private readonly string productURI;

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger("log");

        private readonly static List<Vendor> vendors = new List<Vendor> {
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
                        
                        products = ApiResponse.Result;                        

                    
                }
                catch (Exception)
                {
                    _log4net.Error("Product Microservice is Down!!");
                }
            }

            return products;
        }

        public List<Product> GetProductsByName(string token,string name)
        {
            _log4net.Info("fetching products by name from product microservice");

            List<Product> productsByName = null;

            string bearer = String.Format("Bearer {0}", token);

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(productURI);
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Add("Authorization", bearer);

                    var response = httpClient.GetAsync("GetByName/"+name);

                    response.Wait();
                    var result = response.Result;

                    if (!result.IsSuccessStatusCode)
                    {
                        _log4net.Info("Product microservice failed");

                        return null;
                    }
                    var ApiResponse = result.Content.ReadAsAsync<List<Product>>();
                    ApiResponse.Wait();

                    productsByName = ApiResponse.Result;
                }
                catch (Exception)
                {
                    _log4net.Error("Product Microservice is Down!!");
                }
            }

            return productsByName;
        }

        public List<Product> GetProductsById(string token,int id)
        {
            _log4net.Info("fetching products by ID from product microservice");

            List<Product> productsById = null;

            string bearer = String.Format("Bearer {0}", token);

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(productURI);
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Add("Authorization", bearer);

                    var response = httpClient.GetAsync("GetById/"+id);

                    response.Wait();
                    var result = response.Result;

                    if (!result.IsSuccessStatusCode)
                    {
                        _log4net.Info("Product microservice failed");

                        return null;
                    }
                    var ApiResponse = result.Content.ReadAsAsync<List<Product>>();
                    ApiResponse.Wait();

                    productsById = ApiResponse.Result;

                }
                catch (Exception)
                {
                    _log4net.Error("Product Microservice is Down!!");
                }
            }
            return productsById;
        }

        // add rating

        public Boolean AddRating(string token,int id, int rating)
        {
            _log4net.Info("Adding rating");


            string bearer = String.Format("Bearer {0}", token);

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(productURI);
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Add("Authorization", bearer);

                    var pocoObject = new
                    {
                        Id = id,
                        rating = rating
                    };


                    var response = httpClient.PostAsJsonAsync("AddProductRating",pocoObject);

                    response.Wait();
                    var result = response.Result;

                    if (!result.IsSuccessStatusCode)
                    {
                        _log4net.Info("Product microservice failed");
                        return false;
                    }

                }
                catch (Exception)
                {
                    _log4net.Error("Product Microservice is Down!!");
                }
                return true;
            }
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
