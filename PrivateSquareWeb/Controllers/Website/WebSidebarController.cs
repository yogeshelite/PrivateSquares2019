using PrivateSquareWeb.CommonCls;
using PrivateSquareWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrivateSquareWeb.Controllers.Website
{
    public class WebSidebarController : Controller

    {
       
        // GET: WebSidebar
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult WebSiteSidebar()
        {
            ProductModel objModel = new ProductModel();
            var ProductCatList = CommonFile.GetProductCategory(null);
            ViewBag.ProductCatList = ProductCatList;
            ViewBag.ProdCatList36 = ProductCatList.Where(x => x.ParentCatId == 36).ToList();
            ViewBag.ProdCatList37 = ProductCatList.Where(x => x.ParentCatId == 37).ToList();
            ViewBag.ProdCatList38 = ProductCatList.Where(x => x.ParentCatId == 38).ToList();
            ViewBag.ProdCatList39 = ProductCatList.Where(x => x.ParentCatId == 39).ToList();
            ViewBag.ProdCatList41 = ProductCatList.Where(x => x.ParentCatId == 41).ToList();
            ViewBag.ProdCatList42 = ProductCatList.Where(x => x.ParentCatId == 42).ToList();
     
            return PartialView("~/Views/Shared/_WebSiteSidebar.cshtml", objModel);
        }
    }
}