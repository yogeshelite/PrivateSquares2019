﻿using Newtonsoft.Json;
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
            ListAllProduct = GetProduct();
            var SearchProductList = ListAllProduct.Where(x => x.ProductCatId == id).ToList();

            ViewBag.UsersProduct = SearchProductList;
            var ProductCatList = CommonFile.GetProductCategory();
            // ViewBag.ProductCatList = ProductCatList;
            ViewBag.ProductCatList = ProductCatList;
            return View();
        }
        public List<ProductModel> GetProduct()
        {
            var GetUserProductList = new List<ProductModel>();
            ProductModel objmodel = new ProductModel();
            LoginModel MdUser = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager);
            //if (MdUser.Id != 0)
            //    objmodel.UserId = Convert.ToInt64(MdUser.Id);
            var _request = JsonConvert.SerializeObject(objmodel);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetProduct, _request);
            GetUserProductList = JsonConvert.DeserializeObject<List<ProductModel>>(ObjResponse.Response);
            return GetUserProductList;

        }
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
                    ViewBag.UsersProduct = SearchPriceRange(SearchPrice);
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
            //objModel.ProductCatId = id;
            var SearchProductList = ListAllProduct.Where(x => x.ProductCatId == id).ToList();

            ViewBag.UsersProduct = SearchProductList;
            var ProductCatList = CommonFile.GetProductCategory();
            // ViewBag.ProductCatList = ProductCatList;
            ViewBag.ProductCatList = ProductCatList;
            return PartialView("~/Views/ProductCatWise/PartialCatwiseProductValue.cshtml", objModel);
        }
        public PartialViewResult PartialCatwiseProductPrice(decimal Price,long id)
        {
            ProductModel objModel = new ProductModel();
            objModel.ProductCatId = id;
            var SearchProductList = SearchPriceRange(Price.ToString());

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
        private List<ProductModel> SearchPriceRange(string ProductPrice)
        {

            var SearchProductList = ListAllProduct.Where(x => x.DiscountPrice > Convert.ToDecimal(500) && (x.DiscountPrice < Convert.ToDecimal(ProductPrice))).ToList();
            //var SearchProductList = ListAllProduct.Where(x => x.DiscountPrice > Convert.ToDecimal(ProductName)).ToList();
            return SearchProductList;
        }
    }
}