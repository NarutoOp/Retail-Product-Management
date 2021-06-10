using E_Commerce_Portal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Portal.Controllers
{
    public class ECommerceController : Controller
    {
        private readonly ILogger<ECommerceController> _logger;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ECommerceController));

        static List<Product> products = new List<Product> {
            new Product() {Id = 1, Price = 20000, Name = "Iphone", Description="Some example text.", Image_Name="1.jfif", Rating=2 },
            new Product() {Id = 2, Price = 2000, Name = "Bracelet", Description="Some example text.", Image_Name="1.jfif", Rating=3 }
        };

        public ECommerceController(ILogger<ECommerceController> logger)
        {
            _logger = logger;
            _log4net.Info("Logger initiated");
        }


        public IActionResult Index()
        {
            return View(products);
        }

        // Search for a product
        [HttpGet]
        public IActionResult Index(string productName)
        {
            var searchProduct = products;
            _log4net.Info("User is Searching product by Id");
            ViewData["ProductName"] = productName;
            if (!String.IsNullOrEmpty(productName))
            {
                searchProduct = products.Where(x => x.Name == productName).ToList();
            }

            return View(searchProduct);
        }

        // GET: ECommerceController
        

        // GET: ECommerceController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ECommerceController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ECommerceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ECommerceController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ECommerceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ECommerceController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ECommerceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
