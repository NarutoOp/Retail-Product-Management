using E_Commerce_Portal.Models;
using E_Commerce_Portal.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Portal.Controllers
{
    public class CartController : Controller
    {
        static List<Product> products = new List<Product> {
            new Product() {Id = 1, Price = 20000, Name = "Iphone", Description="Some example text.", Image_Name="1.jfif", Rating=2 },
            new Product() {Id = 2, Price = 2000, Name = "Bracelet", Description="Some example text.", Image_Name="1.jfif", Rating=3 }
        };

        static List<Vendor> vendors = new List<Vendor> {
            new Vendor() {VendorId = 1, VendorName = "Ekart", DeliveryCharge = 50, Rating=2 },
            new Vendor() {VendorId = 2, VendorName = "Logic", DeliveryCharge = 40, Rating=3 }
        };

        static List<Cart> cart = new List<Cart>();

        // GET: CartController
        // cart page
        public ActionResult Index()
        {
            List<ProductCartView> mymodel = new List<ProductCartView>();
            List<Product> carProducts = new List<Product>();
            foreach(var item in cart)
            {
                mymodel.Add(
                    new ProductCartView()
                    {
                    
                        Products = products.SingleOrDefault(z => z.Id == item.ProductId),
                        Carts = item
                    }
               );
            }
            
            return View(mymodel);
        }

        // add cart form
        public ActionResult AddToCart(int Id)
        {
            ViewData["ProductId"] = Id;
            return View();
        }

        // add cart form
        [HttpPost]
        public ActionResult AddToCart(Cart productCart)
        {
            var product = cart.SingleOrDefault(x => x.ProductId == productCart.ProductId);
            if (product == null)
            {
                productCart.Quantity = 1;
                cart.Add(productCart);
            }
            else
                product.Quantity += 1;
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
