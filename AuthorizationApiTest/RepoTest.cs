using AuthorizationAPI.Models;
using AuthorizationAPI.Provider;
using AuthorizationAPI.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AuthorizationApiTest
{
    public class RepoTest
    {
        List<UserCredentials> users = new List<UserCredentials>();

        [SetUp]
        public void SetUp()
        {
            users.Add(new UserCredentials { Id = 1, Username = "admin", Password = "admin", Counter = 2 });

        }

        [TestCase]
        public void GetUserCredTest()
        {
            UserCredentials input = new UserCredentials { Username = "admin", Password = "admin" };

            var moqProvider = new Mock<IUserProvider>();
            moqProvider.Setup(c => c.GetUser(input)).Returns(users[0]);

            var moqRepo = new UserRepo(moqProvider.Object);
                
            var result = moqRepo.GetUserCred(input);

            Assert.IsNotNull(result);
        }

        [TestCase]
        public void RegisterUserCredTest()
        {
            UserCredentials input = new UserCredentials { Username = "user", Password = "user" };

            var moqProvider = new Mock<IUserProvider>();
            moqProvider.Setup(c => c.RegisterUser(input)).Returns(true);

            var moqRepo = new UserRepo(moqProvider.Object);

            var result = moqRepo.RegisterUserCred(input);

            Assert.IsTrue(result);
        }

        [TestCase]
        public void IncrementCountTest()
        {
            UserCredentials input = new UserCredentials { Username = "user", Password = "user" };

            var moqProvider = new Mock<IUserProvider>();
            moqProvider.Setup(c => c.IncrementCounter(input)).Returns(1);

            var moqRepo = new UserRepo(moqProvider.Object);

            var result = moqRepo.IncrementCount(input);

            Assert.AreEqual(1,result);
        }
    }
}
