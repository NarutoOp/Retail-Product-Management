
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using VendorService.Controllers;
using VendorService.Models;
using VendorService.Repository;
namespace VendorServiceTest
{
    public class TestController
    {
        List<Vendor> vendors;
        List<VendorStock> vendorStocks;
        Mock<IVendorDetailRepo<Vendor>> moqRepo;
        Mock<ILogger<VendorController>> moqLogger;
        [SetUp]
        public void Setup()
        {
            moqRepo = new Mock<IVendorDetailRepo<Vendor>>();


            moqLogger = new Mock<ILogger<VendorController>>();
            vendors = new List<Vendor>()
            {
                new Vendor() { Id = 201, Name="DelhiMotoShop", DeliveryCharge = 45, Rating=  5 },
                new Vendor() { Id=202, Name="HydMotoShop", DeliveryCharge= 50, Rating= 4 }
            };
            vendorStocks = new List<VendorStock>()
            {
                 new VendorStock() { Id = 1, ProductId = 101, VendorId = 201, HandInStocks= 24, ReplinshmentDate= Convert.ToDateTime(" 2021 - 02 - 02"),Vendor=new Vendor() { Id = 201, Name="DelhiMotoShop", DeliveryCharge = 45, Rating=  5 }},
                 new VendorStock() { Id = 2, ProductId = 1, VendorId = 202, HandInStocks = 24, ReplinshmentDate = Convert.ToDateTime(" 2021 - 02 - 02"),Vendor=new Vendor() { Id = 201, Name="DelhiMotoShop", DeliveryCharge = 45, Rating=  5 } }
            };
        }

        [Test]
        public void TestControllerGetAll()
        {
            moqRepo.Setup(v => v.GetAll()).Returns(vendors);
            VendorController vc = new VendorController(moqRepo.Object, moqLogger.Object);
            var result = vc.Get();
            CollectionAssert.AreEqual(vendors, result);
        }
        [Test]
        public void TestControllerGetAll_ReturnsNotNull()
        {
            moqRepo.Setup(v => v.GetAll()).Returns(vendors);
            VendorController vc = new VendorController(moqRepo.Object, moqLogger.Object);
            var result = vc.Get();
            Assert.IsNotNull(result);
        }
        [TestCase(1)]
        [TestCase(101)]
        
        public void TestControllerGetByProductId_ReturnPositive(int id)
        {
                moqRepo.Setup(v => v.GetVendor(id)).Returns(vendorStocks.Where(v => v.ProductId == id && v.HandInStocks > 0).Select(ve => ve.Vendor));
                VendorController vc = new VendorController(moqRepo.Object, moqLogger.Object);
                var actual = vc.Get(id);
                ObjectResult okObjectResult = (ObjectResult)actual;
                Assert.AreEqual(200, okObjectResult.StatusCode); 
        }
        [TestCase(115)]
        public void TestControllerGetByProductId_ReturnNegative(int id)
        {
            moqRepo.Setup(v => v.GetVendor(id)).Returns(vendorStocks.Where(v => v.ProductId == id && v.HandInStocks > 0).Select(ve => ve.Vendor));
            VendorController vc = new VendorController(moqRepo.Object, moqLogger.Object);
            var actual = vc.Get(id);
            ObjectResult okObjectResult = (ObjectResult)actual;
            Assert.AreEqual(404, okObjectResult.StatusCode);
        }
    }
}