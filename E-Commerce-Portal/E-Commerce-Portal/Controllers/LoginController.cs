using E_Commerce_Portal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Portal.Controllers
{
    public class LoginController : Controller
    {

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(LoginController));

        // Display Login form to user
        public ActionResult Index()
        {
            _log4net.Info("User is logging in");
            return View(new Login());
        }

        [HttpPost]
        public ActionResult Index(Login login)
        {
            _log4net.Info("User is logging in");
            return RedirectToAction("Index", "ECommerce");
        }
    }
}
