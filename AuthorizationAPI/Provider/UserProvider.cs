using AuthorizationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationAPI.Provider
{
    public class UserProvider : IUserProvider
    {
        private static List<UserCredentials> List = new List<UserCredentials>()
        {
            new UserCredentials{ Id = 1, Username = "user1", Password = "user1"},
            new UserCredentials{ Id = 2, Username = "user2", Password = "user2"}
        };
        public List<UserCredentials> GetList()
        {
            return List;
        }

        public UserCredentials GetUser(UserCredentials cred)
        {
            List<UserCredentials> rList = GetList();
            UserCredentials userCred = rList.FirstOrDefault(user => user.Username == cred.Username && user.Password == cred.Password);

            return userCred;
        }
    }
}
