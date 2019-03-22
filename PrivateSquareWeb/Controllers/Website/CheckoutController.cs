using Newtonsoft.Json;
using PrivateSquareWeb.CommonCls;
using PrivateSquareWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
                PreRequiestCheckout();
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
        private void bindCountryStateCity()
        {

            //var CountryList = CommonFile.GetCountry();

            //ViewBag.CountryList = new SelectList(CountryList, "Id", "Name");

            var StateList = CommonFile.GetState();

            ViewBag.StateList = new SelectList(StateList, "Id", "Name");

            var CityList = CommonFile.GetCity();

            ViewBag.CityList = new SelectList(CityList, "Id", "Name");



        }
        [HttpPost]
        public ActionResult AddressesList(AddressModel ObjModel)

        {
            // List<AddressModel> ListUserAddress;
            // ListUserAddress = GetUserAddress(null);
            LoginModel MdUser = Services.GetLoginWebUser(this.ControllerContext.HttpContext, _JwtTokenManager);

            if (ModelState.IsValid)
            {
                //if (!CommonFile.IsUserAuthenticate(this.ControllerContext.HttpContext))
                //{
                //    return RedirectToAction("Index", "Login");
                //}
                if (ObjModel.Id == null)
                    ObjModel.Id = 0;
                if (MdUser.Id != 0)
                {
                    ObjModel.UserId = Convert.ToInt64(MdUser.Id);
                }
                if (ObjModel.Id != 0)
                {
                    ObjModel.Operation = "update";
                }
                else
                {
                    ObjModel.Operation = "insert";
                }

                var _request = JsonConvert.SerializeObject(ObjModel);
                ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiSaveAddress, _request);
                if (String.IsNullOrWhiteSpace(ObjResponse.Response))

                {
                    @ViewBag.ResponseMessage = "Error ! Unable to Submit Address";
                    PreRequiestCheckout();
                    return View("AddAddress", ObjModel);
                }

                if (ObjResponse.Response.Equals("Save Address"))
                {
                    //@ViewBag.ResponseMessage = "Your Current Password is Wrong";
                    //ViewBag.UserAddress = ListUserAddress;
                    PreRequiestCheckout();
                    return View("Index", ObjModel);
                }
            }

            else
            {
                PreRequiestCheckout();

            }
            // ViewBag.UserAddress = ListUserAddress;
            return View("Index", ObjModel);

        }
        private void PreRequiestCheckout()
        {
            LoginModel MdUser = Services.GetLoginWebUser(this.ControllerContext.HttpContext, _JwtTokenManager);

            AddToCart objAddToCart = new AddToCart();
            ViewBag.LoginEmail = MdUser.EmailId;
            ViewBag.ItemCount = objAddToCart.GetItemCount(this.ControllerContext.HttpContext);
            ViewBag.TotalAmount = objAddToCart.GetTotalAmountCheckOut(this.ControllerContext.HttpContext);
            if (ViewBag.TotalAmount < 500)
            {
                ViewBag.ShippingCharges = 50;                       //setting shipping/delivery charges
                ViewBag.TotalAmountAfterCharges = ViewBag.TotalAmount + ViewBag.ShippingCharges;
            }
            else
            {
                ViewBag.ShippingCharges = 0;
                ViewBag.TotalAmountAfterCharges = ViewBag.TotalAmount;
            }
            List<AddressModel> UserAddressList = GetUserAddress();
            ViewBag.UserAddress = UserAddressList;
            bindCountryStateCity();
        }
        public static decimal totalamount;
        [HttpPost]
        public JsonResult PlaceOrder(long AddressId, string PaymentMode)
        {
            LoginModel MdUser = Services.GetLoginWebUser(this.ControllerContext.HttpContext, _JwtTokenManager);
            SaleOrderModel objModel = new SaleOrderModel();
            List<AddToCartModel> ListAddToCart = Services.GetMyCart(this.ControllerContext.HttpContext, _JwtTokenManager);

            objModel = GetSaleOrderValues(ListAddToCart);
            objModel.Operation = "insert";
            if (totalamount != 0)
            {

                objModel.TotalAmount = totalamount;
                SaveCouponHistory(objModel);
            }
            objModel.PaymentMode = PaymentMode;
            objModel.UserId = MdUser.Id;
            var _request = JsonConvert.SerializeObject(objModel);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiSaveOrders, _request);

            if (ObjResponse.Response.Equals("Order Saved"))
            {
                Services.RemoveCookie(this.ControllerContext.HttpContext, "addtocart");
            }
            String Response = "[{\"Response\":\"" + ObjResponse.Response + "\"}]";
            return Json(Response);
        }

        private SaleOrderModel GetSaleOrderValues(List<AddToCartModel> ListCart)
        {
            decimal TotalAmount = 0;
            decimal TotalDiscount = 0;
            for (int i = 0; i < ListCart.Count; i++)
            {
                TotalAmount += ListCart[i].Amount;
                TotalDiscount += ListCart[i].Discount;
            }
            SaleOrderModel objModel = new SaleOrderModel();
            objModel.TotalAmount = TotalAmount;
            objModel.TotalDiscount = TotalDiscount;
            objModel.XmlSaleOrderDetail = ListToXml(ListCart);

            return objModel;
        }
        private String ListToXml(List<AddToCartModel> ListCart)
        {
            LoginModel MdUser = Services.GetLoginWebUser(this.ControllerContext.HttpContext, _JwtTokenManager);

            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("UserId");
            dt.Columns.Add("ProductId");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Discount");
            dt.Columns.Add("Amount");

            for (int i = 0; i < ListCart.Count; i++)
            {
                DataRow NewDataRow;
                NewDataRow = dt.NewRow();
                NewDataRow["UserId"] = MdUser.Id;
                NewDataRow["ProductId"] = ListCart[i].ProductId;
                NewDataRow["Quantity"] = ListCart[i].Qty;
                NewDataRow["Discount"] = ListCart[i].Discount;
                NewDataRow["Amount"] = ListCart[i].Amount;
                dt.Rows.Add(NewDataRow);
            }
            var collectionWrapperAddress = new
            {
                SaleOrderDetail = dt
            };
            string JSONAddressResult;
            JSONAddressResult = JsonConvert.SerializeObject(collectionWrapperAddress);
            #region  Code For DataSet To Xml For Address 
            var dataAddress = JsonConvert.DeserializeObject(JSONAddressResult);
            var xmlNodeSaleOrder = JsonConvert.DeserializeXmlNode(dataAddress.ToString(), "root").OuterXml;

            #endregion
            return xmlNodeSaleOrder;
        }

        [HttpPost]
        public ActionResult GetCoupon(string CouponCode)
        {
            CouponModel ObjCouponModel = new CouponModel();
            LoginModel MdUser = Services.GetLoginWebUser(this.ControllerContext.HttpContext, _JwtTokenManager);
            if (MdUser.Id != 0)
                ObjCouponModel.UserId = Convert.ToInt64(MdUser.Id);
            ObjCouponModel.CouponCode = CouponCode;
            var _request = JsonConvert.SerializeObject(ObjCouponModel);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetCoupon, _request);
            var ObjResponse1 = JsonConvert.DeserializeObject<List<ResponseModel>>(ObjResponse.Response);
            var couponresponse = ObjResponse1[0].Response.ToString();
            if (couponresponse.Equals("already used"))
            {
                ViewBag.CouponResponse = "Coupon already used";
                return JavaScript("alert('coupon already used')");
            }
            if (couponresponse.Equals("coupon expired"))
            {
                ViewBag.CouponResponse = "coupon expired";
                return JavaScript("alert('Coupon Expired')");
            }
            if (couponresponse.Equals("coupon does not exist"))
            {
                ViewBag.CouponResponse = "coupon does not exist";
                return JavaScript("alert('Coupon does not exist')");
            }
            else
            {
                string[] ArrResponse = couponresponse.Split(',');
                if (ArrResponse[3] == "Discount")                   //if coupon type is "discount"
                {

                    ViewBag.CouponResponse = ArrResponse[6];

                    AddToCart objAddToCart = new AddToCart();
                    totalamount = objAddToCart.GetTotalAmountCheckOut(this.ControllerContext.HttpContext);
                    if (totalamount < Convert.ToInt64(ArrResponse[4]))
                    {
                        return JavaScript("alert('Cart value is not sufficient')");
                    }
                    ViewBag.TotalAmount = totalamount - Convert.ToInt64(ArrResponse[2]);
                    if (ViewBag.TotalAmount <= 500)
                    {
                        ViewBag.TotalAmount += 50;                          //Adding shipping/delivery charges
                        ViewBag.TotalAmount -= (2 * (ViewBag.TotalAmount));  //making the total negative for handling it securely in client side
                        ViewBag.TotalAmountAfterCharges = ViewBag.TotalAmount;
                    }
                    //else
                    //{
                    //    ViewBag.TotalAmount = TotalAmount;
                    //}
                    totalamount = ViewBag.TotalAmount;
                    SaleOrderModel objmodel = new SaleOrderModel();
                    return Json(ViewBag.TotalAmount);
                }
                else if (ArrResponse[3] == "BOGO")        //Condition for Coupon Type=="BOGO" (Buy One Get One)
                {
                    return JavaScript("alert('Coupon does not exist')");
                }
                return JavaScript("alert('Error')");

            }
        }

        private void SaveCouponHistory(SaleOrderModel couponModel)
        {
            CouponModel ObjcouponModel = new CouponModel();
            LoginModel MdUser = Services.GetLoginWebUser(this.ControllerContext.HttpContext, _JwtTokenManager);
            if (MdUser.Id != 0)
            { ObjcouponModel.UserId = MdUser.Id; }
            ObjcouponModel.CouponId = 1;
            //ObjcouponModel.UserId = 15;
            ObjcouponModel.OrderId = 15;
            ObjcouponModel.Discount = 100;
            ObjcouponModel.Operation = "insert";
            var _request = JsonConvert.SerializeObject(ObjcouponModel);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiSaveCouponHistory, _request);
            if (ObjResponse.Response.Equals("Coupon History Saved"))
            {
                //return "Coupon History Saved";
            }

            //return "error saving coupon history";
        }

        
        public ActionResult OrderSuccessful()
        {
            return View();
        }
    }
}