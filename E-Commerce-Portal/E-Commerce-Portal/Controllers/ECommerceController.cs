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

        public ECommerceController(ILogger<ECommerceController> logger)
        {
            _logger = logger;
        }

        // Display Login form to user
        public ActionResult Login()
        {
            _log4net.Info("User is logging in");
            return View();
        }

        // GET: ECommerceController
        public ActionResult Index()
        {
            return View();
        }

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
