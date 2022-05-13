using QuanLiSoTietKiem.Common;
using QuanLiSoTietKiem.DAL;
using QuanLiSoTietKiem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLiSoTietKiem.helpers;
using System.Data.Entity;

namespace QuanLiSoTietKiem.Controllers
{
    public class AuthencationController : Controller
    {
        private ManageSavingContext db = new ManageSavingContext();

        // GET: Authencation
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "UserName,Password")] UserDetail userDetail)
        {
            if (ModelState.IsValid)
            {

                string hashPassword = Hash.ComputeSha256(userDetail.Password);
                var data = db.Staffs.Where(s => s.UserName.Equals(userDetail.UserName) && s.Password.Equals(hashPassword));
                if (data.Count() > 0)
                {
                    Session["FullName"] = data.FirstOrDefault().Name;
                    Session["UserName"] = data.FirstOrDefault().UserName;
                    Session["ID"] = data.FirstOrDefault().ID;
                    Session["Role"] = data.FirstOrDefault().Office.ShortName;
                    return Redirect("/");
                }

            }
            TempData["Error"] = "Vui lòng kiểm tra lại tài khoản !";
            return RedirectToAction("Login");

        }


        public ActionResult Logout()
        {
            Session["FullName"] = null;
            Session["UserName"] = null;
            Session["ID"] = null;
            Session["Role"] = null;
            return RedirectToAction("Login");
        }


        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(string OldPassword, string NewPassword, string RepeatPassword)
        {

            Staff staff = db.Staffs.Find(Convert.ToInt32(Session["ID"].ToString()));

            if (staff == null)
            {
                return RedirectToAction("Login");
            }

            if (NewPassword != RepeatPassword)
            {
                TempData["Error"] = "Mật khẩu mới không khớp nhau !";
                return View();
            }

            if (staff.Password != Hash.ComputeSha256(OldPassword))
            {
                TempData["Error"] = "Vui lòng kiểm tra lại mật khẩu !";
                return View();
            }

            staff.Password = Hash.ComputeSha256(NewPassword);
            db.Entry(staff).State = EntityState.Modified;
            db.SaveChanges();
            TempData["Success"] = "Đổi mật khẩu thành công !";
            return Redirect("/Dashboard");
        }

    }
}