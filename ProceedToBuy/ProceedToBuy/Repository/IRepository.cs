using ProceedToBuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedToBuy.Repository
{
    public interface IRepository<T>
    {
        public CartVendor AddToCart(Cart t);
        public List<Cart> GetCart();
        public bool AddToWishList(int customerId,int productId);

        public List<VendorWishlist> GetWishlist(int id);

    }
}
