using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrivateSquareWeb.Models
{
    public class ProductModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Product Name cannot be empty")]
        public string ProductName { get; set; }
        public long ProductCatId { get; set; }
        public string ProductImage { get; set; }
        [Required(ErrorMessage ="Selling Price cannot be empty")]
        public decimal SellingPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public long BusinessId { get; set; }
        public long UserId { get; set; }
       // public string Description { get; set; }
        public string Operation { get; set; }
    }
}