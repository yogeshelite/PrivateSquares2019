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

        public ActionResult ProductDetail(long id)
        {
            List<ProductModel> Product = GetProduct(id);
            ProductModel objModel = new ProductModel();
            ViewBag.ProductReviews = CommonFile.GetProductReviews(id);      //ViewBag used for showing reviews on the ProductDetails Page
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
            ViewBag.SimilarProductList = ListAllProduct.Where(x => x.ProductCatId == objModel.ProductCatId).ToList();       //ViewBag for showing similar products in the Product Detail Page
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
        //public ActionResult SearchProducts(HeaderPartialModel objModel)
        //{
        //    ListAllProduct = CommonFile.GetProduct();
        //    if(String.IsNullOrWhiteSpace(objModel.SearchBarText))
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    var SearchList = ListAllProduct.Where(x => x.ProductName.ToUpper().Contains(objModel.SearchBarText.ToString().ToUpper())).ToList();
        //    ViewBag.UsersProduct = SearchList;
        //    ViewBag.PopularProducts = CommonFile.GetPopularProduct();
        //    return View("Index");
        //}

        public ActionResult WishList(ProductModel objmodel)
        {
            LoginModel MdUser = Services.GetLoginWebUser(this.ControllerContext.HttpContext, _JwtTokenManager);
            if (MdUser.Id != 0)
            {
                objmodel.UserId = Convert.ToInt64(MdUser.Id);
            }
            else { return JavaScript("window.alert('Please Login to access wishlist');"); }
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
            var result = SaveWishlist(objmodel);
             if (result == "Product Exists")
            {
                return JavaScript("window.alert('Product Already added to the Wishlist');");
            }
            return JavaScript("window.alert('Product added to the Wishlist');");

        }

        public string SaveWishlist(AddToCartModel objmodel)
        {

            var _request = JsonConvert.SerializeObject(objmodel);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiSaveWishlist, _request);
           
                return ObjResponse.Response;
        }

        public ActionResult DeleteFromWishlist(int ProductId)
        {
            AddToCartModel objmodel = new AddToCartModel();

            LoginModel MdUser = Services.GetLoginWebUser(this.ControllerContext.HttpContext, _JwtTokenManager);
            if (MdUser.Id != 0)
            { objmodel.UserId = Convert.ToInt64(MdUser.Id); }
            else
            {
                return View("WebHome");
            }
            objmodel.ProductId = ProductId;
            objmodel.Operation = "delete";
            var _request = JsonConvert.SerializeObject(objmodel);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiSaveWishlist, _request);
            return JavaScript("window.alert(Product removed successfully);");
        }



        public ActionResult ProductQuickView(long ProductId)
        {

            List<ProductModel> Product = GetProduct(ProductId);
            ProductModel objModel = new ProductModel();
            if (Product != null && Product.Count() > 0)
            {
                objModel.Id = ProductId;
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
            return PartialView("_WebQuickView", objModel);
        }

        public ActionResult SaveReviews(long productid, int productstars, string reviewtext)
        {
            LoginModel MdUser = Services.GetLoginWebUser(this.ControllerContext.HttpContext, _JwtTokenManager);
            ContactUsModel contactUsModel = new ContactUsModel();
            if (MdUser.Id != 0)
            {
                // objmodel.Name = MdUser.Name;
                // contactUsModel.ProductId = objmodel.Id;
                contactUsModel.UserId = Convert.ToInt64(MdUser.Id);
                contactUsModel.ProductId = productid;
                contactUsModel.Operation = "insert";
                contactUsModel.TotalRating = 5;
                contactUsModel.GivenRating = productstars;
                contactUsModel.Review = reviewtext;

                //contactUsModel.Review = objmodel.Review;
            }
            else
            {
                return JavaScript("alert('Please login to review this product')");
            }
            var _request = JsonConvert.SerializeObject(contactUsModel);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiSaveReview, _request);
            if (ObjResponse.Response == "Review Exists")
            {
                return JavaScript("alert('You have already reviewed this product');");
            }
            else
            {
                ViewBag.ReviewResponse = "Your Review has been submitted sucessfully.";
            }
            return RedirectToAction("ProductDetail", productid);
        }

        public ActionResult SearchBar(HeaderPartialModel objModel)
        {
            ListAllProduct = CommonFile.GetProduct();
            if (String.IsNullOrWhiteSpace(objModel.SearchBarText))
            {
                return RedirectToAction("Index");
            }
            var SearchList = ListAllProduct.Where(x => x.ProductName.ToUpper().Contains(objModel.SearchBarText.ToString().ToUpper())).ToList();
            ViewBag.UsersProduct = SearchList;
            ViewBag.PopularProducts = CommonFile.GetPopularProduct();
         
            return View();
        }
        public ActionResult MyOrders(SaleOrderModel objmodel)
        {
            LoginModel MdUser = Services.GetLoginWebUser(this.ControllerContext.HttpContext, _JwtTokenManager);
            if (MdUser.Id != 0)
            {
                objmodel.UserId = Convert.ToInt64(MdUser.Id);
            }
            else { return JavaScript("window.alert('Please Login to access wishlist');"); }
            var _request = JsonConvert.SerializeObject(objmodel);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetOrders, _request);
            var Orders = JsonConvert.DeserializeObject<List<SaleOrderModel>>(ObjResponse.Response);
            ViewBag.MyOrders = Orders;
            return View(objmodel);
        }

        public ActionResult OrderDetails(long id)
        {
            SaleOrderModel objmodel = new SaleOrderModel();
            LoginModel MdUser = Services.GetLoginWebUser(this.ControllerContext.HttpContext, _JwtTokenManager);
            if (MdUser.Id != 0)
            {
                objmodel.UserId = Convert.ToInt64(MdUser.Id);
            }
            objmodel.SaleOrderId = id;
            objmodel.Id = id;
            var _request = JsonConvert.SerializeObject(objmodel);
            ResponseModel ObjResponse = CommonFile.GetApiResponse(Constant.ApiGetOrders, _request);
            var Orderdetails = JsonConvert.DeserializeObject<List<SaleOrderModel>>(ObjResponse.Response);
            ViewBag.Orderdetails = Orderdetails;
            return View(objmodel);
        }

    }
}