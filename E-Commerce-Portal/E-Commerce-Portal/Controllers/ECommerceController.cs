using E_Commerce_Portal.Models;
using E_Commerce_Portal.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Portal.Controllers
{
    public class ECommerceController : Controller
    {
        
        private readonly IRepository repo;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ECommerceController));



        public ECommerceController(IRepository _repo)
        {

            repo = _repo;
            _log4net.Info("Logger in ECommerce");
        }


        public IActionResult Index()
        {
            string token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                _log4net.Info("User is not logged in");
                ViewBag.Message = "Please Login First";
                return RedirectToAction("Index","Login");
            }
            _log4net.Info("User is seeing products");


            return View(repo.GetProducts(token));
        }

        // Search for a product
        [HttpGet]
        public IActionResult Index(string productName)
        {
            string token = HttpContext.Session.GetString("token");

            List<Product> productsByName = repo.GetProducts(token);

            if (token == null)
            {
                _log4net.Info("User is not logged in");
                ViewBag.Message = "Please Login First";
                return RedirectToAction("Index", "Login");
            }

            /*var searchProduct = repo.GetProducts(token);*/
            _log4net.Info("User is Searching product by Id");
            /*ViewData["ProductName"] = option;*/
            if (!String.IsNullOrEmpty(productName))
            {
                /*searchProduct = products.Where(x => x.Name == productName).ToList();*/
                productsByName = repo.GetProductsByName(token, productName);
            }

            return View(productsByName);
        }



        /*        public IActionResult SearchById(int productId)
                {
                    string token = HttpContext.Session.GetString("token");
                    List<Product> productsById = new List<Product>();

                    if (token == null)
                    {
                        _log4net.Info("User is not logged in");
                        ViewBag.Message = "Please Login First";
                        return RedirectToAction("Index", "Login");
                    }

                    _log4net.Info("User is Searching product by Id");
                    ViewData["ProductId"] = productId;
                    if (productId!= 0)
                    {
                        productsById = repo.GetProductsById(token, productId);
                    }

                    return View(productsById);
                }
         */

        /*[HttpGet]
        public IActionResult Index(string productName)
        {
            string token = HttpContext.Session.GetString("token");
            List<Product> products = repo.GetProducts(token);

            if (token == null)
            {
                _log4net.Info("User is not logged in");
                ViewBag.Message = "Please Login First";
                return RedirectToAction("Index", "Login");
            }

            var searchProduct = repo.GetProducts(token);
            _log4net.Info("User is Searching product by Id");
            ViewData["ProductName"] = productName;
            if (!String.IsNullOrEmpty(productName))
            {
                searchProduct = products.Where(x => x.Name == productName).ToList();
            }

            return View(searchProduct);
        }*/
       


    }

}
