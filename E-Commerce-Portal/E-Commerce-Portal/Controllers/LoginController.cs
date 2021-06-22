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
        public ActionResult Index(Login cred)
        {
            _log4net.Info("User is logging in");
            

            string tokenURI = configuration.GetValue<string>("MyLinkValue:tokenUri");

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(cred), Encoding.UTF8, "application/json");

                using (var response = httpClient.PostAsync(tokenURI, content))
                {
                    response.Wait();
                    var result = response.Result;

                    if (!result.IsSuccessStatusCode)
                    {
                        _log4net.Info("Login failed");

                        if ((int)result.StatusCode == 401)
                        {
                            _log4net.Info("User is banned");

                            ViewBag.Message = "User is banned. You had entered wrong credentials multiple time";
                        }
                        else
                        {

                            _log4net.Info("Entered wrong credentials");
                            ViewBag.Message = "Please Enter valid credentials entering wrong credentials multiple time will ban you";
                        }

                        return View("Index");
                    }
                    _log4net.Info("Login Successful and token generated");

                    var ApiResponse = result.Content.ReadAsAsync<UserToken>();
                    ApiResponse.Wait();
                    var res = ApiResponse.Result;


                    string userName = res.Username;
                    int userId = res.Id;
                    string strtoken = res.Token;
                    string Address = res.Address;

                    HttpContext.Session.SetString("token", strtoken);
                    HttpContext.Session.SetString("username", userName);
                    HttpContext.Session.SetInt32("userid", userId);
                    HttpContext.Session.SetString("address", Address);
                }
            }
            
            return RedirectToAction("Index", "ECommerce");
        }

        public ActionResult Register()
        {
            _log4net.Info("User is registering in");
            return View(new Login());
        }

        [HttpPost]
        public ActionResult Register(Login reg)
        {
            _log4net.Info("User is registering in");

            string tokenURI = configuration.GetValue<string>("MyLinkValue:tokenUri")+"/register";

            using (var httpClient = new HttpClient())
            {
                
                StringContent content = new StringContent(JsonConvert.SerializeObject(reg), Encoding.UTF8, "application/json");

                var response = httpClient.PostAsync(tokenURI,content);
                response.Wait();
                var result = response.Result;

                if (!result.IsSuccessStatusCode)
                {
                    _log4net.Info("Registration failed");
                    ViewBag.Message = "Username Already exists !!";
                    return View("Register");
                }
                _log4net.Info("Registration Successful");
            }
            return RedirectToAction("Index", "Login");
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index","Login");
        }
    }
}
