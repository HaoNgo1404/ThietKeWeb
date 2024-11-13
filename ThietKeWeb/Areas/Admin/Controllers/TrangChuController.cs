using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThietKeWeb.Models;

namespace ThietKeWeb.Areas.Admin.Controllers
{
    [OverrideAuthorization]

    public class TrangChuController : Controller
    {
        // GET: Admin/TrangChu

        public ActionResult Index()
        {
            return View();
        }
    }
}