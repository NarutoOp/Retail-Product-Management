using AuthorizationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationAPI.Provider
{
    public interface IUserProvider
    {

        public UserCredentials GetUser(UserCredentials cred);
        public bool RegisterUser(UserCredentials cred);

        public int IncrementCounter(UserCredentials cred);
    }
}
