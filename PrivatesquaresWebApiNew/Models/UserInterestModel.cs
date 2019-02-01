using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrivatesquaresWebApiNew.Models
{
    public class UserInterestModel
    {
        public long UserId { get; set; }
        public long InterestId { get; set; }
        public long InterestCatId { get; set; }

    }
}