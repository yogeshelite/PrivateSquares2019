﻿using Newtonsoft.Json;
using PrivateSquareWeb.CommonCls;
using PrivateSquareWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrivateSquareWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.AllUsers = GetAllUsers();
            return View();
        }
        public List<UsersProfileModel> GetAllUsers()
        {
            var GetAllUserList = new List<UsersProfileModel>();
            var _request = "";//_JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(loginModel));
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetUsersProfile, "");
            GetAllUserList = JsonConvert.DeserializeObject<List<UsersProfileModel>>(ObjResponse.Response);
            return GetAllUserList;

        }
        public List<BusinessModel> GetUsersBusiness()
        {
            var GetUserBusinessList = new List<BusinessModel>();
            BusinessModel objmodel = new BusinessModel();
            objmodel.UserId = Convert.ToInt64(Services.GetCookie(this.ControllerContext.HttpContext,"usrId").Value);
            var _request =JsonConvert.SerializeObject(objmodel);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetUserBusiness, _request);
            GetUserBusinessList = JsonConvert.DeserializeObject<List<BusinessModel>>(ObjResponse.Response);
            return GetUserBusinessList;

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult MyBusinessList()
        {
            
            ViewBag.UsersBusiness = GetUsersBusiness();
            return View();
        }
    }
}