using Newtonsoft.Json;
using PrivateSquareWeb.CommonCls;
using PrivateSquareWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrivateSquareWeb.Controllers.Website
{
    public class ProductCatWiseController : Controller
    {
        static List<ProductModel> ListAllProduct;
        JwtTokenManager _JwtTokenManager = new JwtTokenManager();

        // GET: ProductCatWise

        public ActionResult Index(long? id)
        {
            ListAllProduct = CommonFile.GetProduct();
            var SearchProductList = ListAllProduct.Where(x => x.ProductCatId == id).ToList();
            ViewBag.SearchCatId = id;
            ViewBag.UsersProduct = SearchProductList;
            var ProductCatList = CommonFile.GetProductCategory();
            // ViewBag.ProductCatList = ProductCatList;
            ViewBag.ProductCatList = ProductCatList;
            return View();
        }
        //public List<ProductModel> GetProduct()
        //{
        //    var GetUserProductList = new List<ProductModel>();
        //    ProductModel objmodel = new ProductModel();
        //    LoginModel MdUser = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager);
        //    //if (MdUser.Id != 0)
        //    //    objmodel.UserId = Convert.ToInt64(MdUser.Id);
        //    var _request = JsonConvert.SerializeObject(objmodel);
        //    ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetProduct, _request);
        //    GetUserProductList = JsonConvert.DeserializeObject<List<ProductModel>>(ObjResponse.Response);
        //    return GetUserProductList;

        //}
        public ActionResult ProcessForm(FormCollection frm, string submit)
        {
            ProductModel objModel = new ProductModel();
            String SearchText = frm["TxtSearch"];
            String SearchPrice = frm["TxtPriceRange"];
            switch (submit)
            {
                case "Search":
                    ViewBag.UsersProduct = SearchProduct(SearchText);
                    var ProductCatList = CommonFile.GetProductCategory();

                    ViewBag.ProductCatList = ProductCatList;
                    break;
                case "PriceRange":
                    ViewBag.UsersProduct = SearchPriceRange(SearchPrice, 0);
                    var ProductCatListPrice = CommonFile.GetProductCategory();
                    ViewBag.ProductCatList = ProductCatListPrice;
                    // return PartialCatwiseProductValue(2);
                    break;
            }
            return View("Index");
            // return PartialView("~/Views/ProductCatWise/PartialCatwiseProductValue.cshtml", objModel);
        }
        public PartialViewResult PartialCatwiseProductValue(long? id)
        {
            ProductModel objModel = new ProductModel();
            List<ProductModel> SearchProductList = new List<ProductModel>();
            //objModel.ProductCatId = id;
            if (id == null)
            {
                SearchProductList = ListAllProduct;
            }
            else
            {
                objModel.ProductCatId = Convert.ToInt64(id);
                SearchProductList = ListAllProduct.Where(x => x.ProductCatId == id).ToList();
            }


            ViewBag.UsersProduct = SearchProductList;
            var ProductCatList = CommonFile.GetProductCategory();
            // ViewBag.ProductCatList = ProductCatList;
            ViewBag.ProductCatList = ProductCatList;
            return PartialView("~/Views/ProductCatWise/PartialCatwiseProductValue.cshtml", objModel);
        }
        public PartialViewResult PartialCatwiseProductPrice(decimal Price, long CategoryId)
        {
            ProductModel objModel = new ProductModel();
            //if (!string.IsNullOrEmpty(ProductId))
            //{
            //    objModel.ProductCatId = Convert.ToInt64(ProductId);
            //}
            objModel.ProductCatId = CategoryId;
            var SearchProductList = SearchPriceRange(Price.ToString(), CategoryId);

            ViewBag.UsersProduct = SearchProductList;
            var ProductCatList = CommonFile.GetProductCategory();
            // ViewBag.ProductCatList = ProductCatList;
            ViewBag.ProductCatList = ProductCatList;
            return PartialView("~/Views/ProductCatWise/PartialCatwiseProductPrice.cshtml", objModel);
        }
        private List<ProductModel> SearchProduct(string ProductName)
        {
            //ListAllProduct = GetProduct();
            //Item Name
            var SearchProductList = ListAllProduct.Where(x => x.ProductName.ToUpper().Contains(ProductName.ToUpper())).ToList();
            //Item Price

            //var SearchProductList = ListAllProduct.Where(x => x.DiscountPrice > Convert.ToDecimal(ProductName) && (x.DiscountPrice < Convert.ToDecimal(ProductName))).ToList();
            //var SearchProductList = ListAllProduct.Where(x => x.DiscountPrice > Convert.ToDecimal(ProductName)).ToList();
            return SearchProductList;
        }
        private List<ProductModel> SearchPriceRange(string ProductPrice, long CategoryId)
        {

            var SearchProductList = ListAllProduct.Where(x => x.DiscountPrice > Convert.ToDecimal(0) && (x.DiscountPrice < Convert.ToDecimal(ProductPrice)) && x.ProductCatId == CategoryId).ToList();
            //var SearchProductList = ListAllProduct.Where(x => x.DiscountPrice > Convert.ToDecimal(ProductName)).ToList();
            return SearchProductList;
        }

        public ActionResult Sortby(int sortorder,int pageindex,long? productcatid)
        {
            ProductModel objModel = new ProductModel();
            string SortOrder="";
            switch (sortorder)
            {
                case 1:
                    SortOrder = "DiscountPrice";
                    break;  
                case 2:
                    SortOrder = "DiscountPrice desc";
                    break;
                case 3:
                    SortOrder = "Popularity";
                    break;
                case 4:
                    SortOrder = "RecordDate";
                    break;
            }
      
            var sortedproducts = CommonFile.GetSortedProducts(SortOrder,pageindex, productcatid);
            var ProductList = ListAllProduct.Where(x => x.ProductCatId == productcatid).ToList();
            
            ViewBag.UsersProduct = sortedproducts;
            ViewBag.SearchCatId = productcatid;
            
            var ProductCatList = CommonFile.GetProductCategory();
            
            ViewBag.ProductCatList = ProductCatList;
            return PartialView("~/Views/ProductCatWise/PartialCatwiseProductValue.cshtml", objModel);
        }
    }
}