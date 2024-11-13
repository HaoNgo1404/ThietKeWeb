using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ThietKeWeb.Models.ViewModel;
using ThietKeWeb.Models;
using System.Web.Routing;
using System.Diagnostics;

namespace ThietKeWeb.Areas.Admin.Controllers
{
    public class Admin_AccountController : Controller
    {
        private ThietKeWebEntities db = new ThietKeWebEntities();
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [OverrideAuthentication]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var existUser = db.Users.SingleOrDefault(u => u.Username == model.Username);
                if (existUser != null)
                {
                    ModelState.AddModelError("Username", "Tên đăng nhập đã tồn tại.");
                    return View(model);
                }
                var user = new User
                {
                    Username = model.Username,
                    Password = model.Password,
                    UserRole = "Admin"
                };
                db.Users.Add(user);
                var customer = new Customer
                {
                    CustomerName = model.CustomerName,
                    CustomerPhone = model.CustomerPhone,
                    CustomerEmail = model.CustomerEmail,
                    CustomerAddress = model.CustomerAddress,
                    Username = model.Username
                };
                db.Customers.Add(customer);
                db.SaveChanges();
                Session["Username"] = user.Username;
                Session["Role"] = user.UserRole;
                RedirectToAction("Index", "TrangChu");
            }
            return View(model);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [OverrideAuthentication]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM model, bool rememberMe)
        {
            if (ModelState.IsValid)
            {
                var validUser = db.Users.SingleOrDefault(u => u.Username == model.Username && u.Password == model.Password && u.UserRole == "Admin");
                if (validUser != null)
                {
                    Session["Username"] = validUser.Username;
                    Session["Role"] = validUser.UserRole;
                    FormsAuthentication.SetAuthCookie(validUser.Username, rememberMe);
                    return RedirectToAction("Index", "TrangChu");

                }
                else
                {
                    ModelState.AddModelError("", "Tên dăng nhập hoặc mật khẩu không đúng");
                }
            }
            return View(model);
        }
        [OverrideAuthorization]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "TrangChu");
        }
        [Authorize]
        public ActionResult ProfileInfo()
        {
            var user = db.Users.SingleOrDefault(u => u.Username == User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction("Login", "Admin_Account");
            }
            var customer = db.Customers.SingleOrDefault(c => c.Username == user.Username);
            if (customer == null)
            {
                return RedirectToAction("Index", "TrangChu");
            }     
            var model = new RegisterVM
            {
                Username = user.Username,
                CustomerName = customer.CustomerName,
                CustomerPhone = customer.CustomerPhone,
                CustomerEmail = customer.CustomerEmail,
                CustomerAddress = customer.CustomerAddress
            };
            return View(model);
        }
        public ActionResult UpdateProfile(int id)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfile(int id, FormCollection form)
        {
            var customer = db.Customers.SingleOrDefault(c => c.CustomerID == id);
            if (customer == null)
            {
                return RedirectToAction("Index", "TrangChu");
            }

            customer.CustomerName = form["CustomerName"];
            customer.CustomerPhone = form["CustomerPhone"];
            customer.CustomerEmail = form["CustomerEmail"];
            customer.CustomerAddress = form["CustomerAddress"];

            db.SaveChanges();
            ViewBag.Message = "Cập nhật tài khoản thành công!";

            return RedirectToAction("ProfileInfo", new { id = customer.CustomerID });
        }
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.Username == User.Identity.Name);
                if (user == null)
                {
                    return RedirectToAction("Login", "Admin_Account");
                }
                user.Password = model.Password;
                db.SaveChanges();
                return RedirectToAction("ProfileInfo");
            }
            return View(model);
        }
    }
}