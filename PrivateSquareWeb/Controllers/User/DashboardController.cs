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

        public ActionResult PersonalProfile()
        {
            return View();
        }

        public ActionResult MyBusiness()
        {
            return View();
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