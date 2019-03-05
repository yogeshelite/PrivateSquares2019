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
            List<AddressModel> UserAddressList = GetUserAddress();
            ViewBag.UserAddress = UserAddressList;
            bindCountryStateCity();
        }

        [HttpPost]
        public JsonResult PlaceOrder(long AddressId,string PaymentMode)
        {
            LoginModel MdUser = Services.GetLoginWebUser(this.ControllerContext.HttpContext, _JwtTokenManager);
            SaleOrderModel objModel = new SaleOrderModel();
            List<AddToCartModel> ListAddToCart = Services.GetMyCart(this.ControllerContext.HttpContext, _JwtTokenManager);

            objModel = GetSaleOrderValues(ListAddToCart);
            objModel.Operation = "insert";
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
    }
}