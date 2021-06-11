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
    }
}
