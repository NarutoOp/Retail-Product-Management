
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using ProductMicroservices.Context;
using ProductMicroservices.Controllers;
using ProductMicroservices.Model;
using ProductMicroservices.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductTesting
{
    public class Tests
    {
        List<Product> prod;
        readonly ProductController pc = new ProductController();
        int success = 1;
        int failure = 0;

        [SetUp]
        public void Setup()
        {

            prod = new List<Product>()
            {
                new Product() { Id = 1, Price = 20000, Name = "Iphone", Description = "Some example text.", Image_Name = "1.jfif", Rating = 2 },
                new Product() { Id = 2, Price = 2000, Name = "Bracelet", Description = "Some example text.", Image_Name = "1.jfif", Rating = 3 }

            };
        }



        [Test]
        public void GetAllProducts_ReturnsOkRequest()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.GetAllProduct()).Returns(prod);
            ProductController obj = new ProductController(mock.Object);
            OkObjectResult objectResult = (OkObjectResult)obj.GetAll();
            Assert.AreEqual(200, objectResult.StatusCode);
            
        }
      [Test]
        public void GetAllProducts_ReturnsNotNullList()
        {
            var mock = new Mock<IProductRepository>();
           
            ProductController obj = new ProductController(mock.Object);

            var data = obj.GetAll();

            Assert.IsNotNull(data);
        }

        [Test]
        public void SearchProductById_ValidInput_ReturnsOkRequest()
        {
            int id = 2;

            var mock = new Mock<IProductRepository>();

            mock.Setup(x => x.SearchProductByID(id)).Returns((prod.Where(x => x.Id == id)).FirstOrDefault());

            ProductController obj = new ProductController(mock.Object);

            var data = obj.Get(id);

            var res = data as ObjectResult;

            Assert.AreEqual(200, res.StatusCode);
        }


        [Test]
        public void SearchProductById_InvalidInput_ReturnsNotFoundResult()
        {
            int id = 9;

            var mock = new Mock<IProductRepository>();

            mock.Setup(x => x.SearchProductByID(id)).Returns((prod.Where(x => x.Id == id)).FirstOrDefault());
            try
            {
                ProductController obj = new ProductController(mock.Object);

                var data = obj.Get(id);
               
            }
            catch(Exception e)
            {
                Assert.AreEqual("Product not found for the given productID", e.Message);
            }
            
        }
        [Test]
        public void SearchProductByName_ValidInput_ReturnsOkRequest()
        {
            string name = "Iphone";

            var mock = new Mock<IProductRepository>();

            mock.Setup(x => x.SearchProductByName(name)).Returns((prod.Where(x => x.Name == name)).FirstOrDefault());

            ProductController obj = new ProductController(mock.Object);

            var data = obj.GetbyName(name);

            var res = data as ObjectResult;

            Assert.AreEqual(200, res.StatusCode);
        }

        [Test]
        public void SearchProductByName_InvalidInput_ReturnsNotFoundResult()
        {
            string name = "ProductName";

            var mock = new Mock<IProductRepository>();
            try
            {
                Product test = (prod.Where(x => x.Name == name)).FirstOrDefault();
                mock.Setup(x => x.SearchProductByName(name)).Returns(test);

                ProductController obj = new ProductController(mock.Object);

                var data = obj.GetbyName(name);
            }
            catch(Exception e)
            {
                Assert.AreEqual("Product not found for the given productName", e.Message);
            }            
        }
        [Test]
        public void AddProductRating_ValidInput()
        {
            int id = 1;
            int rating = 3;

            var mock = new Mock<IProductRepository>();

            mock.Setup(x => x.AddProductRating(id, rating)).Returns(success);

            ProductController obj = new ProductController(mock.Object);

            var data = obj.AddProductRating(id, rating);

            var res = data as OkResult;

            Assert.AreEqual(200, res.StatusCode);
        }

        [Test]
        public void AddProductRating_InvalidInput()
        {
            int id = 9;
            int rating = 4;

            var mock = new Mock<IProductRepository>();
            try
            {
                mock.Setup(x => x.AddProductRating(id, rating)).Returns(failure);

                ProductController obj = new ProductController(mock.Object);

                var data = obj.PostAddRating(id, rating);
            }

            catch (Exception e)
            {

                Assert.AreEqual("Product not found for the given productID...no rating added", e.Message);
            }
        }
    }
}