using AuthorizationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace AuthorizationAPI.Provider
{
    public class UserProvider : IUserProvider
    {

        UserContext _context;

        public UserProvider(UserContext dbContext)
        {
             _context = dbContext;
        }

        public UserCredentials GetUser(UserCredentials cred)
        {
            List<UserCredentials> rList = _context.Users.ToList();

            /*UserCredentials userCred = rList.FirstOrDefault(user => user.Username == cred.Username && user.Password == cred.Password);*/

            UserCredentials userCred = rList.FirstOrDefault(user => user.Username == cred.Username);
            if(userCred == null)
                return null;

            if (Crypto.VerifyHashedPassword(userCred.Password,cred.Password))
                return userCred;
            return null;
        }

        public bool RegisterUser(UserCredentials cred)
        {
            List<UserCredentials> rList = _context.Users.ToList();
            UserCredentials userCred = rList.FirstOrDefault(user => user.Username == cred.Username);
            if (userCred != null)
            {
                return false;
            }

            string hash = Crypto.HashPassword(cred.Password);

            cred.Password = hash;
            _context.Add(cred);
            _context.SaveChanges();
            

            return true;
        }

        public int IncrementCounter(UserCredentials cred)
        {
            List<UserCredentials> rList = _context.Users.ToList();
            UserCredentials userCred = rList.FirstOrDefault(user => user.Username == cred.Username);
            if (userCred == null)
            {
                return 0;
            }
            userCred.Counter += 1;
            var count = userCred.Counter;
            if (userCred.Counter == 5)
            {
                userCred.BanTime = DateTime.Now.AddDays(1);
                userCred.Counter = 0;
            }

            _context.SaveChanges();


            return count;
        }
    }
}
