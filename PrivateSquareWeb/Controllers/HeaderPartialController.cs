using PrivateSquareWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrivateSquareWeb.Controllers
{
    public class HeaderPartialController : Controller
    {
        // GET: HeaderPartial
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult HeaderValue()
        {
            HeaderPartialModel objModel = new HeaderPartialModel();
            // UserModel MdUser = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager);
            if (Services.GetCookie(this.ControllerContext.HttpContext, "usrName") != null)
            {
                objModel.UserName = Services.GetCookie(this.ControllerContext.HttpContext, "usrName").Value;
                objModel.UserId = Convert.ToInt64(Services.GetCookie(this.ControllerContext.HttpContext, "usrId").Value);
            }
            else
            {

            }
            return PartialView("~/Views/Shared/_Header.cshtml", objModel);
        }
    }
}