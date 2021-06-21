using AuthorizationAPI.Models;
using System.Collections.Generic;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq;
using AuthorizationAPI.Repository;
using AuthorizationAPI.Provider;

namespace AuthorizationApiTest
{
    public class ProviderTest
    {
        List<UserCredentials> users = new List<UserCredentials>();
        Mock<DbSet<UserCredentials>> dbSetMoq;

        [SetUp]
        public void Setup()
        {
            users.Add(new UserCredentials{Id = 1, Username = "admin", Password = "admin", Counter = 2});

            dbSetMoq = new Mock<DbSet<UserCredentials>>();

            var queriableUserList = users.AsQueryable();

            dbSetMoq.As<IQueryable<UserCredentials>>().Setup(x => x.Provider).Returns(queriableUserList.Provider);
            dbSetMoq.As<IQueryable<UserCredentials>>().Setup(x => x.Expression).Returns(queriableUserList.Expression);
            dbSetMoq.As<IQueryable<UserCredentials>>().Setup(x => x.ElementType).Returns(queriableUserList.ElementType);
            dbSetMoq.As<IQueryable<UserCredentials>>().Setup(x => x.GetEnumerator()).Returns(queriableUserList.GetEnumerator());
        }

        

        [TestCase]
        public void GetUserTest()
        {
            var moqContext = new Mock<UserContext>();
            moqContext.Setup(v => v.Users).Returns(dbSetMoq.Object);

            UserProvider provider = new UserProvider(moqContext.Object);

            UserCredentials user = new UserCredentials { Username = "admin", Password = "admin" };

            var result = provider.GetUser(user);

            Assert.IsNotNull(result);
        }

        [TestCase]
        public void RegisterUserTest()
        {
            var moqContext = new Mock<UserContext>();
            moqContext.Setup(v => v.Users).Returns(dbSetMoq.Object);

            UserProvider provider = new UserProvider(moqContext.Object);

            UserCredentials user = new UserCredentials { Username = "root", Password = "root" };

            var result = provider.RegisterUser(user);

            Assert.IsTrue(result);
        }

        [TestCase]
        public void IncrementCounterTest()
        {
            var moqContext = new Mock<UserContext>();
            moqContext.Setup(v => v.Users).Returns(dbSetMoq.Object);

            UserProvider provider = new UserProvider(moqContext.Object);

            UserCredentials user = new UserCredentials { Username = "user", Password = "user" };

            var result = provider.IncrementCounter(user);

            Assert.IsNotNull(result);
        }
    }
}