using AuthorizationAPI.Models;
using AuthorizationAPI.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationAPI.Repository
{
    public class UserRepo : IUserRepo
    {

        private IUserProvider provider;

        public UserRepo(IUserProvider _provider)
        {
            provider = _provider;
        }
        public UserCredentials GetUserCred(UserCredentials cred)
        {
            if(cred == null)
            {
                return null;
            }

            UserCredentials user = provider.GetUser(cred);

            return user;
        }

        public bool RegisterUserCred(UserCredentials cred)
        {

            var user = provider.RegisterUser(cred);

            return user;
        }

        public int IncrementCount(UserCredentials cred)
        {

            var count = provider.IncrementCounter(cred);

            return count;
        }
    }
}
