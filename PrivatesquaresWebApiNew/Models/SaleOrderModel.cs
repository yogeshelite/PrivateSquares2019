using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrivatesquaresWebApiNew.Models
{
    public class SaleOrderModel
    {

        public long UserId { get; set; }
        public long? SaleOrderId { get; set; }

        public long ProductId { get; set; }
        public string PaymentMode { get; set; }

        public string XmlSaleOrderDetail { get; set; }

        public decimal TotalDiscount { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Operation { get; set; }
    }
}