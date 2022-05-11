using QuanLiSoTietKiem.Common;
using QuanLiSoTietKiem.DAL;
using QuanLiSoTietKiem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLiSoTietKiem.helpers;

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
                    return Redirect("/Office");
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
            return RedirectToAction("Login");
        }

    }
}