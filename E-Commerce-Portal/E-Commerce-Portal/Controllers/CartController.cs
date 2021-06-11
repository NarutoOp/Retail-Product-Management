using E_Commerce_Portal.Models;
using E_Commerce_Portal.Services;
using E_Commerce_Portal.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Portal.Controllers
{
    public class CartController : Controller
    {
        private readonly IRepository repo;
        private readonly ILogger<CartController> _logger;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ECommerceController));


        public CartController(ILogger<CartController> logger, IRepository _repo)
        {
            
            repo = _repo;
            _logger = logger;
            _log4net.Info("Logger in Cart");
        }

 
        // cart page
        
        public ActionResult Index()
        {
            List<ProductCartView> mymodel = new List<ProductCartView>();
            
            foreach(var item in repo.GetCarts())
            {
                mymodel.Add(
                    new ProductCartView()
                    {
                    
                        Products = repo.GetProducts().SingleOrDefault(z => z.Id == item.ProductId),
                        Carts = item
                    }
               );
            }
            _log4net.Info("User is viewing cart");

            return View(mymodel);
        }

        // add cart form
        public ActionResult AddToCart(int Id)
        {
            ViewData["ProductId"] = Id;
            _log4net.Info("User is in add to cart page");
            return View();
        }

        // add cart form
        [HttpPost]
        public ActionResult AddToCart(Cart productCart)
        {
            var product = repo.GetCarts().SingleOrDefault(x => x.ProductId == productCart.ProductId);
            if (product == null)
            {
                productCart.Quantity = 1;
                repo.AddCart(productCart);
            }
            else
                product.Quantity += 1;

            _log4net.Info("User is adding to cart");
            return RedirectToAction("Index");
        }
        





        // GET: CartController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CartController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CartController/Create
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

        // GET: CartController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CartController/Edit/5
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

        // GET: CartController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CartController/Delete/5
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
