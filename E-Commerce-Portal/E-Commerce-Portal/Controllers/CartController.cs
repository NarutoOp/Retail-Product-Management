using E_Commerce_Portal.Models;
using E_Commerce_Portal.Services;
using E_Commerce_Portal.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            string token = HttpContext.Session.GetString("token");
            
            if (token == null)
            {
                _log4net.Info("User is not logged in");
                ViewBag.Message = "Please Login First";
                return RedirectToAction("Index", "Login");
            }
            int userid = (int)HttpContext.Session.GetInt32("userid");

            List<ProductListView> mymodel = new List<ProductListView>();
            int sum = 0;

            

            foreach(var item in repo.GetCarts(token,userid))
            {
                Product product = repo.GetProducts(token).SingleOrDefault(z => z.Id == item.ProductId);
                sum += product.Price * item.Quantity;
                mymodel.Add(
                    new ProductListView()
                    {
                        CartId = item.CartId,
                        Product = product,
                        Vendor = repo.GetVendors(token).SingleOrDefault(z => z.Id == item.VendorId),
                        Quantity = item.Quantity,
                        AddedDate = item.DeliveryDate,
                        TotalAmount = product.Price * item.Quantity
                    }
               );
            }
            _log4net.Info("User is viewing cart");
            ViewBag.GrandTotal = sum;


            return View(mymodel);
        }

        // add cart form
        public ActionResult AddToCart(int Id)
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                _log4net.Info("User is not logged in");
                ViewBag.Message = "Please Login First";
                return RedirectToAction("Index", "Login");
            }
            

            ViewData["ProductId"] = Id;
            ViewData["CustomerId"] = HttpContext.Session.GetInt32("userid");
            ViewData["DeliveryDate1"] = DateTime.Today.AddDays(2);
            ViewData["DeliveryDate2"] = DateTime.Today.AddDays(3);
            _log4net.Info("User is in add to cart page");
            return View();
        }

        // add cart form
        [HttpPost]
        public ActionResult AddToCart(Cart productCart)
        {
            string token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                _log4net.Info("User is not logged in");
                ViewBag.Message = "Please Login First";
                return RedirectToAction("Index", "Login");
            }

            var msg = repo.AddCart(token, productCart);
            

            _log4net.Info("User is adding to cart");
            if (msg == "true") { 
                
                return RedirectToAction("Index");
            }
            TempData["SuccessMessage"] = msg;
            return RedirectToAction("Index","WishList");
        }



        //edit cart
        public ActionResult EditCart(int Id)
        {
            string token = HttpContext.Session.GetString("token");
            if (HttpContext.Session.GetString("token") == null)
            {
                _log4net.Info("User is not logged in");
                ViewBag.Message = "Please Login First";
                return RedirectToAction("Index", "Login");
            }
            int userid = (int)HttpContext.Session.GetInt32("userid");

            var carts = repo.GetCarts(token, userid);
            var cart = carts.SingleOrDefault(c => c.CartId == Id);

            ViewData["ProductId"] = cart.ProductId;
            ViewData["CustomerId"] = userid;
            ViewData["zipcode"] = cart.Zipcode;
            ViewData["DeliveryDate1"] = DateTime.Today.AddDays(2);
            ViewData["DeliveryDate2"] = DateTime.Today.AddDays(3);
            _log4net.Info("User is in add to cart page");
            return View();
        }

        // add cart form
        [HttpPost]
        public ActionResult EditCart(Cart productCart)
        {
            string token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                _log4net.Info("User is not logged in");
                ViewBag.Message = "Please Login First";
                return RedirectToAction("Index", "Login");
            }

            var msg = repo.AddCart(token, productCart);
            TempData["SuccessMessage"] = msg;

            _log4net.Info("User is adding to cart");
            if (msg == "true")
                return RedirectToAction("Index");
            return RedirectToAction("Index", "WishList");
        }




        // GET: CartController/Details/5
        public ActionResult Checkout()
        {
            string token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                _log4net.Info("User is not logged in");
                ViewBag.Message = "Please Login First";
                return RedirectToAction("Index", "Login");
            }
            int id = (int)HttpContext.Session.GetInt32("userid");
            repo.Checkout(token, id);

            return View("Checkout");
        }

        public ActionResult DeleteCart(int Id)
        {
            string token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                _log4net.Info("User is not logged in");
                ViewBag.Message = "Please Login First";
                return RedirectToAction("Index", "Login");
            }
            repo.DeleteCart(token, Id);

            return RedirectToAction("Index");
        }





    }
}
