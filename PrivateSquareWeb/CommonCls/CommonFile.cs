using Newtonsoft.Json;
using PrivateSquareWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace PrivateSquareWeb.CommonCls
{
    public class CommonFile
    {
        static JwtTokenManager _JwtTokenManager = new JwtTokenManager();
        #region Code Block For PostApi Jwt Or Without
        public static ResponseModel GetApiResponse(string Url, String Data)
        {
            var _response = Services.GetApiResponseJson(Url, "POST", Data);

            ResponseModel _data = JsonConvert.DeserializeObject<ResponseModel>(_response);
            ResponseModel ObjResponse = JsonConvert.DeserializeObject<ResponseModel>(_data.Response);
            return ObjResponse;
        }

        public static ResponseModel GetApiResponseJWT(string Url, String Data)
        {
            JwtTokenManager _JwtTokenManager = new JwtTokenManager();
            var _response = Services.GetApiResponseJson(Url, "POST", Data);

            ResponseModel _dataApi = JsonConvert.DeserializeObject<ResponseModel>(_response);

            dynamic _data = _JwtTokenManager.DecodeToken(_dataApi.Response);
            var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(_data);

            if (json.ContainsKey("unique_name"))
            {
                ResponseModel objRespMOdel = JsonConvert.DeserializeObject<ResponseModel>(json["unique_name"]);
                //var responeSend = objRespMOdel.Response;
                return objRespMOdel;
            }
            return new ResponseModel();
        }
        #endregion
        #region Factory Date For All User
        public static List<DropDownModel> GetCountry()
        {
            var CountryList = new List<DropDownModel>();
            DropDownModel objDropdown = new DropDownModel();



            var _request = _JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(objDropdown));
            ResponseModel ObjResponse = CommonFile.GetApiResponseJWT(Constant.ApiGetCountry, _request);
            CountryList = JsonConvert.DeserializeObject<List<DropDownModel>>(ObjResponse.Response);


            //var _request = JsonConvert.SerializeObject(objUserProfile);
            //ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetCountry, _request);
            //CountryList = JsonConvert.DeserializeObject<List<DropDownModel>>(ObjResponse.Response);
            return CountryList;
        }
        public static List<DropDownModel> GetState()
        {
            var StateList = new List<DropDownModel>();
            DropDownModel objDropdown = new DropDownModel();

            var _request = _JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(objDropdown));
            ResponseModel ObjResponse = CommonFile.GetApiResponseJWT(Constant.ApiGetState, _request);
            StateList = JsonConvert.DeserializeObject<List<DropDownModel>>(ObjResponse.Response);

            //var _request = JsonConvert.SerializeObject(objUserProfile);
            //ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetState, _request);
            //StateList = JsonConvert.DeserializeObject<List<DropDownModel>>(ObjResponse.Response);

            return StateList;
        }
        public static List<DropDownModel> GetCity()
        {
            var CityList = new List<DropDownModel>();
            DropDownModel objDropdown = new DropDownModel();

            var _request = _JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(objDropdown));
            ResponseModel ObjResponse = CommonFile.GetApiResponseJWT(Constant.ApiGetCity, _request);
            CityList = JsonConvert.DeserializeObject<List<DropDownModel>>(ObjResponse.Response);

            return CityList;
        }
        public static List<DropDownModel> GetProfession()
        {
            var ProfessionList = new List<DropDownModel>();
            DropDownModel objDropdown = new DropDownModel();

            var _request = _JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(objDropdown));
            ResponseModel ObjResponse = CommonFile.GetApiResponseJWT(Constant.ApiGetProfession, _request);
            ProfessionList = JsonConvert.DeserializeObject<List<DropDownModel>>(ObjResponse.Response);


            ////  var _request = JsonConvert.SerializeObject(objUserProfile);
            //ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetProfession, "");
            //ProfessionList = JsonConvert.DeserializeObject<List<DropDownModel>>(ObjResponse.Response);
            return ProfessionList;
        }
        public static List<DropDownModel> GetProductCategory()
        {
            var ProductCategoryList = new List<DropDownModel>();
            DropDownModel objDropdown = new DropDownModel();

            var _request = _JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(objDropdown));
            ResponseModel ObjResponse = CommonFile.GetApiResponseJWT(Constant.ApiGetProductCategory, _request);
            ProductCategoryList = JsonConvert.DeserializeObject<List<DropDownModel>>(ObjResponse.Response);


            ////  var _request = JsonConvert.SerializeObject(objUserProfile);
            //ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetProductCategory, "");
            //ProductCategoryList = JsonConvert.DeserializeObject<List<DropDownModel>>(ObjResponse.Response);
            return ProductCategoryList;
        }
        #endregion
        public static List<BusinessModel> GetUsersBusiness(long UserId)
        {
            var GetUserBusinessList = new List<BusinessModel>();
            BusinessModel objmodel = new BusinessModel();
            objmodel.UserId = UserId;

            var _request = _JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(objmodel));
            ResponseModel ObjResponse = CommonFile.GetApiResponseJWT(Constant.ApiGetUserBusiness, _request);
            GetUserBusinessList = JsonConvert.DeserializeObject<List<BusinessModel>>(ObjResponse.Response);


            //var _request = JsonConvert.SerializeObject(objmodel);
            //ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetUserBusiness, _request);
            //GetUserBusinessList = JsonConvert.DeserializeObject<List<BusinessModel>>(ObjResponse.Response);

            return GetUserBusinessList;

        }
        public static int SendMailContact(string receiverEmailId, string subject, string userName, string userPassword, string body)
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
                //var body = "<b>Thanks For Visit Your </b><p> User Name Is=" + userName + "</p><p> Password Is= " + userPassword + "</p>";
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
        public static bool ValidateEmailIsValid(String EmailId)
        {
            string email = EmailId;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        #region For Encode Password
        public static string GeneratePassword(int length) //length of salt    
        {
            const string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            var randNum = new Random();
            var chars = new char[length];
            var allowedCharCount = allowedChars.Length;
            for (var i = 0; i <= length - 1; i++)
            {
                chars[i] = allowedChars[Convert.ToInt32((allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }
        public static string EncodePassword(string pass, string salt) //encrypt password    
        {
            byte[] bytes = Encoding.Unicode.GetBytes(pass);
            byte[] src = Encoding.Unicode.GetBytes(salt);
            byte[] dst = new byte[src.Length + bytes.Length];
            System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dst);
            //return Convert.ToBase64String(inArray);    
            return EncodePasswordMd5(Convert.ToBase64String(inArray));
        }
        public static string EncodePasswordMd5(string pass) //Encrypt using MD5    
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;
            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)    
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(pass);
            encodedBytes = md5.ComputeHash(originalBytes);
            //Convert encoded bytes back to a 'readable' string    
            return BitConverter.ToString(encodedBytes);
        }
        #endregion

        public static bool IsUserAuthenticate(HttpContextBase httpContext)
        {
            LoginModel MDUser = Services.GetLoginUser(httpContext, _JwtTokenManager);
            if (MDUser.Id != 0)
                return true;
            else
                return false;
        }
        public static List<DropDownModel> GetProfessionalKeyword()

        {
            var ProfessionalKeywordList = new List<DropDownModel>();
            DropDownModel objUserProfile = new DropDownModel();
            var _request = JsonConvert.SerializeObject(objUserProfile);
            ResponseModel objResponse = CommonFile.GetApiResponse(Constant.ApiGetProfessionalKeyword, _request);
            ProfessionalKeywordList = JsonConvert.DeserializeObject<List<DropDownModel>>(objResponse.Response);
            return ProfessionalKeywordList;

        }
    }
}