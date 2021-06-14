using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProceedToBuy.Models;
using ProceedToBuy.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace ProceedToBuy.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class ProceedToBuyController : ControllerBase
    {
        IRepository<Cart> _repository;

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ProceedToBuyController));


        public ProceedToBuyController(IRepository<Cart> repository)
        {
            _repository = repository;

        }
        // GET: api/<ProceedToBuyController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            throw new NotImplementedException();
            //return new string[] { "value1", "value2" };
        }

        // GET api/<ProceedToBuyController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            throw new NotImplementedException();
            
        }

        // POST api/<ProceedToBuyController>
        [HttpPost]
        public Cart Post([FromBody] Cart _cart)
        {
            _log4net.Info("Posting Cart");
            return _repository.AddToCart(_cart);
            


        }
        [Route("WishList")]
        [HttpPost]
        public IActionResult WishList(int customerId,int productId)
        {
            _log4net.Info("Posting WishList");
            _repository.AddToWishList(customerId,productId);
            return Ok("Success");



        }

        // PUT api/<ProceedToBuyController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<ProceedToBuyController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
