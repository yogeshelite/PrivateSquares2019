using Newtonsoft.Json;
using PrivateSquareWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace PrivateSquareWeb.CommonCls
{
    public class CommonFile
    {
        public static ResponseModel GetApiResponse(string Url, String Data)
        {
            var _response = Services.GetApiResponseJson(Url, "POST", Data);

            ResponseModel _data = JsonConvert.DeserializeObject<ResponseModel>(_response);
            ResponseModel ObjResponse = JsonConvert.DeserializeObject<ResponseModel>(_data.Response);
            return ObjResponse;
        }

        public static List<DropDownModel> GetCountry()
        {
            var CountryList = new List<DropDownModel>();
            DropDownModel objUserProfile = new DropDownModel();
              var _request = JsonConvert.SerializeObject(objUserProfile);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetCountry, _request);
            CountryList = JsonConvert.DeserializeObject<List<DropDownModel>>(ObjResponse.Response);
            return CountryList;
        }
        public static List<DropDownModel> GetState()
        {
            var StateList = new List<DropDownModel>();
            DropDownModel objUserProfile = new DropDownModel();
              var _request = JsonConvert.SerializeObject(objUserProfile);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetState, _request);
            StateList = JsonConvert.DeserializeObject<List<DropDownModel>>(ObjResponse.Response);
            return StateList;
        }
        public static List<DropDownModel> GetCity()
        {
            var CityList = new List<DropDownModel>();
            DropDownModel objUserProfile = new DropDownModel();
             var _request = JsonConvert.SerializeObject(objUserProfile);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetCity, _request);
            CityList = JsonConvert.DeserializeObject<List<DropDownModel>>(ObjResponse.Response);
            return CityList;
        }
        public static List<DropDownModel> GetProfession()
        {
            var ProfessionList = new List<DropDownModel>();
            DropDownModel objUserProfile = new DropDownModel();
            //  var _request = JsonConvert.SerializeObject(objUserProfile);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetProfession, "");
            ProfessionList = JsonConvert.DeserializeObject<List<DropDownModel>>(ObjResponse.Response);
            return ProfessionList;
        }
        public static List<DropDownModel> GetProductCategory()
        {
            var ProductCategoryList = new List<DropDownModel>();
            DropDownModel objUserProfile = new DropDownModel();
            //  var _request = JsonConvert.SerializeObject(objUserProfile);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetProductCategory, "");
            ProductCategoryList = JsonConvert.DeserializeObject<List<DropDownModel>>(ObjResponse.Response);
            return ProductCategoryList;
        }

        public static int SendMailContact(string receiverEmailId, string subject, string userName, string userPassword)
        {
            try
            {
                string Host = string.Empty;
                string Port = string.Empty; ;
                string EnableSsl = "false";
                string UseDefaultCredentials = "false";
                string FromMailAddress = string.Empty; ;
                string FromMailerPWD = string.Empty;
                bool IsGmailSmptServer = Convert.ToBoolean(ConfigurationManager.AppSettings["IsGmailSmpt"]);
                if (IsGmailSmptServer)
                {

                    #region Gmail Setting For Mail
                    Host = ConfigurationManager.AppSettings["Host"];
                    Port = ConfigurationManager.AppSettings["Port"];
                    EnableSsl = ConfigurationManager.AppSettings["EnableSsl"];
                    UseDefaultCredentials = ConfigurationManager.AppSettings["UseDefaultCredentials"];
                    FromMailAddress = ConfigurationManager.AppSettings["FromMailAddress"];
                    FromMailerPWD = ConfigurationManager.AppSettings["FromMailerPWD"];
                    #endregion
                }
                else
                {
                    #region Godaddy Setting For Mail
                    Host = ConfigurationManager.AppSettings["HostGoD"];
                    Port = ConfigurationManager.AppSettings["PortGoD"];
                    FromMailAddress = ConfigurationManager.AppSettings["FromMailAddressGoD"];
                    FromMailerPWD = ConfigurationManager.AppSettings["FromMailerPWDGoD"];
                    #endregion
                }
                var senderEmail = new MailAddress(FromMailAddress, userName);
                var receiverEmail = new MailAddress(receiverEmailId, "Receiver");
                var password = FromMailerPWD;
                var body = "<b>Thanks For Visit Your </b><p> User Name Is=" + userName + "</p><p> Password Is= " + userPassword + "</p>";
                var smtp = new SmtpClient
                {
                    Host = Host,
                    Port = Convert.ToInt16(Port),
                    EnableSsl = Convert.ToBoolean(EnableSsl),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = Convert.ToBoolean(UseDefaultCredentials),
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,

                })
                {
                    smtp.Send(mess);
                }
                return 1;
            }
            catch (Exception ex)
            {
                string exception = ex.ToString();
                return 0;
                //ViewBag.Error = "Some Error";
            }
        }

    }
}