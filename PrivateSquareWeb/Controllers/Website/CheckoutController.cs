using Newtonsoft.Json;
using PrivateSquareWeb.CommonCls;
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
                List<AddressModel> UserAddressList = GetUserAddress();
                ViewBag.UserAddress = UserAddressList;
            }
            return View();
        }
        public List<AddressModel> GetUserAddress()
        {
            var GetUserAddressList = new List<AddressModel>();
            AddressModel objmodel = new AddressModel();
            LoginModel MdUser = Services.GetLoginWebUser(this.ControllerContext.HttpContext, _JwtTokenManager);
            if (MdUser.Id != 0)
                objmodel.UserId = Convert.ToInt64(MdUser.Id);
            var _request = JsonConvert.SerializeObject(objmodel);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetUserAddress, _request);
            GetUserAddressList = JsonConvert.DeserializeObject<List<AddressModel>>(ObjResponse.Response);
            return GetUserAddressList;

        }
    }
}