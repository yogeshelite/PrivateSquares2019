using Newtonsoft.Json;
using PrivatesquaresWebApiNew.Models;
using PrivateSquareWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrivateSquareWeb.Controllers.User
{
    public class InterestController : Controller
    {
       static List<InterestModel> ListInterest;
        // GET: Interest
        public ActionResult Index()
        {
            var InterestCategoryList = GetInterestCategory();
            ViewBag.InterestCategoryList = new SelectList(InterestCategoryList, "Id", "Name");
            ListInterest = GetAllInterest();
            return View();
        }

        public List<InterestCategoryModel> GetInterestCategory()
        {
            var ProductCategoryList = new List<InterestCategoryModel>();
            var _request = "";//_JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(loginModel));
            ResponseModel ObjResponse = GetApiResponse(Constant.ApiGetAllInterestCategory, "");
            ProductCategoryList = JsonConvert.DeserializeObject<List<InterestCategoryModel>>(ObjResponse.Response);
            return ProductCategoryList;

        }
        public JsonResult GetCateWiseInterest(long CatId)
        {
             List<InterestModel> FilterInterestModelsList= ListInterest.Where(i=>i.InterestCatId==CatId).ToList();
            var JsonResponse = Json(FilterInterestModelsList);
            return JsonResponse;
        }
        public List<InterestModel> GetAllInterest()
        {
            var InterestList = new List<InterestModel>();
            var _request = "";//_JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(loginModel));
            ResponseModel ObjResponse = GetApiResponse(Constant.ApiGetAllInterest, "");
            InterestList = JsonConvert.DeserializeObject<List<InterestModel>>(ObjResponse.Response);
            return InterestList;
        }
        private ResponseModel GetApiResponse(string Url, String Data)
        {
            var _response = Services.GetApiResponseJson(Url, "POST", Data);

            ResponseModel _data = JsonConvert.DeserializeObject<ResponseModel>(_response);
            ResponseModel ObjResponse = JsonConvert.DeserializeObject<ResponseModel>(_data.Response);
            return ObjResponse;
        }
    }
}