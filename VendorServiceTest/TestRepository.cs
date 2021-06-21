using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using VendorService.Controllers;
using VendorService.Data;
using VendorService.Models;
using VendorService.Repository;

namespace VendorServiceTest
{
    class TestRepository
    {
        List<Vendor> vendors;
        List<VendorStock> vendorStocks;
        Mock<DbSet<Vendor>> dbSetMoq;
        Mock<DbSet<VendorStock>> dbSetMoqStock;
        [SetUp]
        public void SetUp()
        {
            vendors = new List<Vendor>()
            {
                new Vendor() { Id = 201, Name="DelhiMotoShop", DeliveryCharge = 45, Rating=  5 },
                new Vendor() { Id=202, Name="HydMotoShop", DeliveryCharge= 50, Rating= 4 }
            };
            vendorStocks = new List<VendorStock>()
            {
                 new VendorStock() { Id = 1, ProductId = 101, VendorId = 201, HandInStocks= 24, ReplinshmentDate=Convert.ToDateTime(" 2021 - 02 - 02"),Vendor=new Vendor() { Id = 201, Name="DelhiMotoShop", DeliveryCharge = 45, Rating=  5 }},
                 new VendorStock() { Id = 2, ProductId = 1, VendorId = 202, HandInStocks = 24, ReplinshmentDate = Convert.ToDateTime(" 2021 - 02 - 02"),Vendor=new Vendor() { Id = 201, Name="DelhiMotoShop", DeliveryCharge = 45, Rating=  5 } }
            };
            dbSetMoq = new Mock<DbSet<Vendor>>();
            var queriableVendorList = vendors.AsQueryable();
            dbSetMoq.As<IQueryable<Vendor>>().Setup(vendor => vendor.Provider).Returns(queriableVendorList.Provider);
            dbSetMoq.As<IQueryable<Vendor>>().Setup(vendor => vendor.Expression).Returns(queriableVendorList.Expression);
            dbSetMoq.As<IQueryable<Vendor>>().Setup(vendor => vendor.ElementType).Returns(queriableVendorList.ElementType);
            dbSetMoq.As<IQueryable<Vendor>>().Setup(vendor => vendor.GetEnumerator()).Returns(queriableVendorList.GetEnumerator());
            dbSetMoqStock = new Mock<DbSet<VendorStock>>();
            var queriableVendorStockList = vendorStocks.AsQueryable();
            dbSetMoqStock.As<IQueryable<VendorStock>>().Setup(v => v.Provider).Returns(queriableVendorStockList.Provider);
            dbSetMoqStock.As<IQueryable<VendorStock>>().Setup(v => v.Expression).Returns(queriableVendorStockList.Expression);
            dbSetMoqStock.As<IQueryable<VendorStock>>().Setup(v => v.ElementType).Returns(queriableVendorStockList.ElementType);
            dbSetMoqStock.As<IQueryable<VendorStock>>().Setup(v => v.GetEnumerator()).Returns(queriableVendorStockList.GetEnumerator());

        }
        [TestCase]
        public void TestRepoGetAll()
        {

            //setup the dbset

            var moqContext = new Mock<VendorContext>();
            moqContext.Setup(v => v.Vendor).Returns(dbSetMoq.Object);
            
            VendorDetailRepo vc = new VendorDetailRepo(moqContext.Object);
            var result = vc.GetAll();
            CollectionAssert.AreEqual(vendors, result);
        }
        [TestCase]
        public void TestRepoGetVendor()
        {
            var moqContext = new Mock<VendorContext>();
            moqContext.Setup(v => v.Vendor).Returns(dbSetMoq.Object);
            moqContext.Setup(v => v.VendorStock).Returns(dbSetMoqStock.Object);
            VendorDetailRepo vc = new VendorDetailRepo(moqContext.Object);
            var result = (vc.GetVendor(101));
            Vendor test = new Vendor() { Id = 201, Name = "DelhiMotoShop", DeliveryCharge = 45, Rating = 5 };
            List<Vendor> expected = new List<Vendor>();
            expected.Add(test);

            List<Vendor> actual = new List<Vendor>();
            Vendor vi;
            foreach (Vendor v in result)
            {
                vi = new Vendor()
                {
                    Id = v.Id,
                    Name = v.Name,
                    DeliveryCharge = v.DeliveryCharge,
                    Rating = v.Rating
                };
                actual.Add(vi);
            }
            Assert.AreEqual(expected[0].Id, actual[0].Id);
        }
    }
}
