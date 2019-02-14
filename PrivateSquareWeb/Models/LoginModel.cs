using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrivateSquareWeb.Models
{
    public class LoginModel
    {
        public long Id { get; set; } = 0;
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Name { get; set; }
        public string ProfileImg { get; set; } = "";
        public string Mobile { get; set; }
    }
}