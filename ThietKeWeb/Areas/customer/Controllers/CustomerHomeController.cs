using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThietKeWeb.Models;
using ThietKeWeb.Models.ViewModel;
using PagedList;
using System.Net;

namespace ThietKeWeb.Areas.customer.Controllers
{
    public class CustomerHomeController : Controller
    {
         private MyStoreEntities db = new MyStoreEntities();
        // GET: Customer/CustomerHome

        public ActionResult CustomerHome(string SearchTerm, int? page)
        {
            var model = new HomeProductVM();
            var products = db.Products.AsQueryable();
            //tìm kiếm sp dựa trên từ khóa
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                model.SearchTerm = SearchTerm;
                products = products.Where(p => p.ProductName.Contains(SearchTerm) ||
                                    p.ProductDescription.Contains(SearchTerm) ||
                                    p.Category.CategoryName.Contains(SearchTerm));
            }
            //đoạn code liên quan đến phân trang
            //lấy số trang hiện tại(mặ cđịnh là trang 1 nếu kh có giá trị)
            int pageNumer = page ?? 1;
            int pageSize = 6;//số sp mỗi trang

            model.BestSellerProducts = products.OrderByDescending(p => p.OrderDetails.Count()).Take(5).ToPagedList(pageNumer, pageSize);



            return View(model);

        }

            public ActionResult ProductList() { return View(); }


            public ActionResult ProductDetail(int? id, int? quatity, int? page)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Product pro = db.Products.Find(id);
                if (pro == null)
                {
                    return HttpNotFound();
                }

                var products = db.Products.Where(p => p.CategoryID == pro.CategoryID && p.ProductID != pro.ProductID).AsQueryable();

                ProductDetailVM model = new ProductDetailVM();

                int pageNumer = page ?? 1;
                int pageSize = model.PageSize;
                model.product = pro;
                model.RelatedProducts = products.OrderBy(p => p.ProductID).Take(8).ToPagedList(pageNumer, pageSize);
                model.BestSellerProduct = products.OrderByDescending(p => p.OrderDetails.Count()).Take(8).ToPagedList(pageNumer, pageSize);

                if (quatity.HasValue)
                {
                    model.quatity = quatity.Value;
                }
                return View(model);


            }
        
    }
}