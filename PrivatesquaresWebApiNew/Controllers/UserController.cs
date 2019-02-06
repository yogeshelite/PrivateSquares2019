using Newtonsoft.Json;
using PrivatesquaresWebApiNew.CommonCls;
using PrivatesquaresWebApiNew.Models;
using PrivatesquaresWebApiNew.Persistance.Repositary;
using PrivatesquaresWebApiNew.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;

namespace PrivatesquaresWebApiNew.Controllers
{
    public class UserController : ApiController
    {
        private IUserServices userServices;

        public UserController()
        {
            userServices = new UserServices(new UserRepositary());
        }
        [Route("api/User/Registeration")]
        [HttpPost]
        // public IHttpActionResult Registeration(RequestModel requestModel)
        public IHttpActionResult Registeration([FromBody] RequestModel requestModel)
        {
            // = JsonConvert.DeserializeObject<ApiRequestModel>(System.Web.HttpContext.Current.Request.Form["encrypted"].ToString());

            //var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            var data = requestModel.Data;
            UserRegisterModel user = JsonConvert.DeserializeObject<UserRegisterModel>(data);
            // if (request.ContainsKey("unique_name"))
            {
                var sendResponse = new ResponseModel() { Response = JsonConvert.SerializeObject(userServices.RegisterUser(user)), Success = true };
                var sendJson = Json(sendResponse);
                return sendJson;
            }

            //  return Json(new ResponseModel() { Response = BadRequest().ToString(), Success = false });


        }

        [HttpPost]
        [Route("api/User/AuthenticateUser")]
        public IHttpActionResult AuthenticateUser(RequestModel requestModel)
        {

            // var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            var data = requestModel.Data;
            // Dictionary<string, object> request = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
            //if (request.ContainsKey("unique_name"))
            {
                UserRegisterModel login = JsonConvert.DeserializeObject<UserRegisterModel>(data);
                var SendResponse = new ResponseModel() { Response = JsonConvert.SerializeObject(userServices.Login(login)), Success = true };
                return Json(SendResponse);
            }
            //return Json(new ResponseModel() { Response = BadRequest().ToString(), Success = false });
        }

        [Route("api/User/SaveProfile")]
        [HttpPost]
        // public IHttpActionResult Registeration(RequestModel requestModel)
        public IHttpActionResult SaveUserProfile(RequestModel requestModel)
        {
            #region Comment Code For DataSet To Json 
            //DataTable dt = new DataTable();
            //dt.Clear();
            //dt.Columns.Add("UserId");
            //dt.Columns.Add("InterestId");
            //dt.Columns.Add("InterestCatId");
            //DataRow _ravi;
            //_ravi = dt.NewRow();
            //_ravi["UserId"] = "1";
            //_ravi["InterestId"] = "2";
            //_ravi["InterestCatId"] = "3";
            //dt.Rows.Add(_ravi);
            //_ravi = dt.NewRow();
            //_ravi["UserId"] = "1";
            //_ravi["InterestId"] = "3";
            //_ravi["InterestCatId"] = "4";
            //dt.Rows.Add(_ravi);
            //_ravi = dt.NewRow();
            //_ravi["UserId"] = "1";
            //_ravi["InterestId"] = "4";
            //_ravi["InterestCatId"] = "5";
            //dt.Rows.Add(_ravi);
            //_ravi = dt.NewRow();
            //_ravi["UserId"] = "1";
            //_ravi["InterestId"] = "6";
            //_ravi["InterestCatId"] = "7";
            //dt.Rows.Add(_ravi);

            //string JSONresult;
            //JSONresult = JsonConvert.SerializeObject(dt);
            #endregion

            #region Comment Code For jsonToDataTable
            // List<UserInterestModel> usersList = JsonConvert.DeserializeObject<List<UserInterestModel>>(JSONresult);
            //DataTable dt1= CovnertJsonToDataTable.ToDataTable<UserInterestModel>(usersList);
            #endregion
            var data = requestModel.Data;
            UserProfileModel objUserProfile = JsonConvert.DeserializeObject<UserProfileModel>(data);
            var sendResponse = new ResponseModel() { Response = JsonConvert.SerializeObject(userServices.SaveProfile(objUserProfile)), Success = true };
            var sendJson = Json(sendResponse);
            return sendJson;
        }
        [Route("api/User/SaveBusiness")]
        [HttpPost]
        // public IHttpActionResult Registeration(RequestModel requestModel)
        public IHttpActionResult SaveBusiness(RequestModel requestModel)
        {

            var data = requestModel.Data;
            BusinessModel objUserProfile = JsonConvert.DeserializeObject<BusinessModel>(data);
            var sendResponse = new ResponseModel() { Response = JsonConvert.SerializeObject(userServices.SaveBusiness(objUserProfile)), Success = true };
            var sendJson = Json(sendResponse);
            return sendJson;
        }

        [Route("api/User/SaveUserInterest")]
        [HttpPost]

        public IHttpActionResult SaveUserInterest(RequestModel requestModel)
        {

            var data = JsonConvert.DeserializeObject(requestModel.Data);
            var xmlNode = JsonConvert.DeserializeXmlNode(data.ToString(), "root").OuterXml;
            //  XNode node = JsonConvert.DeserializeXNode(data).outerxml;

            UserInterestModel objUserInterest = new UserInterestModel();
            objUserInterest.XmlData = xmlNode;
            // UserInterestModel objUserInterest = JsonConvert.DeserializeObject<UserInterestModel>(data.ToString());
            var sendResponse = new ResponseModel() { Response = JsonConvert.SerializeObject(userServices.SaveUserInterest(objUserInterest)), Success = true };
            var sendJson = Json(sendResponse);
            return sendJson;
        }

        [Route("api/User/SaveProduct")]
        [HttpPost]
        // public IHttpActionResult Registeration(RequestModel requestModel)
        public IHttpActionResult SaveProduct(RequestModel requestModel)
        {

            var data = requestModel.Data;
            ProductModel objProductModel = JsonConvert.DeserializeObject<ProductModel>(data);
            var sendResponse = new ResponseModel() { Response = JsonConvert.SerializeObject(userServices.SaveProduct(objProductModel)), Success = true };
            var sendJson = Json(sendResponse);
            return sendJson;
        }

        [Route("api/User/GetUsersProfile")]
        [HttpPost]
        public IHttpActionResult GetUserProfile(RequestModel requestModel)
        {

            //var data = requestModel.Data;
            //ProductModel objProductModel = JsonConvert.DeserializeObject<ProductModel>(data);
            var sendResponse = new ResponseModel() { Response = JsonConvert.SerializeObject(userServices.GetUsersProfile()), Success = true };
            var sendJson = Json(sendResponse);
            return sendJson;
        }



        [Route("api/User/GetAllInterest")]
        [HttpPost]
        public IHttpActionResult GetAllInterest(RequestModel requestModel)
        {

            //var data = requestModel.Data;
            //ProductModel objProductModel = JsonConvert.DeserializeObject<ProductModel>(data);
            var sendResponse = new ResponseModel() { Response = JsonConvert.SerializeObject(userServices.GetAllInterest()), Success = true };
            var sendJson = Json(sendResponse);
            return sendJson;
        }
        [Route("api/User/GetAllInterestCategory")]
        [HttpPost]
        public IHttpActionResult GetAllInterestCategory(RequestModel requestModel)
        {

            //var data = requestModel.Data;
            //ProductModel objProductModel = JsonConvert.DeserializeObject<ProductModel>(data);
            var sendResponse = new ResponseModel() { Response = JsonConvert.SerializeObject(userServices.GetAllInterestCategory()), Success = true };
            var sendJson = Json(sendResponse);
            return sendJson;
        }
        [Route("api/User/SaveNetwork")]
        [HttpPost]
        public IHttpActionResult SaveNetwork(RequestModel requestModel)
        {

            var data = requestModel.Data;
            NetworkModel objNetworkModel = JsonConvert.DeserializeObject<NetworkModel>(data);
            var sendResponse = new ResponseModel() { Response = JsonConvert.SerializeObject(userServices.SaveNetwork(objNetworkModel)), Success = true };
            var sendJson = Json(sendResponse);
            return sendJson;
        }
        [Route("api/User/GetNetwork")]
        [HttpPost]
        public IHttpActionResult GetNetwork(RequestModel requestModel)
        {

            var data = requestModel.Data;
            NetworkModel objNetworkModel = JsonConvert.DeserializeObject<NetworkModel>(data);
            var sendResponse = new ResponseModel() { Response = JsonConvert.SerializeObject(userServices.GetNetwork(objNetworkModel)), Success = true };
            var sendJson = Json(sendResponse);
            return sendJson;
        }
    }
}
