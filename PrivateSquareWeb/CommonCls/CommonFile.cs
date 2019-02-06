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
    }
}