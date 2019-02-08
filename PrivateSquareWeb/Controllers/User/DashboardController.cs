﻿using Newtonsoft.Json;
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
            objUserProfile.UserId = Convert.ToInt64(Services.GetCookie(this.ControllerContext.HttpContext,"usrId").Value);
            var _request = JsonConvert.SerializeObject(objUserProfile);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetProfile, _request);
            GetUserProfile = JsonConvert.DeserializeObject<List<UserProfileModel>>(ObjResponse.Response);
            return GetUserProfile;

        }
        public List<DropDownModel> GetProfession()
        {
            var ProfessionList = new List<DropDownModel>();
            DropDownModel objUserProfile = new DropDownModel();
          //  var _request = JsonConvert.SerializeObject(objUserProfile);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetProfession,"");
            ProfessionList = JsonConvert.DeserializeObject<List<DropDownModel>>(ObjResponse.Response);
            return ProfessionList;
        }
        public ActionResult Product()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaveProduct( FormCollection frmColl)
        {
            HttpPostedFileBase FileUpload = Request.Files["FileUploadImage"];

            String FileName = SaveImage(FileUpload);
            ProductModel ObjProductModel = new ProductModel();
            ObjProductModel.Id = 0;
            ObjProductModel.ProductName = frmColl["productname"];
            ObjProductModel.ProductCatId= Convert.ToInt64(frmColl["category"]);
            ObjProductModel.ProductImage = FileName;
            ObjProductModel.SellingPrice = Convert.ToInt64(frmColl["sellingprice"]); 
            ObjProductModel.DiscountPrice = Convert.ToInt64(frmColl["discountPrice"]);
           // ObjProductModel.Description = frmColl["description"];
            ObjProductModel.UserId = Convert.ToInt64(Services.GetCookie(this.ControllerContext.HttpContext,"usrId").Value);
            ObjProductModel.Operation = "insert";
            var _request = JsonConvert.SerializeObject(ObjProductModel);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiSaveProduct, _request);
            if (String.IsNullOrWhiteSpace(ObjResponse.Response))
            {
                return View("Product", ObjProductModel);

            }

            return RedirectToAction("MyBusinessList", "Home");
        }
        public ActionResult PersonalProfile()
        {
            List<UserProfileModel> UserProfile = GetUserProfile();
            var listProfession=GetProfession();
            ViewBag.ProfessionList = listProfession;
            UserProfileModel objModel = new UserProfileModel();
            if (UserProfile == null )
            {

            }
            else if(UserProfile == null && UserProfile.Count()>0)
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

                //FormCollection frmColl = new FormCollection();
                //frmColl.Add("firstname", objModel.FirstName);
                //frmColl.Add("lastname", objModel.LastName);
                //frmColl.Add("address", objModel.Location);
                //frmColl.Add("dob", objModel.DOB.ToString());
                //frmColl.Add("ddlProfessionalCat", objModel.ProfessionalCatId.ToString());
                //frmColl.Add("Keywords", objModel.ProfessionalKeyword);
                //frmColl.Add("Pincode", objModel.Pincode);
                //frmColl.Add("email", objModel.EmailId);
                //frmColl.Add("description", objModel.Description);
                //frmColl.Add("phone", objModel.Phone);
                //frmColl.Add("country", objModel.CountryId.ToString());
                
            }
            return View(objModel);
        }

        public ActionResult MyBusiness()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaveProfile(FormCollection frmColl)
        {
            HttpPostedFileBase FileUpload = Request.Files["FileUploadImage"];

            String FileName = SaveImage(FileUpload);


            UserProfileModel objModel = new UserProfileModel();
            objModel.Id = 0;
            objModel.UserId = Convert.ToInt64(Services.GetCookie(this.ControllerContext.HttpContext, "usrId").Value);
            objModel.Phone= Services.GetCookie(this.ControllerContext.HttpContext, "usrName").Value;
            objModel.FirstName = frmColl["firstname"];
            objModel.LastName = frmColl["lastname"];
            objModel.Location = frmColl["address"];
            objModel.DOB = Convert.ToDateTime(frmColl["dob"]);
            objModel.ProfessionalCatId = Convert.ToInt64(frmColl["ddlProfessionalCat"]);
            objModel.ProfessionalKeyword = frmColl["Keywords"];
            objModel.Pincode = frmColl["Pincode"];
            objModel.EmailId = frmColl["email"];
            objModel.Description = frmColl["description"];
            objModel.Phone = frmColl["phone"];
            objModel.CountryId = Convert.ToInt64(frmColl["country"]);
            objModel.ProfileImage = FileName;
            //objModel.Operation = "insert";
            var _request = JsonConvert.SerializeObject(objModel);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiSaveProfile, _request);
            if (String.IsNullOrWhiteSpace(ObjResponse.Response))
            {
                return View("Index", objModel);

            }

            return RedirectToAction("MyBusinessList", "Home");
            
        }
        [HttpPost]
        public ActionResult SaveBussiness(FormCollection frmColl, HttpPostedFileBase FileUpload1)
        {

            HttpPostedFileBase FileUpload = Request.Files["FileUploadImage"];

            String FileName = SaveImage(FileUpload);
           

            BusinessModel objModel = new BusinessModel();
            objModel.Id = 0;
            objModel.UserId = Convert.ToInt64(Services.GetCookie(this.ControllerContext.HttpContext,"usrId").Value);
            objModel.BusinessName = frmColl["businessname"];
            objModel.Location = frmColl["address"];
            objModel.ProfessionalCatId = Convert.ToInt64(frmColl["ddlProfessionalCat"]);
            objModel.ProfessionalKeyword = frmColl["Keywords"];
            objModel.PinCode = frmColl["Pincode"];
            objModel.Email = frmColl["email"]; 
            objModel.Description = frmColl["description"];
            objModel.Phone = frmColl["phone"]; 
            objModel.CountryId = Convert.ToInt64(frmColl["country"]) ;
            objModel.BusinessLogo = FileName;
            objModel.Operation = "insert";
            var _request = JsonConvert.SerializeObject(objModel);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiSaveBusiness, _request);
            if (String.IsNullOrWhiteSpace(ObjResponse.Response))
            {
                return View("Index", objModel);

            }

            return RedirectToAction("MyBusinessList","Home");
        }
        private String SaveImage(HttpPostedFileBase FileUpload)
        {
            string filename = FileUpload.FileName;
            string targetpath = Server.MapPath("~/DocImg/");
            string Extention = Path.GetExtension(filename);
            string DynamicFileName = Guid.NewGuid().ToString() + Extention;
            FileUpload.SaveAs(targetpath + DynamicFileName);
            return DynamicFileName;
        }

    }
}