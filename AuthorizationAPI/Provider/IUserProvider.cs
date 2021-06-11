using AuthorizationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationAPI.Provider
{
    public interface IUserProvider
    {
        public List<UserCredentials> GetList();

        public UserCredentials GetUser(UserCredentials cred);
    }
}
