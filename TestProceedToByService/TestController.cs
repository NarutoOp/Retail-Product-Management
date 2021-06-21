using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ProceedToBuy.Controllers;
using ProceedToBuy.Models;
using ProceedToBuy.Repository;
using ProceedToBuy.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestProceedToByService
{
    class TestController
    {
        List<Cart> carts;
        List<VendorWishlist> wishList;
        List<Vendor> vendors;
        [SetUp]
        public void Setup()
        {
            carts = new List<Cart>
            {
                new Cart{CartId=1,ProductId=101,CustomerId=1,DeliveryDate=Convert.ToDateTime("2021/06/21"),Quantity=5,VendorId=201,Zipcode=641008 },
                new Cart{CartId=2,ProductId=102,CustomerId=1,DeliveryDate=Convert.ToDateTime("2021/06/21"),Quantity=5,VendorId=202,Zipcode=641008 }
            };
            vendors = new List<Vendor>()
            {
                new Vendor() { Id = 201, Name="DelhiMotoShop", DeliveryCharge = 45, Rating=  5 },
                new Vendor() { Id=202, Name="HydMotoShop", DeliveryCharge= 50, Rating= 4 }
            };
            wishList = new List<VendorWishlist>
            {
                new VendorWishlist{Id=1,CustomerId=1,ProductId=101,DateAddedToWishlist=DateTime.Now,Quantity=5,VendorId=201}
            };
        }
        [Test]
        public void GetAllCarts()
        {
            var moqRepo = new Mock<IRepository<Cart>>();
            moqRepo.Setup(c => c.GetCart()).Returns(carts);
            var moqProvider = new Mock<IProvider>();
            var moqLogger = new Mock<ILogger<ProceedToBuyController>>();
            ProceedToBuyController _proceedToBuyCon = new ProceedToBuyController(moqRepo.Object, moqLogger.Object);
            var actual = _proceedToBuyCon.Get();
            CollectionAssert.AreEqual(carts, actual);
        }
        [TestCase]
        public void AddToCart_Positive()
        {
            Cart input = new Cart { CartId = 1, ProductId = 101, CustomerId = 1, DeliveryDate = Convert.ToDateTime("2021/06/21"), Quantity = 5, VendorId = 201, Zipcode = 641008 };
            var moqRepo = new Mock<IRepository<Cart>>();
            moqRepo.Setup(c => c.GetCart()).Returns(carts);
            moqRepo.Setup(c => c.AddToCart(input)).Returns(true);
            var moqProvider = new Mock<IProvider>();
            var moqLogger = new Mock<ILogger<ProceedToBuyController>>();
            ProceedToBuyController _proceedToBuyCon = new ProceedToBuyController(moqRepo.Object, moqLogger.Object);
            Assert.IsTrue(_proceedToBuyCon.Post(input));
        }
        [TestCase(1)]
        public void GetWishList(int customerId)
        {
            var moqRepo = new Mock<IRepository<Cart>>();
            moqRepo.Setup(c => c.GetCart()).Returns(carts);
            List<VendorWishlist> expected = (wishList.Where(w => w.CustomerId == customerId)).ToList();
            moqRepo.Setup(c => c.GetWishlist(customerId)).Returns(expected);
            var moqProvider = new Mock<IProvider>();
            var moqLogger = new Mock<ILogger<ProceedToBuyController>>();
            ProceedToBuyController _proceedToBuyCon = new ProceedToBuyController(moqRepo.Object, moqLogger.Object);

            Assert.AreEqual(expected, _proceedToBuyCon.GetWishList(customerId));
        }
        [TestCase(1,101)]
        public void AddToWishList(int customerId, int productId)
        {
            var moqRepo = new Mock<IRepository<Cart>>();
            moqRepo.Setup(c => c.GetCart()).Returns(carts);
            moqRepo.Setup(c => c.AddToWishList(customerId,productId)).Returns(true);
            var moqProvider = new Mock<IProvider>();
            var moqLogger = new Mock<ILogger<ProceedToBuyController>>();
            ProceedToBuyController _proceedToBuyCon = new ProceedToBuyController(moqRepo.Object, moqLogger.Object);
            Assert.AreEqual(200,((ObjectResult)_proceedToBuyCon.WishList(customerId, productId)).StatusCode);
        }
        [TestCase(1)]
        public void DeleteAll_Positive(int customerId)
        {
            var moqRepo = new Mock<IRepository<Cart>>();
            moqRepo.Setup(c => c.GetCart()).Returns(carts);
            moqRepo.Setup(c => c.DeleteCustomerCart(customerId)).Returns(true);
            var moqProvider = new Mock<IProvider>();
            var moqLogger = new Mock<ILogger<ProceedToBuyController>>();
            ProceedToBuyController _proceedToBuyCon = new ProceedToBuyController(moqRepo.Object, moqLogger.Object);
            Assert.AreEqual("Success", ((ObjectResult)_proceedToBuyCon.DeleteAll(customerId)).Value);
        }
        [TestCase(-144)]
        public void DeleteAll_Negative(int customerId)
        {
            var moqRepo = new Mock<IRepository<Cart>>();
            moqRepo.Setup(c => c.GetCart()).Returns(carts);
            moqRepo.Setup(c => c.DeleteCustomerCart(customerId)).Returns(false);
            var moqProvider = new Mock<IProvider>();
            var moqLogger = new Mock<ILogger<ProceedToBuyController>>();
            ProceedToBuyController _proceedToBuyCon = new ProceedToBuyController(moqRepo.Object, moqLogger.Object);
            Assert.AreEqual("Failed", ((ObjectResult)_proceedToBuyCon.DeleteAll(customerId)).Value);
        }
        [TestCase(1)]
        public void DeleteCart_Positive(int cartId)
        {
            var moqRepo = new Mock<IRepository<Cart>>();
            moqRepo.Setup(c => c.GetCart()).Returns(carts);
            moqRepo.Setup(c => c.DeleteCustomerCart(cartId)).Returns(true);
            var moqProvider = new Mock<IProvider>();
            var moqLogger = new Mock<ILogger<ProceedToBuyController>>();
            ProceedToBuyController _proceedToBuyCon = new ProceedToBuyController(moqRepo.Object, moqLogger.Object);
            Assert.AreEqual("Success", ((ObjectResult)_proceedToBuyCon.DeleteAll(cartId)).Value);
        }
        [TestCase(-144)]
        public void DeleteCart_Negative(int cartId)
        {
            var moqRepo = new Mock<IRepository<Cart>>();
            moqRepo.Setup(c => c.GetCart()).Returns(carts);
            moqRepo.Setup(c => c.DeleteCartById(cartId)).Returns(false);
            var moqProvider = new Mock<IProvider>();
            var moqLogger = new Mock<ILogger<ProceedToBuyController>>();
            ProceedToBuyController _proceedToBuyCon = new ProceedToBuyController(moqRepo.Object, moqLogger.Object);
            Assert.AreEqual("Failed", ((ObjectResult)_proceedToBuyCon.DeleteAll(cartId)).Value);
        }
    }
}
