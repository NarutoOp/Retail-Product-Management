using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedToBuy.Repository
{
    public interface IRepository<T>
    {
        public T AddToCart(T t);
        public bool AddToWishList(int customerId,int productId);

    }
}
