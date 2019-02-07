using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrivateSquareWeb.Models
{
    public class BusinessModel
    {
        public int Id { get; set; } = 0;
        public string BusinessName { get; set; } = "";
        public string Location { get; set; }="";
        public string BusinessLogo { get; set; }="";
        public long ProfessionalCatId { get; set; }=0;
        public string ProfessionalKeyword { get; set; }="";
        public long CityId { get; set; }=0;
        public string PinCode { get; set; } = "";
        public long UserId { get; set; } = 0;
        public string Email { get; set; } = "";
        public string Description { get; set; } = "";
        public long CountryId { get; set; } = 0;
        public string Phone { get; set; } = "";
        public string Operation { get; set; } = "";
    }
}