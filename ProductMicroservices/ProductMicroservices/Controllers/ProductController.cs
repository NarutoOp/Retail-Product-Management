using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductMicroservices.Model;
using ProductMicroservices.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductMicroservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
      
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ProductController));
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _log4net.Info("Logger initiated");
        }

        // GET api/<ProductController>
        [HttpGet("GetAll")]

        public IActionResult GetAll()
        {
            _log4net.Info("Loading all available product");
            var product = _productRepository.GetAllProduct();
            if (product == null)
            {
                _log4net.Error("There are no product in the stock");
                return NotFound("There are no product in the stock");
            }

            return new OkObjectResult(product);
        }



        // GET api/<ProductController>/5
        [HttpGet("GetById/{id}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            _log4net.Info("Searching product by productId");
            var product = _productRepository.SearchProductByID(id);
            if (product == null)
            {
                _log4net.Error("Product not found for the given productID");
                return NotFound("Product not found for the given productID");
            }
            
            return new OkObjectResult(product);
        }

        [HttpGet("GetByName/{name}")]
        [Authorize]
        public IActionResult GetByName(string name)
        {
            _log4net.Info("Searching product by productName");
            var product = _productRepository.SearchProductByName(name);
            if (product == null)
            {
                _log4net.Error("Product not found for the given productName");
                return NotFound("Product not found for the given productName");
            }
            return new OkObjectResult(product);
        }

        // POST api/<ProductController>
        [HttpPost("AddProductRating")]
        public IActionResult PostAddRating(JsonData data)
        {
            var product = _productRepository.SearchProductByID(data.Id);
            if (product == null)
            {
                _log4net.Error("Product not found for the given productID...no rating added");
                return NotFound("Product not found for the given productID...no rating added");
            }
            else
            {
                _log4net.Info("Added Rating to the Product");
                _productRepository.AddProductRating(data.Id, data.rating);
            }
            return Ok("Success");

        } 
        
    }

   
    }

