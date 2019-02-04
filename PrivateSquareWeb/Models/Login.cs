using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrivateSquareWeb.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Mobile number is required.")]

        public string Mobile { get; set; }
    }
}