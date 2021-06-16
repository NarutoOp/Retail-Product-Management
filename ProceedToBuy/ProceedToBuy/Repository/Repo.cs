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
        public List<Cart> GetCart()
        {

            return _proceedToBuyContext.Carts.ToList();
        }
        public CartVendor AddToCart(Cart _cart)
        {

            CartVendor cv = new CartVendor();
            cv.Carts = _cart;
            cv.Vendor = _provider.GetVendors(_cart.ProductId);
            if(cv.Vendor == null)
            {
                AddToWishList(_cart.CustomerId, _cart.ProductId);
                return cv;
            }
            else
            {

                Cart val = _proceedToBuyContext.Carts.SingleOrDefault(c => c.CustomerId == _cart.CustomerId && c.ProductId == _cart.ProductId);
                if (val!= null)
                {
                    val.Quantity += _cart.Quantity;
                }
                else
                    _proceedToBuyContext.Carts.Add(_cart);
                _proceedToBuyContext.SaveChanges();

            }
           
            return cv;
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

        public List<VendorWishlist> GetWishlist(int id)
        {
            return _proceedToBuyContext.VendorWishlists.Where(v => v.CustomerId == id).ToList();
        }

    }
}
