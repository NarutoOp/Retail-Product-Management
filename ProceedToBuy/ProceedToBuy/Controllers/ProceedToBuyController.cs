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
    [Authorize]
    [ApiController]
    public class ProceedToBuyController : ControllerBase
    {
        IRepository<Cart> _repository;
        private readonly ILogger<ProceedToBuyController> _logger;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ProceedToBuyController));


        public ProceedToBuyController(IRepository<Cart> repository, ILogger<ProceedToBuyController> logger)
        {
            _repository = repository;
            _logger = logger;

        }
        // GET: api/<ProceedToBuyController>
        [HttpGet]
        public IEnumerable<Cart> Get()
        {
            return _repository.GetCart();
        }

        // GET api/<ProceedToBuyController>/5
        [HttpGet("{id}")]
        public List<Cart> Get(int id)
        {
            List<Cart> cart = _repository.GetCart();
            return cart.Where(x => x.CustomerId == id).ToList();
            
        }

        // POST api/<ProceedToBuyController>
        [HttpPost]
        public Boolean Post([FromBody] Cart _cart)
        {
            _log4net.Info("Adding to Cart");
            return _repository.AddToCart(_cart);

        }

        [HttpGet("GetWishList/{id}")]
        public IEnumerable<VendorWishlist> GetWishList(int id)
        {
            return _repository.GetWishlist(id);
        }

        [Route("WishList")]
        [HttpPost]
        public IActionResult WishList(int customerId,int productId)
        {
            _log4net.Info("Addding to WishList");
            _repository.AddToWishList(customerId,productId);
            return Ok("Success");
        }

        [Route("DeleteAll/{id}")]
        [HttpGet]
        public IActionResult DeleteAll (int id)
        {
            _log4net.Info("Checking out");
            if(_repository.DeleteCustomerCart(id))
                return Ok("Success");
            return BadRequest("Failed");
        }

        [Route("DeleteCart/{id}")]
        [HttpDelete]
        public IActionResult DeleteByCartId(int id)
        {
            _log4net.Info("Posting WishList");
            if (_repository.DeleteCartById(id))
                return Ok("Success");
            return BadRequest("Failed");
        }
    }
}
