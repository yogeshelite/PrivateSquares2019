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
    public class WebCompareController : Controller
    {
        // GET: WebCompare
        JwtTokenManager _JwtTokenManager = new JwtTokenManager();
        static List<ProductImages> EditProductImageList;


        public ActionResult Index(long id)
        {
        
                //ViewBag.UsersProduct = CommonFile.GetProduct();
                //var Users = ViewBag.UsersProduct;


            var Product = GetProduct(id).ToList();
            
            CompareProductModel objModel = new CompareProductModel();
            if (Product != null && Product.Count() > 0)
            {
                objModel.Id = id;
                objModel.ProductName = Product[0].ProductName;
                objModel.ProductCatId = Product[0].ProductCatId;
                objModel.ProductImage = Product[0].ProductImage;
                objModel.SellingPrice = Product[0].SellingPrice;
                objModel.DiscountPrice = Product[0].DiscountPrice;
                objModel.BusinessId = Product[0].BusinessId;
                objModel.UserId = Product[0].UserId;
                objModel.Description = Product[0].Description;
                objModel.VendorName = Product[0].VendorName;
                objModel.ProductImages = Product[0].ProductImages;

                List<ProductImages> ListProductImages = new List<ProductImages>();
                if (!String.IsNullOrEmpty(objModel.ProductImages))
                {
                    String[] ProductImages = objModel.ProductImages.Split(',');
                    ListProductImages = GetSelectedProductImages(ProductImages, objModel.ProductImage);
                    EditProductImageList = ListProductImages;
                    ViewBag.ProductImages = ListProductImages;
                }
                else
                {
                    // String[] ProductImages = new String [0];
                    ViewBag.ProductImages = ListProductImages;
                }
            }
            ViewBag.UsersProduct = objModel;
            return View(objModel);
        }
        
        public List<CompareProductModel> GetProduct(long Id)
        {
            var GetProduct = new List<CompareProductModel>();
            CompareProductModel objProduct = new CompareProductModel();
            objProduct.Id = Id;
            //LoginModel MdUser = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager);
            //if (MdUser.Id != 0)
            //    objProduct.UserId = Convert.ToInt64(MdUser.Id);
            var _request = JsonConvert.SerializeObject(objProduct);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetProductDetail, _request);
            GetProduct = JsonConvert.DeserializeObject<List<CompareProductModel>>(ObjResponse.Response);
            return GetProduct;

        }
        private List<ProductImages> GetSelectedProductImages(String[] ProductImages, String DefaultImage)
        {
            List<ProductImages> ListProductImages = new List<ProductImages>();


            for (int i = 0; i < ProductImages.Length; i++)
            {
                ProductImages objProductImage = new ProductImages();
                objProductImage.Name = ProductImages[i];
                if (objProductImage.Name.Equals(DefaultImage))
                {
                    objProductImage.IsSelected = true;
                }
                else
                {
                    objProductImage.IsSelected = false;
                }
                ListProductImages.Add(objProductImage);

            }
            return ListProductImages;
        }
        public JsonResult AddToCart(AddToCartModel objmodel)
        {
            AddToCart objAddToCart = new AddToCart();
            return objAddToCart.AddToCartFun(objmodel, this.ControllerContext.HttpContext);

        }



    }
}