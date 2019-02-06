using Newtonsoft.Json;
using PrivateSquareWeb.CommonCls;
using PrivateSquareWeb.Models;
using System;
using System.Collections.Generic;
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
        public ActionResult SaveBussiness(FormCollection frmColl )
        {
            BusinessModel objModel = new BusinessModel();
            objModel.Id = 0;
            objModel.BusinessName = frmColl["businessname"];
            objModel.Location = frmColl["address"];
            objModel.ProfessionalCatId = 0;//Convert.ToInt64(frmColl["ddlProfessionalCat"]);
            objModel.ProfessionalKeyword = frmColl["Keywords"];
            objModel.PinCode = frmColl["Pincode"];
            objModel.Operation = "insert";
            var _request = JsonConvert.SerializeObject(objModel);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiSaveBusiness, _request);
            if (String.IsNullOrWhiteSpace(ObjResponse.Response))
            {
                return View("Index", objModel);

            }

            return View();
        }


    }
}