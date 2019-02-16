using Newtonsoft.Json;
using PrivateSquareWeb.CommonCls;
using PrivateSquareWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrivateSquareWeb.Controllers
{
    public class ChangePasswordController : Controller
    {
        JwtTokenManager _JwtTokenManager = new JwtTokenManager();

        // GET: ChangePassword
        public ActionResult Index()
        {
            return View();

        }
        [HttpPost]
        public ActionResult ChangePassword(LoginModel ObjModel)
        {
            if (ModelState.IsValid)
            {
                LoginModel MdUser = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager);
                if (MdUser.Id != 0)
                {
                    ObjModel.Id = Convert.ToInt64(MdUser.Id);
                }
                string PasswordEncripy = CommonFile.EncodePasswordMd5(ObjModel.NewPassword);
                string PasswordEncripy2 = CommonFile.EncodePasswordMd5(ObjModel.Password);
                ObjModel.ConfirmNewPassword = PasswordEncripy;
                ObjModel.NewPassword = PasswordEncripy;
                ObjModel.Password = PasswordEncripy2;
                var _request = JsonConvert.SerializeObject(ObjModel);
                ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiChangePassword, _request);
                ResponseModel ObjResponse1 = JsonConvert.DeserializeObject<ResponseModel>(ObjResponse.Response);
                @ViewBag.ResponseMessage = ObjResponse1.Response;
                if (String.IsNullOrWhiteSpace(ObjResponse.Response))
                {
                    return View("Index", ObjModel);
                }
                Services.RemoveCookie(this.ControllerContext.HttpContext, "usr");
                return RedirectToAction("Index", "Login");
            }

            return View("Index", ObjModel);
        }
    }
}