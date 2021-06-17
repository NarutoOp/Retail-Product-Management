using AuthorizationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationAPI.Repository
{
    public interface IUserRepo
    {
        public UserCredentials GetUserCred(UserCredentials cred);
        public bool RegisterUserCred(UserCredentials cred);
        public int IncrementCount(UserCredentials cred);
    }
}
