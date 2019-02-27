using PrivateSquareWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrivateSquareWeb.Controllers.Website
{
    public class CheckoutController : Controller
    {
        JwtTokenManager _JwtTokenManager = new JwtTokenManager();
        // GET: Checkout
        public ActionResult Index()
        {
            LoginModel MdUser = Services.GetLoginWebUser(this.ControllerContext.HttpContext, _JwtTokenManager);
            if (MdUser.Id == 0)
                return RedirectToAction("Index", "WebLogin");
            else
            {
                AddToCart objAddToCart = new AddToCart();
                ViewBag.LoginEmail = MdUser.EmailId;
                ViewBag.ItemCount = objAddToCart.GetItemCount(this.ControllerContext.HttpContext);
                ViewBag.TotalAmount= objAddToCart.GetTotalAmountCheckOut(this.ControllerContext.HttpContext);
            }
            return View();
        }
    }
}