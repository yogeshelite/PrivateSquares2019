using Newtonsoft.Json;
using PrivateSquareWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrivateSquareWeb.CommonCls
{
    public class CommonFile
    {
        public static ResponseModel GetApiResponse(string Url, String Data)
        {
            var _response = Services.GetApiResponseJson(Url, "POST", Data);

            ResponseModel _data = JsonConvert.DeserializeObject<ResponseModel>(_response);
            ResponseModel ObjResponse = JsonConvert.DeserializeObject<ResponseModel>(_data.Response);
            return ObjResponse;
        }

        public static List<DropDownModel> GetCountry()
        {
            var CountryList = new List<DropDownModel>();
            DropDownModel objUserProfile = new DropDownModel();
              var _request = JsonConvert.SerializeObject(objUserProfile);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetCountry, _request);
            CountryList = JsonConvert.DeserializeObject<List<DropDownModel>>(ObjResponse.Response);
            return CountryList;
        }
        public static List<DropDownModel> GetState()
        {
            var StateList = new List<DropDownModel>();
            DropDownModel objUserProfile = new DropDownModel();
              var _request = JsonConvert.SerializeObject(objUserProfile);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetState, _request);
            StateList = JsonConvert.DeserializeObject<List<DropDownModel>>(ObjResponse.Response);
            return StateList;
        }
        public static List<DropDownModel> GetCity()
        {
            var CityList = new List<DropDownModel>();
            DropDownModel objUserProfile = new DropDownModel();
             var _request = JsonConvert.SerializeObject(objUserProfile);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetCity, _request);
            CityList = JsonConvert.DeserializeObject<List<DropDownModel>>(ObjResponse.Response);
            return CityList;
        }
        public static List<DropDownModel> GetProfession()
        {
            var ProfessionList = new List<DropDownModel>();
            DropDownModel objUserProfile = new DropDownModel();
            //  var _request = JsonConvert.SerializeObject(objUserProfile);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetProfession, "");
            ProfessionList = JsonConvert.DeserializeObject<List<DropDownModel>>(ObjResponse.Response);
            return ProfessionList;
        }
        public static List<DropDownModel> GetProductCategory()
        {
            var ProductCategoryList = new List<DropDownModel>();
            DropDownModel objUserProfile = new DropDownModel();
            //  var _request = JsonConvert.SerializeObject(objUserProfile);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetProductCategory, "");
            ProductCategoryList = JsonConvert.DeserializeObject<List<DropDownModel>>(ObjResponse.Response);
            return ProductCategoryList;
        }

    }
}