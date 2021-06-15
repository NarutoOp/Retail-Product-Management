using E_Commerce_Portal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Portal.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration configuration;

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(LoginController));

        public LoginController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        // Display Login form to user
        public ActionResult Index()
        {
            _log4net.Info("User is logging in");
            return View(new Login());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(Login cred)
        {
            _log4net.Info("User is logging in");
            

            string tokenURI = configuration.GetValue<string>("MyLinkValue:tokenUri");

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(cred), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(tokenURI, content))
                {

                    if (!response.IsSuccessStatusCode)
                    {
                        _log4net.Info("Login failed");
                        ViewBag.Message = "Please Enter valid credentials";
                        return View("Index");
                    }
                    _log4net.Info("Login Successful and token generated");
                    string strtoken = await response.Content.ReadAsStringAsync();



                    string userName = cred.Username;
                    int userId = cred.Id;

                    HttpContext.Session.SetString("token", strtoken);
                    HttpContext.Session.SetString("user", JsonConvert.SerializeObject(cred));
                    HttpContext.Session.SetString("owner", userName);
                    HttpContext.Session.SetInt32("userid", userId);
                }
            }
            
            return RedirectToAction("Index", "ECommerce");
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index","Login");
        }
    }
}
