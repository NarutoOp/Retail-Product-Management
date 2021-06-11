using E_Commerce_Portal.Models;
using E_Commerce_Portal.Services;
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
        private readonly IRepository repo;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ECommerceController));



        public ECommerceController(ILogger<ECommerceController> logger, IRepository _repo)
        {
            _logger = logger;
            repo = _repo;
            _log4net.Info("Logger in ECommerce");
        }


        public IActionResult Index()
        {
            _log4net.Info("User is seeing products");
            return View(repo.GetProducts());
        }

        // Search for a product
        [HttpGet]
        public IActionResult Index(string productName)
        {
            var searchProduct = repo.GetProducts();
            _log4net.Info("User is Searching product by Id");
            ViewData["ProductName"] = productName;
            if (!String.IsNullOrEmpty(productName))
            {
                searchProduct = repo.GetProducts().Where(x => x.Name == productName).ToList();
            }

            return View(searchProduct);
        }


        // GET: ECommerceController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
    }

}
