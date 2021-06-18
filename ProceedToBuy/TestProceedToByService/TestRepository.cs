using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using ProceedToBuy.Models;
using ProceedToBuy.Repository;
using ProceedToBuy.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestProceedToByService
{
    public class TestRepository
    {
        List<Cart> carts;
        List<VendorWishlist> wishList;
        Mock<DbSet<Cart>> dbSetMoq;
        Mock<DbSet<VendorWishlist>> dbWishListSetMoq;
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
            dbSetMoq = new Mock<DbSet<Cart>>();
            var queriableCartList = carts.AsQueryable();
            dbSetMoq.As<IQueryable<Cart>>().Setup(v => v.Provider).Returns(queriableCartList.Provider);
            dbSetMoq.As<IQueryable<Cart>>().Setup(v => v.Expression).Returns(queriableCartList.Expression);
            dbSetMoq.As<IQueryable<Cart>>().Setup(v => v.ElementType).Returns(queriableCartList.ElementType);
            dbSetMoq.As<IQueryable<Cart>>().Setup(v => v.GetEnumerator()).Returns(queriableCartList.GetEnumerator());
            dbWishListSetMoq = new Mock<DbSet<VendorWishlist>>();
            var queriableWishList = wishList.AsQueryable();
            dbWishListSetMoq.As<IQueryable<VendorWishlist>>().Setup(v => v.Provider).Returns(queriableWishList.Provider);
            dbWishListSetMoq.As<IQueryable<VendorWishlist>>().Setup(v => v.Expression).Returns(queriableWishList.Expression);
            dbWishListSetMoq.As<IQueryable<VendorWishlist>>().Setup(v => v.ElementType).Returns(queriableWishList.ElementType);
            dbWishListSetMoq.As<IQueryable<VendorWishlist>>().Setup(v => v.GetEnumerator()).Returns(queriableWishList.GetEnumerator());
            
        }

        [Test]
        public void GetAllCarts()
        {
            var moqContext = new Mock<ProceedToBuyContext>();
            moqContext.Setup(c => c.Carts).Returns(dbSetMoq.Object);
            var moqProvider = new Mock<IProvider>();            
            Repo repo = new Repo(moqContext.Object,moqProvider.Object);
            var actual = repo.GetCart();
            CollectionAssert.AreEqual(carts,actual);
        }
        [Test]
        public void GetAllCarts_ReturnsNotNull()
        {
            var moqContext = new Mock<ProceedToBuyContext>();
            moqContext.Setup(c => c.Carts).Returns(dbSetMoq.Object);
            var moqProvider = new Mock<IProvider>();
            Repo repo = new Repo(moqContext.Object, moqProvider.Object);
            var actual = repo.GetCart();
            Assert.IsNotNull(actual);
        }
        [TestCase]
        public void AddToCart_Positive()
        {
            Cart input = new Cart { CartId = 1, ProductId = 101, CustomerId = 1, DeliveryDate = Convert.ToDateTime("2021/06/21"), Quantity = 5, VendorId = 201, Zipcode = 641008 };
            var moqContext = new Mock<ProceedToBuyContext>();
            moqContext.Setup(c => c.Carts).Returns(dbSetMoq.Object);
            var moqProvider = new Mock<IProvider>();
            moqProvider.Setup(p => p.GetVendors(input.ProductId)).Returns(vendors[0]);
            Repo repo = new Repo(moqContext.Object, moqProvider.Object);
            Assert.IsTrue(repo.AddToCart(input));
        }
        [TestCase(1,101)]
        public void AddToWishList(int customerId,int productId)
        {
            var moqContext = new Mock<ProceedToBuyContext>();
            moqContext.Setup(c => c.Carts).Returns(dbSetMoq.Object);
            moqContext.Setup(c => c.VendorWishlists).Returns(dbWishListSetMoq.Object);
            var moqProvider = new Mock<IProvider>();
            Repo repo = new Repo(moqContext.Object, moqProvider.Object);
            Assert.IsTrue(repo.AddToWishList(customerId, productId));
        }
        [TestCase(1)]
        
        public void GetWishList_Positive(int customerId)
        {
            var moqContext = new Mock<ProceedToBuyContext>();
            moqContext.Setup(c => c.Carts).Returns(dbSetMoq.Object);
            moqContext.Setup(c => c.VendorWishlists).Returns(dbWishListSetMoq.Object);
            var moqProvider = new Mock<IProvider>();
            Repo repo = new Repo(moqContext.Object, moqProvider.Object);
            List<VendorWishlist> actual = repo.GetWishlist(customerId);
            CollectionAssert.AreEqual(wishList, actual);
        }
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(102)]
        public void GetWishList_Negative(int customerId)
        {
            var moqContext = new Mock<ProceedToBuyContext>();
            moqContext.Setup(c => c.Carts).Returns(dbSetMoq.Object);
            moqContext.Setup(c => c.VendorWishlists).Returns(dbWishListSetMoq.Object);
            var moqProvider = new Mock<IProvider>();
            Repo repo = new Repo(moqContext.Object, moqProvider.Object);
            List<VendorWishlist> actual = repo.GetWishlist(customerId);
            Assert.IsFalse(wishList.Equals(actual));
        }
        [TestCase(1)]
        
        public void DeleteCustomerCart_Positive(int customerId)
        {
            var moqContext = new Mock<ProceedToBuyContext>();
            moqContext.Setup(c => c.Carts).Returns(dbSetMoq.Object);
            moqContext.Setup(c => c.VendorWishlists).Returns(dbWishListSetMoq.Object);
            var moqProvider = new Mock<IProvider>();
            Repo repo = new Repo(moqContext.Object, moqProvider.Object);
            Assert.IsTrue(repo.DeleteCustomerCart(customerId));
        }
        [TestCase(-1)]
        [TestCase(102)]
        public void DeleteCustomerCart_Negative(int customerId)
        {
            var moqContext = new Mock<ProceedToBuyContext>();
            moqContext.Setup(c => c.Carts).Returns(dbSetMoq.Object);
            moqContext.Setup(c => c.VendorWishlists).Returns(dbWishListSetMoq.Object);
            var moqProvider = new Mock<IProvider>();
            Repo repo = new Repo(moqContext.Object, moqProvider.Object);
            Assert.IsFalse(repo.DeleteCustomerCart(customerId));
        }
        [TestCase(1)]
        public void DeleteByCartId(int cardId)
        {
            var moqContext = new Mock<ProceedToBuyContext>();
            moqContext.Setup(c => c.Carts).Returns(dbSetMoq.Object);
            moqContext.Setup(c => c.VendorWishlists).Returns(dbWishListSetMoq.Object);
            var moqProvider = new Mock<IProvider>();
            Repo repo = new Repo(moqContext.Object, moqProvider.Object);
            Assert.IsTrue(repo.DeleteCartById(cardId));
        }


    }
}