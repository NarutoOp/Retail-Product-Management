using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductMicroservices.Contants;
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
    [Authorize]
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
                _log4net.Error(Constant.ProductNotFound);
                return NotFound(Constant.ProductNotFound);
            }

            return new OkObjectResult(product);
        }



        // GET api/<ProductController>/5
        [HttpGet("GetById/{id}")]
        public IActionResult Get(int id)
        {
            _log4net.Info("Searching product by productId");
            var product = _productRepository.SearchProductByID(id);
            if (product == null)
            {
                _log4net.Error(Constant.ProductNotFoundById);
                return NotFound(Constant.ProductNotFoundById);
            }
            
            return new OkObjectResult(product);
        }

        [HttpGet("GetByName/{name}")]
        public IActionResult GetbyName(string name)
        {
            _log4net.Info("Searching product by productName");
            var product = _productRepository.SearchProductByName(name);
            if (product == null)
            {
                _log4net.Error(Constant.ProductNotFoundByName);
                return NotFound(Constant.ProductNotFoundByName);
            }
            return new OkObjectResult(product);
        }

        // POST api/<ProductController>
        [HttpPost("AddProductRating")]
        public IActionResult PostAddRating(JsonData data)
        {
            var product = _productRepository.SearchProductByID(data.id);
            if (product == null)
            {
                _log4net.Error(Constant.ProductRatingNotAdded);
                return NotFound(Constant.ProductRatingNotAdded);
            }
            else
            {
                _log4net.Info(Constant.ProductRating);
                _productRepository.AddProductRating(data.id, data.rating);
            }
            return Ok("Success");

        } 
        
    }

   
    }

