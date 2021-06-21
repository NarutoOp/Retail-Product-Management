using E_Commerce_Portal.Models;
using E_Commerce_Portal.Services;
using E_Commerce_Portal.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Portal.Controllers
{
    public class WishListController : Controller
    {
        private readonly IRepository repo;

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ECommerceController));
        public WishListController(IRepository _repo)
        {
            repo = _repo;
            _log4net.Info("Logger in WishList");

        }
        // GET: WishListController
        public ActionResult Index()
        {
            ViewBag.SuccessMsg = "";
            if(TempData["SuccessMessage"] != null)
                ViewBag.SuccessMsg = TempData["SuccessMessage"].ToString();

            string token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                _log4net.Info("User is not logged in");
                ViewBag.Message = "Please Login First";
                return RedirectToAction("Index", "Login");
            }
            _log4net.Info("User is seeing WishLists");

            List<ProductListView> mymodel = new List<ProductListView>();

            foreach (var item in repo.GetWishlists(token, Convert.ToInt32(HttpContext.Session.GetInt32("userid"))))
            {
                Product product = repo.GetProducts(token).SingleOrDefault(z => z.Id == item.ProductId);
                int quantity = item.Quantity;
                mymodel.Add(
                    new ProductListView()
                    {

                        Product = product,
                        Vendor = repo.GetVendors(token).SingleOrDefault(z => z.Id == item.VendorId),
                        Quantity = quantity,
                        AddedDate = item.DateAddedToWishlist,
                        TotalAmount = product.Price * quantity
                    }
               );

            }

            return View(mymodel);

        }
    }
}