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
            var ProductCatList = CommonFile.GetProductCategory();
            ViewBag.ProductCatList = ProductCatList;
     
            return PartialView("~/Views/Shared/_WebSiteSidebar.cshtml", objModel);
        }
    }
}