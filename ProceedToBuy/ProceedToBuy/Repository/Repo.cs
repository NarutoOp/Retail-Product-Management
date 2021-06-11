using Microsoft.Extensions.Configuration;
using ProceedToBuy.Models;
using ProceedToBuy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ProceedToBuy.Repository
{
    public class Repo : IRepository<Cart>
    {

        IProvider _provider;
        ProceedToBuyContext _proceedToBuyContext;
        public Repo(ProceedToBuyContext proceedToBuyContext,IProvider provider)
        {
            _provider = provider;
            _proceedToBuyContext = proceedToBuyContext;
           
        }
        public Cart AddToCart(Cart _cart)
        {
       
            
            _cart.Vendor = _provider.GetVendors(_cart.ProductId);
            if(_cart.Vendor == null)
            {
                AddToWishList(_cart.CustomerId, _cart.ProductId);

            }
            else
            {
                _proceedToBuyContext.Carts.Add(_cart);
                _proceedToBuyContext.SaveChanges();

            }
           
            return _cart;
        }
        public bool AddToWishList(int customerId,int productId)
        {
            VendorWishlist vendorWishlist = new VendorWishlist();
            vendorWishlist.ProductId = productId;
            vendorWishlist.Quantity = 1;
            vendorWishlist.DateAddedToWishlist = DateTime.Now;
            Vendor vendor = _provider.GetVendors(productId);
            vendorWishlist.VendorId = vendor.Id;
            _proceedToBuyContext.VendorWishlists.Add(vendorWishlist);
            _proceedToBuyContext.SaveChanges();
            return true;
        }
        
    }
}
