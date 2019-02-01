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
        public IHttpActionResult Registeration(RequestModel requestModel)
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

            //DataSet dataSet1 = JsonConvert.DeserializeObject<DataSet>(JSONresult);
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

            


        }
       
    }
}
