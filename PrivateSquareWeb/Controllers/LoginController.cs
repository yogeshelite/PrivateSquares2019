using Newtonsoft.Json;
using PrivateSquareWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrivateSquareWeb.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {

            return View();
        }
        public PartialViewResult SidebarValue()
        {
            //UserSubscribePlain = GetUserSubscriptionPlan();
            //ViewBag.UserSubscribePlain = UserSubscribePlain;
            return PartialView("~\\Views\\Shared\\_OTP.cshtml");
        }
        [HttpPost]
        public ActionResult UserRegister(UserRegisterModel ObjModel)
        {

            if (String.IsNullOrWhiteSpace(ObjModel.Mobile))
                return View("Index");
            // if (ModelState.IsValid)
            {
                var _request = JsonConvert.SerializeObject(ObjModel);
                ResponseModel ObjResponse = GetApiResponse(Constant.ApiRegister, _request);
                if (String.IsNullOrWhiteSpace(ObjResponse.Response))
                {
                    return View("Index", ObjModel);

                }
                Session["OtpData"] = ObjResponse.Response;
                Session["Mobile"] = ObjModel.Mobile;
                ViewBag.OtpMessage = ObjResponse.Response;
                // return View();
                return RedirectToAction("OTP");
            }

        }
        public ActionResult OTP()
        {
            var otp = Session["OtpData"];
            String OtpCheck = (string)otp;
            String Mobile = (string)Session["Mobile"];
            //Session["OtpData"] = OtpCheck;
            //Session["Mobile"] = Mobile;
            UserRegisterModel ObjModel = new UserRegisterModel();
            ViewBag.OtpMessage = OtpCheck;
            //return PartialView("_OTP", ObjModel);
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserRegisterModel ObjModel)
        {
            var otp = Session["OtpData"];
            String OtpCheck = (string)otp;
            if (string.IsNullOrWhiteSpace(ObjModel.Otp))
                return View("OTP", ObjModel);
            if (OtpCheck.Equals(ObjModel.Otp))
            {
                ObjModel.Mobile = (string)Session["Mobile"];
                var _request = JsonConvert.SerializeObject(ObjModel);
                ResponseModel ObjResponse = GetApiResponse(Constant.ApiLogin, _request);

                if (String.IsNullOrWhiteSpace(ObjResponse.Response))
                {
                    return View("Index", ObjModel);

                }
                String VarResponse = ObjResponse.Response;
                string[] ArrResponse = VarResponse.Split(',');
                Services.SetCookie(this.ControllerContext.HttpContext, "usrId", ArrResponse[0]);
                Services.SetCookie(this.ControllerContext.HttpContext, "usrName", ArrResponse[1]);

                ViewBag.LoginMessage = "Login Success";
               return RedirectToAction("Index", "Interest");
            }
            else
            {
                ViewBag.OtpMessage = OtpCheck;
                ViewBag.LoginMessage = "OtpNot";
            }
            return View("OTP");
        }
        private ResponseModel GetApiResponse(string Url, String Data)
        {
            var _response = Services.GetApiResponseJson(Url, "POST", Data);

            ResponseModel _data = JsonConvert.DeserializeObject<ResponseModel>(_response);
            ResponseModel ObjResponse = JsonConvert.DeserializeObject<ResponseModel>(_data.Response);
            return ObjResponse;
        }

        public JsonResult GetOtp(UserRegisterModel ObjModel)
        {
            if (String.IsNullOrWhiteSpace(ObjModel.Mobile))
                return Json("Mobile No Not Blank");
            // if (ModelState.IsValid)
            {
                var _request = JsonConvert.SerializeObject(ObjModel);
                ResponseModel ObjResponse = GetApiResponse(Constant.ApiRegister, _request);
                if (String.IsNullOrWhiteSpace(ObjResponse.Response))
                {
                    return Json("Please Fill Mobile No");

                }
                String Response = "[{\"Response\":\"" + ObjResponse.Response + "\"}]";
                ViewBag.OtpMessage = ObjResponse.Response;
                Session["OtpData"] = ObjResponse.Response;
                return Json(Response);
            }
        }
    }
}