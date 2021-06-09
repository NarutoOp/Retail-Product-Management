using AuthorizationAPI.Model;
using AuthorizationAPI.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationAPI.Repository
{
    public class PensionRepo : IPensionRepo
    {
        private IPensionProvider provider;

        public PensionRepo(IPensionProvider _provider)
        {
            provider = _provider;
        }
        public PensionCredentials GetPensionerCred(PensionCredentials cred)
        {
            if(cred == null)
            {
                return null;
            }

            PensionCredentials pensioner = provider.GetPensioner(cred);

            return pensioner;
        }
    }
}
