using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMicroservices.Contants
{
    public static class Constant
    {
        public static string ProductNotFound => "There are no product in the stock";
        public static string ProductNotFoundById => "Product not found for the given productId";

        public static string ProductNotFoundByName => "Product not found for the given productName";
        public static string ProductRatingNotAdded => "Product not found for the given productID...no rating added";
        public static string ProductRating => "Added Rating to the Product";
    }
}
