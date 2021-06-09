using AuthorizationAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationAPI.Provider
{
    public class PensionProvider : IPensionProvider
    {
        private static List<PensionCredentials> List = new List<PensionCredentials>()
        {
            new PensionCredentials{ Username = "user1", Password = "user1"},
            new PensionCredentials{ Username = "user2", Password = "user2"}
        };
        public List<PensionCredentials> GetList()
        {
            return List;
        }

        public PensionCredentials GetPensioner(PensionCredentials cred)
        {
            List<PensionCredentials> rList = GetList();
            PensionCredentials penCred = rList.FirstOrDefault(user => user.Username == cred.Username && user.Password == cred.Password);

            return penCred;
        }
    }
}
