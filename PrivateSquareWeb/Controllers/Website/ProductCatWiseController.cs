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
            var SearchProductList = new List<ProductModel>();
            if (!CommonFile.IsParentCategory(id))
            {
                SearchProductList = ListAllProduct.Where(x => x.ProductCatId == id).ToList();
            }
            else { SearchProductList = ListAllProduct.Where(x => x.ParentCatId == id).ToList(); }
            ViewBag.SearchCatId = id;
            ViewBag.UsersProduct = SearchProductList;
            ViewBag.SearchResultCount = SearchProductList.Count;
            var ProductCatList = CommonFile.GetProductCategory(null);
            // ViewBag.ProductCatList = ProductCatList;
            ViewBag.ProductCatList = ProductCatList;
            return View();
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
                    var ProductCatList = CommonFile.GetProductCategory(null);

                    ViewBag.ProductCatList = ProductCatList;
                    break;
                case "PriceRange":
                    ViewBag.UsersProduct = SearchPriceRange(SearchPrice, 0);
                    var ProductCatListPrice = CommonFile.GetProductCategory(null);
                    ViewBag.ProductCatList = ProductCatListPrice;
                    // return PartialCatwiseProductValue(2);
                    break;
            }
            return View("Index");
            // return PartialView("~/Views/ProductCatWise/PartialCatwiseProductValue.cshtml", objModel);
        }
        public PartialViewResult PartialCatwiseProductValue(long? id)
        {
            var jsonString1 = "{\"ProductCatId\":\"" + id + "\"}";
            Services.SetCookie(this.ControllerContext.HttpContext, "ProductCatWiseCatId", jsonString1.ToString());
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
                if (!CommonFile.IsParentCategory(id))
                {
                    SearchProductList = ListAllProduct.Where(x => x.ProductCatId == id).ToList();
                }
                else
                {
                    SearchProductList = ListAllProduct.Where(x => x.ParentCatId == id).ToList();
                }
            }
            ViewBag.LowerLimit = 1;
            ViewBag.UsersProduct = SearchProductList.Take(12);
            ViewBag.NumberOfPages = SearchProductList.Count / 10;
            ViewBag.SearchResultCount = SearchProductList.Count;
            var ProductCatList = CommonFile.GetProductCategory(id);
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
            var ProductCatList = CommonFile.GetProductCategory(CategoryId);
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

        public ActionResult Sortby(int sortorder, int pageindex, long? productcatid)
        {
            ProductModel objModel = new ProductModel();
            string SortOrder = "";
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

            var sortedproducts = CommonFile.GetSortedProducts(SortOrder, pageindex, productcatid);
            var ProductList = ListAllProduct.Where(x => x.ProductCatId == productcatid).ToList();

            ViewBag.UsersProduct = sortedproducts;
            ViewBag.SearchCatId = productcatid;

            var ProductCatList = CommonFile.GetProductCategory(null);

            ViewBag.ProductCatList = ProductCatList;
            return PartialView("~/Views/ProductCatWise/PartialCatwiseProductValue.cshtml", objModel);
        }

        public PartialViewResult NextPage(long id)
        {
            string SearchCookieValue = Services.GetCookie(this.HttpContext, "ProductCatWiseCatId").Value;
            dynamic _data = SearchCookieValue;
            var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(_data);
            int ProductCatId = int.Parse(json["ProductCatId"].ToString());
            int pageindex = (int)id;
            ViewBag.LowerLimit = ((pageindex / 5) * 5) + 1;
            ViewBag.PageIndex = pageindex;
            ListAllProduct = CommonFile.GetProduct();
            var SearchProductList = new List<ProductModel>();
            if (!CommonFile.IsParentCategory(ProductCatId))
            {
                SearchProductList = ListAllProduct.Where(x => x.ProductCatId == ProductCatId).ToList();
            }
            else
            {
                SearchProductList = ListAllProduct.Where(x => x.ParentCatId == ProductCatId).ToList();
            }
            ViewBag.UsersProduct = SearchProductList.Skip((pageindex - 1) * 12).Take(12);
            ViewBag.ProductsFrom = ((pageindex - 1) * 12) + 1;
            ViewBag.ToProductsCount = Enumerable.Count(ViewBag.UsersProduct);
            ViewBag.SearchResultCount = SearchProductList.Count;
            ViewBag.NumberOfPages = SearchProductList.Count / 10;
            return PartialView("~/Views/ProductCatWise/PartialCatwiseProductValue.cshtml");
        }

        public ActionResult MorePages(long id)
        {
            int pageindex = (int)id;
            ViewBag.PageIndex = pageindex;
            HeaderPartialModel objModel = new HeaderPartialModel();
            if (pageindex % 5 == 0)
            {
                ViewBag.LowerLimit = ((pageindex / 5));
            }
            else
            {
                ViewBag.LowerLimit = ((pageindex / 5) * 5) + 1;
            }
            //ViewBag.NumberOfPages = pageindex+4;
            #region  For binding products in next page

            ListAllProduct = CommonFile.GetProduct();
            if (String.IsNullOrWhiteSpace(objModel.SearchBarText))
            {

                if (objModel.ParentCatId == 0)
                {
                    ViewBag.UsersProduct = ListAllProduct.Skip((pageindex - 1) * 12).Take(12);
                    ViewBag.SearchResultCount = ListAllProduct.Count;
                    if ((ViewBag.SearchResultCount / 10) < (pageindex + 4))
                    {
                        ViewBag.NumberOfPages = (ViewBag.SearchResultCount / 10);
                    }
                    else
                    {
                        ViewBag.NumberOfPages = ViewBag.LowerLimit + 4;
                    }
                    ViewBag.ProductsFrom = ((pageindex - 1) * 12) + 1;
                    ViewBag.ToProductsCount = Enumerable.Count(ViewBag.UsersProduct);
                    return PartialView("~/Views/ProductCatWise/PartialCatwiseProductValue.cshtml", objModel);
                }
                var SearchListWithCategory = ListAllProduct.Where(x => x.ParentCatId.Equals(objModel.ParentCatId)).ToList();
                ViewBag.UsersProduct = SearchListWithCategory.Skip((pageindex - 1) * 12).Take(12);
                ViewBag.SearchResultCount = SearchListWithCategory.Count;
                if ((ViewBag.SearchResultCount / 10) < (pageindex + 4))
                {
                    ViewBag.NumberOfPages = ViewBag.LowerLimit + ((ViewBag.SearchResultCount - 5) - 1);
                }
                else
                {
                    ViewBag.NumberOfPages = pageindex + 4;
                }
                ViewBag.ProductsFrom = ((pageindex - 1) * 12) + 1;
                ViewBag.ToProductsCount = Enumerable.Count(ViewBag.UsersProduct);
                return PartialView("~/Views/ProductCatWise/PartialCatwiseProductValue.cshtml", objModel);
            }
            if (objModel.ParentCatId == 0)
            {
                var SearchList = ListAllProduct.Where(x => x.ProductName.ToUpper().Contains(objModel.SearchBarText.ToString().ToUpper())).ToList();
                ViewBag.UsersProduct = SearchList.Skip((pageindex - 1) * 12).Take(12);
                ViewBag.SearchResultCount = SearchList.Count;
            }
            else
            {
                var SearchList = ListAllProduct.Where(x => x.ProductName.ToUpper().Contains(objModel.SearchBarText.ToString().ToUpper())).ToList();

                var SearchListWithCategory = SearchList.Where(x => x.ParentCatId.Equals(objModel.ParentCatId)).ToList();
                ViewBag.UsersProduct = SearchListWithCategory.Skip((pageindex - 1) * 12).Take(12);

                ViewBag.SearchResultCount = SearchListWithCategory.Count;
            }

            #endregion
            if ((ViewBag.SearchResultCount / 10) < (pageindex + 4))
            {
                ViewBag.NumberOfPages = ViewBag.LowerLimit + ((ViewBag.SearchResultCount - 5) - 1);
            }
            else
            {
                ViewBag.NumberOfPages = pageindex + 4;
            }
            ViewBag.ProductsFrom = ((pageindex - 1) * 12) + 1;
            ViewBag.ToProductsCount = Enumerable.Count(ViewBag.UsersProduct);
            return PartialView("~/Views/ProductCatWise/PartialCatwiseProductValue.cshtml", objModel);
        }

        public ActionResult PreviousPages(long id)
        {
            if (id == 0) { return JavaScript("alert('End of Page')"); }
            int pageindex = (int)id;
            ViewBag.PageIndex = pageindex;
            HeaderPartialModel objModel = new HeaderPartialModel();
            if (pageindex % 5 == 0)
            {
                if ((pageindex / 5) == 1)
                { ViewBag.LowerLimit = ((pageindex / 5)); }
                else
                {
                    ViewBag.LowerLimit = ((pageindex / 5) * 5) + 1;
                }
            }
            else
            {
                ViewBag.LowerLimit = ((pageindex / 5) * 5) + 1;
            }

            #region  For binding products in next page

            ListAllProduct = CommonFile.GetProduct();
            if (String.IsNullOrWhiteSpace(objModel.SearchBarText))
            {

                if (objModel.ParentCatId == 0)
                {
                    ViewBag.UsersProduct = ListAllProduct.Skip((pageindex - 1) * 12).Take(12);
                    ViewBag.SearchResultCount = ListAllProduct.Count;
                    if ((ViewBag.SearchResultCount / 10) < (pageindex + 4))
                    {
                        ViewBag.NumberOfPages = ViewBag.LowerLimit * 5;
                    }
                    else
                    {
                        ViewBag.NumberOfPages = pageindex + 4;
                    }
                    ViewBag.ProductsFrom = ((pageindex - 1) * 12) + 1;
                    ViewBag.ToProductsCount = Enumerable.Count(ViewBag.UsersProduct);
                    return PartialView("~/Views/ProductCatWise/PartialCatwiseProductValue.cshtml", objModel);
                }
                var SearchListWithCategory = ListAllProduct.Where(x => x.ParentCatId.Equals(objModel.ParentCatId)).ToList();
                ViewBag.UsersProduct = SearchListWithCategory.Skip((pageindex - 1) * 12).Take(12);
                ViewBag.SearchResultCount = SearchListWithCategory.Count;
                if ((ViewBag.SearchResultCount / 10) < (pageindex + 4))
                {
                    ViewBag.NumberOfPages = ViewBag.LowerLimit + ((ViewBag.SearchResultCount - 5) - 1);
                }
                else
                {
                    ViewBag.NumberOfPages = pageindex + 4;
                }
                ViewBag.ProductsFrom = ((pageindex - 1) * 12) + 1;
                ViewBag.ToProductsCount = Enumerable.Count(ViewBag.UsersProduct);
                return PartialView("~/Views/ProductCatWise/PartialCatwiseProductValue.cshtml", objModel);
            }
            if (objModel.ParentCatId == 0)
            {
                var SearchList = ListAllProduct.Where(x => x.ProductName.ToUpper().Contains(objModel.SearchBarText.ToString().ToUpper())).ToList();
                ViewBag.UsersProduct = SearchList.Skip((pageindex - 1) * 12).Take(12);
                ViewBag.SearchResultCount = SearchList.Count;
            }
            else
            {
                var SearchList = ListAllProduct.Where(x => x.ProductName.ToUpper().Contains(objModel.SearchBarText.ToString().ToUpper())).ToList();

                var SearchListWithCategory = SearchList.Where(x => x.ParentCatId.Equals(objModel.ParentCatId)).ToList();
                ViewBag.UsersProduct = SearchListWithCategory.Skip((pageindex - 1) * 12).Take(12);

                ViewBag.SearchResultCount = SearchListWithCategory.Count;
            }

            #endregion
            ViewBag.ProductsFrom = ((pageindex - 1) * 12) + 1;
            ViewBag.ToProductsCount = Enumerable.Count(ViewBag.UsersProduct);
            return PartialView("~/Views/ProductCatWise/PartialCatwiseProductValue.cshtml", objModel);
        }
    }
}