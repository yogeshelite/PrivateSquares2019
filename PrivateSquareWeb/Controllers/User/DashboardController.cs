using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrivateSquareWeb.Controllers.User
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PersonalProfile()
        {
            return View();
        }

        public ActionResult MyBusiness()
        {
            return View();
        }

    }
}