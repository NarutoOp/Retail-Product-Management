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
        private readonly string proceedToBuyUri;
        private readonly string vendorUri;

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger("log");
        
        
        public Repository(IConfiguration _configuration)
        {
            configuration = _configuration;
            productURI = configuration.GetValue<string>("MyLinkValue:ProductUri");
            proceedToBuyUri = configuration.GetValue<string>("MyLinkValue:ProceedToBuyUri");
            vendorUri = configuration.GetValue<string>("MyLinkValue:VendorUri");

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

        public List<Product> GetProductsByName(string token, string name)
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

                    var response = httpClient.GetAsync("GetByName/" + name);

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

        public List<Product> GetProductsById(string token, int id)
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

                    var response = httpClient.GetAsync("GetById/" + id);

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

        public Boolean AddRating(string token, int id, int rating)
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


                    var response = httpClient.PostAsJsonAsync("AddProductRating", pocoObject);

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



        public List<Cart> GetCarts(string token,int id)
        {
            string bearer = String.Format("Bearer {0}", token);
            List<Cart> carts = null;
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(proceedToBuyUri);
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Add("Authorization", bearer);

                    var response = httpClient.GetAsync("ProceedToBuy/"+id);

                    response.Wait();
                    var result = response.Result;
                    _log4net.Info(result);
                    if (!result.IsSuccessStatusCode)
                    {
                        _log4net.Info("ProceedToBuy microservice failed");

                        return null;
                    }
                    var ApiResponse = result.Content.ReadAsAsync<List<Cart>>();
                    ApiResponse.Wait();

                    carts = ApiResponse.Result;
                }
                catch (Exception)
                {
                    _log4net.Error("Vendor Microservice is Down!!");
                }
            }
            return carts;
        }

        public List<Vendor> GetVendors(string token)
        {
            string bearer = String.Format("Bearer {0}", token);
            List<Vendor> vendors = null;
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(vendorUri);
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Add("Authorization", bearer);

                    var response = httpClient.GetAsync("Vendor");

                    response.Wait();
                    var result = response.Result;

                    if (!result.IsSuccessStatusCode)
                    {
                        _log4net.Info("Product microservice failed");

                        return null;
                    }
                    var ApiResponse = result.Content.ReadAsAsync<List<Vendor>>();
                    ApiResponse.Wait();


                    vendors = ApiResponse.Result;
                }
                catch (Exception)
                {
                    _log4net.Error("Vendor Microservice is Down!!");
                }
            }
            return vendors;
        }

        public string AddCart(string token,Cart cart)
        {
            string bearer = String.Format("Bearer {0}", token);
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(proceedToBuyUri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", bearer);

                    var responseTask = client.PostAsJsonAsync<Cart>("ProceedToBuy", cart);

                    responseTask.Wait();

                    var result = responseTask.Result;

                    var content = result.Content.ReadAsStringAsync();

                    content.Wait();

                    return content.Result;
                    
                    
                }
            }
            catch(Exception)
            {
                _log4net.Error("Proceed To Buy cant post!!");
            }
            return null;
        }


        // Wishlist

        public List<VendorWishlist> GetWishlists(string token, int id)
        {
            string bearer = String.Format("Bearer {0}", token);
            List<VendorWishlist> wishList = null;
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(proceedToBuyUri);
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Add("Authorization", bearer);

                    var response = httpClient.GetAsync("ProceedToBuy/GetWishList/" + id);

                    response.Wait();
                    var result = response.Result;

                    if (!result.IsSuccessStatusCode)
                    {
                        _log4net.Info("Proceed to buy microservice in wishlist failed");

                        return null;
                    }
                    var ApiResponse = result.Content.ReadAsAsync<List<VendorWishlist>>();
                    ApiResponse.Wait();


                    wishList = ApiResponse.Result;

                }
                catch (Exception)
                {
                    _log4net.Error("Vendor Microservice is Down!!");
                }

            }
            return wishList;
        }

        public void Checkout(string token, int CustomerId)
        {
            string bearer = String.Format("Bearer {0}", token);
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(proceedToBuyUri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", bearer);
                    var responseTask = client.GetAsync("ProceedToBuy/DeleteAll/"+CustomerId);
                    responseTask.Wait();
                    var result = responseTask.Result;

                }
            }
            catch (Exception)
            {
                _log4net.Error("Clear all cart and wishlist item !!");
            }
        }

        public void DeleteCart(string token, int CartId)
        {
            string bearer = String.Format("Bearer {0}", token);
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(proceedToBuyUri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", bearer);
                    var responseTask = client.DeleteAsync("ProceedToBuy/DeleteCart/" + CartId);
                    responseTask.Wait();
                    var result = responseTask.Result;
                }
            }
            catch (Exception)
            {
                _log4net.Error("Delete Cart !!");
            }
        }

    }
}
