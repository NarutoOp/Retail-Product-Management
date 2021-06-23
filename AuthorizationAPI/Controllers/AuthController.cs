using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthorizationAPI.Constants;
using AuthorizationAPI.Models;
using AuthorizationAPI.Provider;
using AuthorizationAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthorizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration _config;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuthController));
        private readonly IUserRepo repo;

        

        public AuthController(IConfiguration config,IUserRepo _repo)
        {
            _config = config;
            repo = _repo;
        }

       
        /// Post method for Login


        [HttpPost]
        public IActionResult Login([FromBody] UserCredentials login)
        {
            AuthRepo auth_repo = new AuthRepo(_config,repo);
            UserToken userToken = new UserToken();
            _log4net.Info("Login initiated!");
            IActionResult response = Unauthorized();

            var user = auth_repo.AuthenticateUser(login);
            if (user == null)
            {
                var count = repo.IncrementCount(login);
 
                return NotFound(Constant.UserNotFound);
                
            }
            else
            {
                if (user.BanTime != null) {
                    var diff = (user.BanTime - DateTime.Now).TotalHours;
                    if (diff > 0)
                    {
                        return Unauthorized(Constant.UserBanned);
                    }
                }
                var tokenString = auth_repo.GenerateJSONWebToken(user);


                userToken.Id = user.Id;
                userToken.Username = user.Username;
                userToken.Token = tokenString;
                userToken.Address = user.Address;

            }

            return new OkObjectResult(userToken);
        }

        [Route("Register")]
        [HttpPost]
        public IActionResult Register([FromBody] UserCredentials reg)
        {

            _log4net.Info("Login initiated!");

            if(repo.RegisterUserCred(reg))
                return Ok();
            return NotFound();
        }





    }
}
