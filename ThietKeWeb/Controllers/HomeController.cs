using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThietKeWeb.Controllers
{
    [OverrideAuthentication]
    public class HomeController : Controller
    {
      private MyStoreEntities db = new MyStoreEntities();

         public ActionResult Index(string SearchTerm, int? page)
 {
     var model = new HomeProduct__2VM();
     var products = db.Products.AsQueryable();
     if (!string.IsNullOrEmpty(SearchTerm))
     {
         model.SearchTerm = SearchTerm;
         products = products.Where(p => p.ProductName.Contains(SearchTerm) ||
                                  p.ProductDescription.Contains(SearchTerm) ||
                                  p.Category.CategoryName.Contains(SearchTerm));


         int pageNumer = page ?? 1;
         int pageSize = 2;


         model.Products = products.OrderByDescending(p => p.OrderDetails.Count()).Take(5).ToPagedList(pageNumer, pageSize);
     }

     //int pageNumer1 = page ?? 1;
     //int pageSize1 = 5;


     //model.Products = products.OrderByDescending(p => p.OrderDetails.Count()).Take(5).ToPagedList(pageNumer1, pageSize1);// lỗi vì dùng [products] -> sản phẩm lọc bởi tìm kiếm

     model.FeaturedProducts = db.Products.OrderByDescending(p => p.OrderDetails.Count()).Take(5).ToList();

     model.Categories = db.Categories.ToList();
    
     return View(model);

 }
        public ActionResult NguonGoc()
        {
            return View();
        }
        public ActionResult DichVu()
        {
            return View();
        }
        public ActionResult NgheNghiep()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult ProDuctDetail()
        {
            return View();
        }
        public ActionResult CaPhe()
        {
            return View();
        }
        public ActionResult Tra()
        {
            return View();
        }
        public ActionResult DaXay()
        {
            return View();
        }
        public ActionResult Khac()
        {
            return View();
        }
        public ActionResult Recruit()
        {
            return View();
        }
        public ActionResult RecruitDetail()
        {
            return View();
        }
        public ActionResult Address()
        {
            return View();
        }
        public ActionResult GioHang()
        {
            return View();
        }
        public ActionResult ThanhToan()
        {
            return View();
        }
        public ActionResult CheckProduct()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult LogIn()
        {
            return View();
        }
        public ActionResult History()
        {
            return View();
        }
    }
}
