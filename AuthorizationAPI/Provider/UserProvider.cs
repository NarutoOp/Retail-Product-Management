using AuthorizationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationAPI.Provider
{
    public class UserProvider : IUserProvider
    {

        UserContext _context;

        public UserProvider(UserContext dbContext)
        {
             _context = dbContext;
        }

        public List<UserCredentials> GetList()
        {
            return _context.Users.ToList();
        }

        public UserCredentials GetUser(UserCredentials cred)
        {
            List<UserCredentials> rList = GetList();
            UserCredentials userCred = rList.FirstOrDefault(user => user.Username == cred.Username && user.Password == cred.Password);

            return userCred;
        }
    }
}
