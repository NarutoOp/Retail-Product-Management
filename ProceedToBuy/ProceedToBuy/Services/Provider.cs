using Microsoft.Extensions.Configuration;
using ProceedToBuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProceedToBuy.Services
{
    public class Provider:IProvider
    {

        private IConfiguration _configure;
        static private string apiBaseUrl;
        public Provider(IConfiguration configure)
        {
            _configure = configure;
            apiBaseUrl = _configure.GetValue<string>("WebAPIBaseUrl");

        }

        public Vendor GetVendors(int productId)
        {
            IList<Vendor> vendors = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                string api = "Vendor/" + productId;
                var responseTask = client.GetAsync(api);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readData = result.Content.ReadAsAsync<IList<Vendor>>();
                    readData.Wait();

                    vendors = readData.Result;
                }
            }
            double max = vendors.Max(v => v.Rating);
            Vendor vendor = vendors.FirstOrDefault(v => v.Rating == max);
            return vendor;

        }
    }
}
