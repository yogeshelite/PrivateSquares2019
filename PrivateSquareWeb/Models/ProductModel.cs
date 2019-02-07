using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrivateSquareWeb.Models
{
    public class ProductModel
    {
        public long Id { get; set; }
        public string ProductName { get; set; }
        public long ProductCatId { get; set; }
        public string ProductImage { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public long BusinessId { get; set; }
        public long UserId { get; set; }
       // public string Description { get; set; }
        public string Operation { get; set; }
    }
}