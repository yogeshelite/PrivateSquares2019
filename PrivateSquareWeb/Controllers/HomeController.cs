using ASPSnippets.FaceBookAPI;
using Newtonsoft.Json;
using PrivateSquareWeb.CommonCls;
using PrivateSquareWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PrivateSquareWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<UsersProfileModel> usersList = GetAllUsers();
            //if (Services.GetCookie(this.ControllerContext.HttpContext,"usrId") != null)
            //{
                
            //}
            ViewBag.AllUsers = usersList;
            // For Facebook Login
            FaceBookUser faceBookUser = new FaceBookUser();
            if (Request.QueryString["error"] == "access_denied")
            {
                ViewBag.Message = "User has denied access.";
            }
            else
            {
                string code = Request.QueryString["code"];
                if (!string.IsNullOrEmpty(code))
                {
                    string data = FaceBookConnect.Fetch(code, "me?fields=id,name,email");
                    faceBookUser = new JavaScriptSerializer().Deserialize<FaceBookUser>(data);
                    faceBookUser.PictureUrl = string.Format("https://graph.facebook.com/{0}/picture", faceBookUser.Id);
                }
            }

            return View();
        }
        public List<UsersProfileModel> GetAllUsers()
        {
            var GetAllUserList = new List<UsersProfileModel>();
           // var _request = "";//_JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(loginModel));
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetUsersProfile, "");
            GetAllUserList = JsonConvert.DeserializeObject<List<UsersProfileModel>>(ObjResponse.Response);
            return GetAllUserList;

        }
        public List<BusinessModel> GetUsersBusiness()
        {
            var GetUserBusinessList = new List<BusinessModel>();
            BusinessModel objmodel = new BusinessModel();
            objmodel.UserId = Convert.ToInt64(Services.GetCookie(this.ControllerContext.HttpContext, "usrId").Value);
            var _request = JsonConvert.SerializeObject(objmodel);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetUserBusiness, _request);
            GetUserBusinessList = JsonConvert.DeserializeObject<List<BusinessModel>>(ObjResponse.Response);
            return GetUserBusinessList;

        }
        public List<ProductModel> GetProduct()
        {
            var GetUserProductList = new List<ProductModel>();
            ProductModel objmodel = new ProductModel();
            objmodel.UserId = Convert.ToInt64(Services.GetCookie(this.ControllerContext.HttpContext, "usrId").Value);
            var _request = JsonConvert.SerializeObject(objmodel);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetProduct, _request);
            GetUserProductList = JsonConvert.DeserializeObject<List<ProductModel>>(ObjResponse.Response);
            return GetUserProductList;

        }
        public List<UsersProfileModel> GetUsersNetwork()
        {
            var GetUserNetworkList = new List<UsersProfileModel>();
            NetworkModel objmodel = new NetworkModel();
            objmodel.LogInUserId = Convert.ToInt64(Services.GetCookie(this.ControllerContext.HttpContext, "usrId").Value);
            var _request = JsonConvert.SerializeObject(objmodel);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetUserNetwork, _request);
            GetUserNetworkList = JsonConvert.DeserializeObject<List<UsersProfileModel>>(ObjResponse.Response);
            return GetUserNetworkList;

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult MyBusinessList()
        {

            ViewBag.UsersBusiness = GetUsersBusiness();
            return View();
        }
        public ActionResult ProductList()
        {
            ViewBag.UsersProduct = GetProduct();
            return View();
        }
        public ActionResult NetworkList()
        {
            ViewBag.UsersNetwork = GetUsersNetwork();
            return View();
        }
        [HttpPost]
        public JsonResult AddNetwork(NetworkModel objNetworkModel)
        {
            if (Services.GetCookie(this.ControllerContext.HttpContext, "usrId") != null)
            {
                objNetworkModel.LogInUserId = Convert.ToInt64(Services.GetCookie(this.ControllerContext.HttpContext, "usrId").Value);

                objNetworkModel.Operation = "insert";
                var _request = JsonConvert.SerializeObject(objNetworkModel);
                ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiSaveNetwork, _request);
                if (String.IsNullOrWhiteSpace(ObjResponse.Response))
                {
                }
                String Response = "[{\"Response\":\"" + ObjResponse.Response + "\"}]";
                return Json(Response);
            }
            else
            {
                String Response = "[{\"Response\":\"" +" NotLogin" + "\"}]";
                return Json(Response);
            }
        }
    }
}