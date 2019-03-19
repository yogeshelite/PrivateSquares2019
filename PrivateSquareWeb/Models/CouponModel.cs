using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrivateSquareWeb.Models
{
    public class CouponModel
    {
        public long UserId { get; set; }
        public string CouponCode { get; set; }
        public string Name { get; set; }
        public string MaxDiscount { get; set; }
        public string PromoType { get; set; }
        public string MinOrderValue { get; set; }
        public string IsRedeemAllowed { get; set; }
        public string Description { get; set; }
    }
}