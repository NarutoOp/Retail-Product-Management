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
            List<Product> products = new List<Product>();

            string token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                _log4net.Info("User is not logged in");
                ViewBag.Message = "Please Login First";
                return RedirectToAction("Index", "Login");
            }
            _log4net.Info("User is seeing products");

            try {
                products = repo.GetProducts(token);
            }
            catch (Exception)
            {
                _log4net.Info("Service is down");
            }
            

            return View(products);
        }

        // Search for a product
        [HttpGet]
        public IActionResult Search(int option, string productNameOrId)
        {
            string token = HttpContext.Session.GetString("token");

            List<Product> products = repo.GetProducts(token);

            if (token == null)
            {
                _log4net.Info("User is not logged in");
                ViewBag.Message = "Please Login First";
                return RedirectToAction("Index", "Login");
            }

            if (option == 2)
            {
                _log4net.Info("User is Searching product by Id");

                if (Int32.Parse(productNameOrId) > 0)
                {
                    products = repo.GetProductsById(token, Int32.Parse(productNameOrId));
                }

                return View("Details", products);
            }

            _log4net.Info("User is Searching product by Name");

            if (!String.IsNullOrEmpty(productNameOrId))
            {
                products = repo.GetProductsByName(token, productNameOrId);
            }

            return View("Details", products);
        }

        [HttpPost]
        public IActionResult AddRating(int productId, int rate)
        {
            string token = HttpContext.Session.GetString("token");

            if (token == null)
            {
                _log4net.Info("User is not logged in");
                ViewBag.Message = "Please Login First";
                return RedirectToAction("Index", "Login");
            }
            ViewBag.Id = productId;
            ViewBag.rating = rate;
            var val = repo.AddRating(token, productId, rate);

            if (val)
                return RedirectToAction("Index");
            else
                return BadRequest("Not able to add rating");
        }




    }

}