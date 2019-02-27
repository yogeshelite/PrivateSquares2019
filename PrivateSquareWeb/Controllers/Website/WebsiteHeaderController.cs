using PrivateSquareWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrivateSquareWeb.Controllers.Website
{
    public class WebsiteHeaderController : Controller
    {
        JwtTokenManager _JwtTokenManager = new JwtTokenManager();
        // GET: WebsiteHeader
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult HeaderValue()
        {
            HeaderPartialModel objModel = new HeaderPartialModel();
            LoginModel MdUser = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager);
            List<AddToCartModel> ListAddToCart = Services.GetMyCart(this.ControllerContext.HttpContext, _JwtTokenManager);
           
                objModel.UserName = MdUser.Name;
                objModel.UserId = Convert.ToInt64(MdUser.Id);
                objModel.ProfileImg = MdUser.ProfileImg;
                objModel.CartItemCount = ListAddToCart.Count();
           
            return PartialView("~/Views/Shared/_WebsiteHeader.cshtml", objModel);
        }
    }
}