using Newtonsoft.Json;
using PrivatesquaresWebApiNew.Models;
using PrivateSquareWeb.CommonCls;
using PrivateSquareWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrivateSquareWeb.Controllers.User
{
    public class DashboardController : Controller
    {
        JwtTokenManager _JwtTokenManager = new JwtTokenManager();
        static List<InterestModel> ListInterest;
        // GET: Dashboard
        public ActionResult Index()
        {

            return View();
        }
        public List<InterestCategoryModel> GetInterestCategory()
        {
            var ProductCategoryList = new List<InterestCategoryModel>();
            // var _request = "";//_JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(loginModel));
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetAllInterestCategory, "");
            ProductCategoryList = JsonConvert.DeserializeObject<List<InterestCategoryModel>>(ObjResponse.Response);
            return ProductCategoryList;

        }

        public List<InterestModel> GetAllInterest()
        {
            var InterestList = new List<InterestModel>();
            // var _request = "";//_JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(loginModel));
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetAllInterest, "");
            InterestList = JsonConvert.DeserializeObject<List<InterestModel>>(ObjResponse.Response);
            return InterestList;
        }
        public JsonResult GetCateWiseInterest(long CatId)
        {
            List<InterestModel> FilterInterestModelsList = ListInterest.Where(i => i.InterestCatId == CatId).ToList();
            var JsonResponse = Json(FilterInterestModelsList);
            return JsonResponse;
        }
        public List<InterestModel> GetCateWiseInterestEdit()
        {
            UserInterestModel objModel = new UserInterestModel();
            LoginModel MdUser = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager);

            if (MdUser.Id != 0)
                objModel.UserId = Convert.ToInt64(MdUser.Id);
            var InterestList = new List<InterestModel>();
            var _request = JsonConvert.SerializeObject(objModel);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetUserInterest, _request);
            InterestList = JsonConvert.DeserializeObject<List<InterestModel>>(ObjResponse.Response);
            return InterestList;

        }

        public List<UserProfileModel> GetUserProfile()
        {

            var GetUserProfile = new List<UserProfileModel>();
            UserProfileModel objUserProfile = new UserProfileModel();
            LoginModel MdUser = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager);

            if (MdUser.Id != 0)
                objUserProfile.UserId = Convert.ToInt64(MdUser.Id);
            var _request = JsonConvert.SerializeObject(objUserProfile);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetProfile, _request);
            string jsonResult = ObjResponse.Response;
            GetUserProfile = JsonConvert.DeserializeObject<List<UserProfileModel>>(jsonResult);

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
            LoginModel MdUser = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager);
            if (MdUser.Id != 0)
                objProduct.UserId = Convert.ToInt64(MdUser.Id);
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
                LoginModel MdUser = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager);

                if (MdUser.Id != 0)
                    ObjProductModel.UserId = Convert.ToInt64(MdUser.Id);

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
            ListInterest = GetAllInterest();
            var InterestCategoryList = GetInterestCategory();
            ViewBag.InterestCategoryList = new SelectList(InterestCategoryList, "Id", "Name");

            List<InterestCategoryModel> CatwiseInterest = new List<InterestCategoryModel>();
            ViewBag.CatwiseInterestList = new SelectList(CatwiseInterest, "Id", "Name");


            var listProfession = CommonFile.GetProfession();
            ViewBag.ProfessionList = new SelectList(listProfession, "Id", "Name");

            List<InterestModel> ListInterestUser = GetCateWiseInterestEdit();
            ViewBag.ListInterestUser = new SelectList(ListInterestUser, "InterestId", "InterestName");


            bindCountryStateCity();
            UserProfileModel objModel = new UserProfileModel();
            List<UserProfileModel> UserProfile = GetUserProfile();

            if (UserProfile == null)
            {
                LoginModel MdUser = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager);

                if (MdUser.Id != 0)
                {
                    objModel.FirstName = MdUser.Name;
                    objModel.UserId = Convert.ToInt64(MdUser.Id);
                }
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
                objModel.OfficeAddress = UserProfile[0].OfficeAddress;
                objModel.OtherAddress = UserProfile[0].OtherAddress;
                objModel.InterestCatId = UserProfile[0].InterestCatId;
                int[] userInterest = Array.ConvertAll(UserProfile[0].StrUserInterestIds.Split(','), int.Parse);
                UserProfile[0].UserInterestIds = userInterest;
                objModel.UserInterestIds = UserProfile[0].UserInterestIds;
                if (!string.IsNullOrWhiteSpace(UserProfile[0].StrUserAddress))
                {
                    string[] ArrUserAddress = UserProfile[0].StrUserAddress.Split('^');
                    ViewBag.UserAddress = ArrUserAddress;
                }
                else
                {
                    
                    ViewBag.UserAddress = "";
                }
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
            LoginModel MdUser = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager);

            if (MdUser.Id != 0)
                objUserProfile.UserId = Convert.ToInt64(MdUser.Id);

            var _request = _JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(objUserProfile));
            ResponseModel ObjResponse = CommonFile.GetApiResponseJWT(Constant.ApiGetBusinessDetail, _request);
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
                objModel.Website = EditBusinessList[0].Website;
                // objModel.StateId = EditBusinessList[0].StateId;

            }
            return View("MyBusiness", objModel);
        }

        [HttpPost]
        public ActionResult SaveProfile(FormCollection frmColl, UserProfileModel objModel)
        {
            String Address = frmColl["txtAddress"];
            objModel.Location = Address;
            String StrDob = frmColl["DOB"];
            DateTime Dob = DateTime.ParseExact(StrDob, "dd/MM/yyyy", null);
            objModel.DOB = Dob;
             //if (ModelState.IsValid)
            {
                
              
                HttpPostedFileBase FileUpload = Request.Files["FileUploadImage"];
                String FileName = SaveImage(FileUpload);

                objModel.Id = 0;
                LoginModel MdUser = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager);

                if (MdUser.Id != 0)
                {
                    objModel.UserId = Convert.ToInt64(MdUser.Id);
                    objModel.Phone = MdUser.Mobile;
                }
                objModel.ProfileImage = FileName;

                #region  Code For DataSet To Json 
                DataTable dt = new DataTable();
                dt.Clear();
                dt.Columns.Add("UserId");
                dt.Columns.Add("InterestId");
                dt.Columns.Add("InterestCatId");
                int[] Arr_Interest = objModel.UserInterestIds;
                for (int i = 0; i < Arr_Interest.Length; i++)
                {
                    DataRow NewDataRow;
                    NewDataRow = dt.NewRow();
                    NewDataRow["UserId"] = MdUser.Id;
                    NewDataRow["InterestId"] = Arr_Interest[i];
                    NewDataRow["InterestCatId"] = "0";
                    dt.Rows.Add(NewDataRow);
                }
                // Add a Root Object Name
                var collectionWrapper = new
                {
                    Interest = dt
                };
                string JSONresult;
                JSONresult = JsonConvert.SerializeObject(collectionWrapper);
                #endregion
                var data = JsonConvert.DeserializeObject(JSONresult);
                var xmlNode = JsonConvert.DeserializeXmlNode(data.ToString(), "root").OuterXml;
                objModel.XmlData = xmlNode;
                String[] ArrayAddress = objModel.Location.Split(',');
                #region  Code For DataSet To Json For Address 
                DataTable dtAddress = new DataTable();
                dtAddress.Clear();
                dtAddress.Columns.Add("UserId");
                dtAddress.Columns.Add("Address");
                //int[] ArrayAddress = objModel.UserInterestIds;
                for (int i = 0; i < ArrayAddress.Length; i++)
                {
                    if(!String.IsNullOrWhiteSpace(ArrayAddress[i]))
                    { 
                    DataRow NewDataRow;
                    NewDataRow = dtAddress.NewRow();
                    NewDataRow["UserId"] = MdUser.Id;
                    NewDataRow["Address"] = ArrayAddress[i];
                    dtAddress.Rows.Add(NewDataRow);
                    }
                }
                // Add a Root Object Name
                var collectionWrapperAddress = new
                {
                    Location = dtAddress
                };
                string JSONAddressResult;
                JSONAddressResult = JsonConvert.SerializeObject(collectionWrapperAddress);
                #endregion
                #region  Code For DataSet To Xml For Address 
                var dataAddress = JsonConvert.DeserializeObject(JSONAddressResult);
                var xmlNodeAddress = JsonConvert.DeserializeXmlNode(dataAddress.ToString(), "root").OuterXml;
                objModel.XmlDataAddress = xmlNodeAddress;
                #endregion


                var _request = _JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(objModel));
                ResponseModel ObjResponse = CommonFile.GetApiResponseJWT(Constant.ApiSaveProfile, _request);
                String VarResponse = ObjResponse.Response;
                if (String.IsNullOrWhiteSpace(ObjResponse.Response))
                {
                    return View("Index", objModel);

                }
                //String UserName = string.Concat(objModel.FirstName, " ", objModel.LastName);
                // Services.SetCookie(this.ControllerContext.HttpContext, "usrName", UserName);
                //HeaderPartialModel objHeaderModel = new HeaderPartialModel();
                //objHeaderModel.UserName = UserName;
                return RedirectToAction("MyBusinessList", "Home");
            }
            return View("PersonalProfile", objModel);
        }
        [HttpPost]
        public ActionResult SaveBussiness(BusinessModel objModel)
        {
            var listProfession = CommonFile.GetProfession();
            ViewBag.ProfessionList = new SelectList(listProfession, "Id", "Name");
            bindCountryStateCity();
            if (string.IsNullOrWhiteSpace((objModel.Id).ToString()))
                objModel.Id = 0;

            if (ModelState.IsValid)
            {
                HttpPostedFileBase FileUpload = Request.Files["FileUploadImage"];
                String FileName = SaveImage(FileUpload);
                //BusinessModel objModel = new BusinessModel();
                //objModel.Id = 0;
                LoginModel MdUser = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager);

                if (MdUser.Id != 0)
                    objModel.UserId = Convert.ToInt64(MdUser.Id);
                objModel.BusinessLogo = FileName;
                if (objModel.Id != 0)
                    objModel.Operation = "update";
                else if (objModel.Id == 0)
                    objModel.Operation = "insert";

                //var _request = JsonConvert.SerializeObject(objModel);
                var _request = _JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(objModel));
                ResponseModel ObjResponse = CommonFile.GetApiResponseJWT(Constant.ApiSaveBusiness, _request);
                String VarResponse = ObjResponse.Response;
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
            if (string.IsNullOrWhiteSpace(FileUpload.FileName))
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