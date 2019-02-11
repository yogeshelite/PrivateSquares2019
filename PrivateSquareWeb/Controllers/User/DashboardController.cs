using Newtonsoft.Json;
using PrivateSquareWeb.CommonCls;
using PrivateSquareWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrivateSquareWeb.Controllers.User
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }
        public List<UserProfileModel> GetUserProfile()
        {
            var GetUserProfile = new List<UserProfileModel>();
            UserProfileModel objUserProfile = new UserProfileModel();
            objUserProfile.UserId = Convert.ToInt64(Services.GetCookie(this.ControllerContext.HttpContext, "usrId").Value);
            var _request = JsonConvert.SerializeObject(objUserProfile);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetProfile, _request);
            GetUserProfile = JsonConvert.DeserializeObject<List<UserProfileModel>>(ObjResponse.Response);
            return GetUserProfile;

        }

        public ActionResult Product()
        {
            //List<DropDownModel> listDrop = new List<DropDownModel>();
            //ViewBag.ProductCatList = listDrop;

            var ProductCatList = CommonFile.GetProductCategory();
            ViewBag.ProductCatList = new SelectList(ProductCatList, "Id", "Name");
            ProductModel objModel = new ProductModel();
            return View(objModel);
        }
        public List<ProductModel> GetProduct(long Id)
        {
            var GetProduct = new List<ProductModel>();
            ProductModel objProduct = new ProductModel();
            objProduct.Id = Id;
            objProduct.UserId = Convert.ToInt64(Services.GetCookie(this.ControllerContext.HttpContext, "usrId").Value);
            var _request = JsonConvert.SerializeObject(objProduct);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetProductDetail, _request);
            GetProduct = JsonConvert.DeserializeObject<List<ProductModel>>(ObjResponse.Response);
            return GetProduct;

        }
        public ActionResult EditProduct(long Id)
        {
            var ProductCatList = CommonFile.GetProductCategory();
            ViewBag.ProductCatList = new SelectList(ProductCatList, "Id", "Name");
            List<ProductModel> Product = GetProduct(Id);
            ProductModel objModel = new ProductModel();
            if (Product != null && Product.Count() > 0)
            {
                objModel.Id = Id;
                objModel.ProductName = Product[0].ProductName;
                objModel.ProductCatId = Product[0].ProductCatId;
                objModel.ProductImage = Product[0].ProductImage;
                objModel.SellingPrice = Product[0].SellingPrice;
                objModel.DiscountPrice = Product[0].DiscountPrice;
                objModel.BusinessId = Product[0].BusinessId;
                objModel.UserId = Product[0].UserId;
                objModel.Description = Product[0].Description;

            }
            return View("Product", objModel);
        }
        [HttpPost]
        public ActionResult SaveProduct(FormCollection frmColl, ProductModel ObjProductModel)
        {
            var ProductCatList = CommonFile.GetProductCategory();

            ViewBag.ProductCatList = new SelectList(ProductCatList, "Id", "Name");

            if (ModelState.IsValid)
            {
                HttpPostedFileBase FileUpload = Request.Files["FileUploadImage"];

                String FileName = SaveImage(FileUpload);

                ObjProductModel.ProductImage = FileName;
                ObjProductModel.UserId = Convert.ToInt64(Services.GetCookie(this.ControllerContext.HttpContext, "usrId").Value);

                if (ObjProductModel.Id == 0)
                    ObjProductModel.Operation = "insert";
                else if (ObjProductModel.Id != 0)
                    ObjProductModel.Operation = "Update";

                var _request = JsonConvert.SerializeObject(ObjProductModel);
                ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiSaveProduct, _request);
                if (String.IsNullOrWhiteSpace(ObjResponse.Response))
                {
                    return View("Product", ObjProductModel);

                }
                return RedirectToAction("ProductList", "Home");
            }
            return View("Product", ObjProductModel);
        }
        public ActionResult PersonalProfile()
        {
            var listProfession = CommonFile.GetProfession();
            ViewBag.ProfessionList = new SelectList(listProfession, "Id", "Name");

            bindCountryStateCity();
            UserProfileModel objModel = new UserProfileModel();
            List<UserProfileModel> UserProfile = GetUserProfile();

            if (UserProfile == null)
            {

            }
            else if (UserProfile != null && UserProfile.Count() > 0)
            {

                objModel.FirstName = UserProfile[0].FirstName;
                objModel.LastName = UserProfile[0].LastName;
                objModel.Location = UserProfile[0].Location;
                objModel.DOB = UserProfile[0].DOB;
                objModel.ProfessionalCatId = UserProfile[0].ProfessionalCatId;
                objModel.Pincode = UserProfile[0].Pincode;
                objModel.EmailId = UserProfile[0].EmailId;
                objModel.Description = UserProfile[0].Description;
                objModel.Phone = UserProfile[0].Phone;
                objModel.CountryId = UserProfile[0].CountryId;
                objModel.Title = UserProfile[0].Title;
                objModel.ProfessionalKeyword = UserProfile[0].ProfessionalKeyword;
                objModel.ProfileImage = UserProfile[0].ProfileImage;
                
            }
            return View(objModel);
        }

        public ActionResult MyBusiness()
        {
            var listProfession = CommonFile.GetProfession();
            ViewBag.ProfessionList = new SelectList(listProfession, "Id", "Name");
            bindCountryStateCity();

            return View();
        }
        public List<BusinessModel> GetBusiness(long Id)
        {
            var GetBusiness = new List<BusinessModel>();
            BusinessModel objUserProfile = new BusinessModel();
            objUserProfile.Id = Id;
            objUserProfile.UserId = Convert.ToInt64(Services.GetCookie(this.ControllerContext.HttpContext, "usrId").Value);
            var _request = JsonConvert.SerializeObject(objUserProfile);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetBusiness, _request);
            GetBusiness = JsonConvert.DeserializeObject<List<BusinessModel>>(ObjResponse.Response);
            return GetBusiness;

        }
        public ActionResult EditMyBusiness(long Id)
        {

            var listProfession = CommonFile.GetProfession();
            ViewBag.ProfessionList = new SelectList(listProfession, "Id", "Name");
            bindCountryStateCity();




            List<BusinessModel> EditBusinessList = GetBusiness(Id);
            BusinessModel objModel = new BusinessModel();
            if (EditBusinessList != null && EditBusinessList.Count() > 0)
            {
                objModel.Id = Id;
                objModel.BusinessName = EditBusinessList[0].BusinessName;
                objModel.BusinessLogo = EditBusinessList[0].BusinessLogo;
                objModel.Location = EditBusinessList[0].Location;
                objModel.ProfessionalCatId = EditBusinessList[0].ProfessionalCatId;
                objModel.ProfessionalKeyword = EditBusinessList[0].ProfessionalKeyword;
                objModel.CityId = EditBusinessList[0].CityId;
                objModel.PinCode = EditBusinessList[0].PinCode;
                objModel.UserId = EditBusinessList[0].UserId;
                objModel.Email = EditBusinessList[0].Email;
                objModel.Description = EditBusinessList[0].Description;
                objModel.CountryId = EditBusinessList[0].CountryId;
                objModel.Phone = EditBusinessList[0].Phone;
                objModel.StateId = EditBusinessList[0].StateId;

            }
            return View("MyBusiness", objModel);
        }

        [HttpPost]
        public ActionResult SaveProfile(FormCollection frmColl, UserProfileModel objModel)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase FileUpload = Request.Files["FileUploadImage"];
                String FileName = SaveImage(FileUpload);
                
                objModel.Id = 0;
                objModel.UserId = Convert.ToInt64(Services.GetCookie(this.ControllerContext.HttpContext, "usrId").Value);
                objModel.Phone = Services.GetCookie(this.ControllerContext.HttpContext, "usrName").Value;
                objModel.ProfileImage = FileName;
                var _request = JsonConvert.SerializeObject(objModel);
                ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiSaveProfile, _request);
                if (String.IsNullOrWhiteSpace(ObjResponse.Response))
                {
                    return View("Index", objModel);

                }
                String UserName = string.Concat(objModel.FirstName, " ", objModel.LastName);
                Services.SetCookie(this.ControllerContext.HttpContext, "usrName", UserName);
                HeaderPartialModel objHeaderModel = new HeaderPartialModel();
                objHeaderModel.UserName = UserName;
                return RedirectToAction("MyBusinessList", "Home");
            }
            return View("PersonalProfile");
        }
        [HttpPost]
        public ActionResult SaveBussiness(FormCollection frmColl, BusinessModel objModel)
        {
            var listProfession = CommonFile.GetProfession();

            ViewBag.ProfessionList = new SelectList(listProfession, "Id", "Name");

            bindCountryStateCity();

            if (ModelState.IsValid)
            {
                HttpPostedFileBase FileUpload = Request.Files["FileUploadImage"];
                String FileName = SaveImage(FileUpload);
                //BusinessModel objModel = new BusinessModel();
                //objModel.Id = 0;
                objModel.UserId = Convert.ToInt64(Services.GetCookie(this.ControllerContext.HttpContext, "usrId").Value);
                objModel.BusinessLogo = FileName;
                if (objModel.Id != 0)
                    objModel.Operation = "update";
                else if (objModel.Id == 0)
                    objModel.Operation = "insert";

                //objModel.ProfessionalCatId = Convert.ToInt64(frmColl["ddlProfessionalCat"]);
                //objModel.CountryId = Convert.ToInt64(frmColl["country"]);
                //objModel.CityId = Convert.ToInt64(frmColl["city"]);
                //objModel.StateId = Convert.ToInt64(frmColl["state"]);

                //objModel.BusinessName = frmColl["businessname"];
                //objModel.Location = frmColl["address"];
                //objModel.ProfessionalKeyword = frmColl["Keywords"];
                //objModel.PinCode = frmColl["Pincode"];
                //objModel.Email = frmColl["email"];
                //objModel.Description = frmColl["description"];
                //objModel.Phone = frmColl["phone"];
                var _request = JsonConvert.SerializeObject(objModel);
                ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiSaveBusiness, _request);
                if (String.IsNullOrWhiteSpace(ObjResponse.Response))
                {
                    return View("MyBusiness", objModel);

                }

                return RedirectToAction("MyBusinessList", "Home");
            }
            return View("MyBusiness", objModel);
        }

        private void bindCountryStateCity()
        {
            var CountryList = CommonFile.GetCountry();
            ViewBag.CountryList = new SelectList(CountryList, "Id", "Name");
            var StateList = CommonFile.GetState();
            ViewBag.StateList = new SelectList(StateList, "Id", "Name");
            var CityList = CommonFile.GetCity();
            ViewBag.CityList = new SelectList(CityList, "Id", "Name");

        }

        private String SaveImage(HttpPostedFileBase FileUpload)
        {
            if (string.IsNullOrWhiteSpace(FileUpload.FileName ))
                return null;
            string filename = FileUpload.FileName;
            string targetpath = Server.MapPath("~/DocImg/");
            string Extention = Path.GetExtension(filename);
            string DynamicFileName = Guid.NewGuid().ToString() + Extention;
            FileUpload.SaveAs(targetpath + DynamicFileName);
            return DynamicFileName;
        }

    }
}