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

    public class WebHomeController : Controller
    {
        JwtTokenManager _JwtTokenManager = new JwtTokenManager();
        static List<ProductImages> EditProductImageList;
        static List<ProductModel> ListAllProduct;
        
        // GET: WebHome
        public ActionResult Index()
        {
            ListAllProduct = CommonFile.GetProduct();
            ViewBag.UsersProduct = ListAllProduct;
            ViewBag.PopularProducts = CommonFile.GetPopularProduct();
            var ProductCatList = CommonFile.GetProductCategory();
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

        //   // var _request = JsonConvert.SerializeObject(objmodel);
        //    //ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetProduct, _request);
        //    //GetUserProductList = JsonConvert.DeserializeObject<List<ProductModel>>(ObjResponse.Response);


        //    var _request = _JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(objmodel));
        //    ResponseModel ObjResponse = CommonFile.GetApiResponseJWT(Constant.ApiGetProduct, _request);
        //    GetUserProductList = JsonConvert.DeserializeObject<List<ProductModel>>(ObjResponse.Response);



        //    return GetUserProductList;

        //}
        public ActionResult ProductDetail(long id)
        {
            List<ProductModel> Product = GetProduct(id);
            ProductModel objModel = new ProductModel();
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

            return View(objModel);
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

        public List<ProductModel> GetProduct(long Id)
        {
            var GetProduct = new List<ProductModel>();
            ProductModel objProduct = new ProductModel();
            objProduct.Id = Id;
            LoginModel MdUser = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager);
            //if (MdUser.Id != 0)
            //    objProduct.UserId = Convert.ToInt64(MdUser.Id);
            var _request = JsonConvert.SerializeObject(objProduct);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetProductDetail, _request);
            GetProduct = JsonConvert.DeserializeObject<List<ProductModel>>(ObjResponse.Response);
            return GetProduct;

        }

        public JsonResult AddToCart(AddToCartModel objmodel)
        {
            AddToCart objAddToCart = new AddToCart();
            return objAddToCart.AddToCartFun(objmodel, this.ControllerContext.HttpContext);

        }
        public JsonResult RemoveQtyToCart(AddToCartModel objmodel)
        {
            AddToCart objAddToCart = new AddToCart();
            return objAddToCart.RemoveQtyToCartFun(objmodel, this.ControllerContext.HttpContext);

        }
        
        [HttpPost]
        public ActionResult ProcessForm(FormCollection frm, string submit)
        {
            String SearchText = frm["TxtSearch"];
            switch (submit)
            {
                case "Search":
                    ViewBag.UsersProduct = SearchProduct(SearchText);
                    var ProductCatList = CommonFile.GetProductCategory();

                    ViewBag.ProductCatList = ProductCatList;
                    break;
                case "Cancel":
                    ViewBag.Message = "The operation was cancelled!";
                    break;
            }
            return View("Index");
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
            //return SearchProductList.OrderBy(s=>s.ProductName).ToList();
        }
        public ActionResult SearchProducts(HeaderPartialModel objModel)
        {
            ListAllProduct = CommonFile.GetProduct();
            var SearchList = ListAllProduct.Where(x => x.ProductName.ToUpper().Contains(objModel.SearchBarText.ToString().ToUpper())).ToList();
            ViewBag.UsersProduct = SearchList;
            ViewBag.PopularProducts = CommonFile.GetPopularProduct();
            return View("Index");
        }

        public ActionResult WishList(ProductModel objmodel)
        {
            LoginModel MdUser = Services.GetLoginWebUser(this.ControllerContext.HttpContext, _JwtTokenManager);
            if (MdUser.Id != 0)
            {
            objmodel.UserId = Convert.ToInt64(MdUser.Id);
            }
            else
            {
               return JavaScript("window.alert('Please Login to access wishlist');");
            }
            var _request = JsonConvert.SerializeObject(objmodel);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetWishlist, _request);
            var ProductWishlist = JsonConvert.DeserializeObject<List<ProductModel>>(ObjResponse.Response);
            ViewBag.Wishlist = ProductWishlist;
            return View(objmodel);
        }

        public ActionResult AddToWishlist(AddToCartModel objmodel)
        {
            LoginModel MdUser = Services.GetLoginWebUser(this.ControllerContext.HttpContext, _JwtTokenManager);
            if (MdUser.Id != 0)
            { objmodel.UserId = Convert.ToInt64(MdUser.Id); }
            else { return JavaScript("window.alert('Please Login to access wishlist');"); }
            objmodel.Operation = "insert";
            //AddToCart objAddToCart = new AddToCart();
            var result = SaveWishlist(objmodel);
            return JavaScript("window.alert('Product added to the Wishlist');");
            
        }

        public string SaveWishlist(AddToCartModel objmodel)
        {

            var _request = JsonConvert.SerializeObject(objmodel);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiSaveWishlist, _request);
            return ObjResponse.Response;
        }
        

    }
}