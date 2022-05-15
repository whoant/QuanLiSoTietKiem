using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLiSoTietKiem.Common;
using QuanLiSoTietKiem.DAL;
using QuanLiSoTietKiem.helpers;
using QuanLiSoTietKiem.Models;
using QuanLiSoTietKiem.Security;

namespace QuanLiSoTietKiem.Controllers
{
    
    public class UserController : Controller
    {
        private ManageSavingContext db = new ManageSavingContext();

        // GET: User
        [CheckSession]
        public ActionResult Index()
        {

            string user = (string)Session["UserName"];
            var user1 = db.Customers.Where(e => e.UserName.Contains(user)).ToList();            
            var interest = db.Interests.ToList();            
            ViewBag.interest = interest;            
            return View(user1);
        }
        [CheckSession]
        public ActionResult DetailUser()
        {
            int i = (int)Session["IDUser"];
            DateTime today = DateTime.Today;
            
            var savingBooks = db.SavingBooks.Where(savingbook => savingbook.CustomerId == i);
            Debug.WriteLine(today);
            TimeSpan timeRemaining = today.Subtract(savingBooks.ToList()[0].ExpirationAt);
            TimeSpan timeRemaining1 = savingBooks.ToList()[0].ExpirationAt.Subtract(today);
            ViewBag.today = today;
            Debug.WriteLine(timeRemaining);
            Debug.WriteLine(timeRemaining1);
            return View(savingBooks.ToList());
        }
        [CheckSession]
        public ActionResult History(int? id)
        {
            if (id == null)
            {                
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Debug.WriteLine(id);
            var savingBooks = db.SavingBooks.Where(savingbook => savingbook.ID == id);
            if (savingBooks == null)
            {
                return HttpNotFound();
            }
            string user = (string)Session["UserName"];
            var user1 = db.Customers.Where(e => e.UserName.Contains(user)).ToList();
            ViewBag.user1 = user1;
            return View(savingBooks.ToList());
        }

        public ActionResult Login()
        {
            // var savingBooks = db.SavingBooks.Where(savingbook => savingbook.CustomerId == 1);
            //return View(savingBooks.ToList());
            return View();
        }

        [HttpPost] 
        public ActionResult Login([Bind(Include = "UserName,Password")] UserDetail userDetail)
        {
            if (ModelState.IsValid)
            {

                string hashPassword = Hash.ComputeSha256(userDetail.Password);                
                var dataUser = db.Customers.Where(s => s.UserName.Equals(userDetail.UserName) && s.Password.Equals(hashPassword));
                
                if (dataUser.Count() > 0)
                {
                    Session["FullName"] = dataUser.FirstOrDefault().FullName;
                    Session["UserName"] = dataUser.FirstOrDefault().UserName;
                    Session["IDUser"] = dataUser.FirstOrDefault().ID;
                    return Redirect("/User");
                }
            }
            TempData["Error"] = "Vui lòng kiểm tra lại tài khoản !";
            return RedirectToAction("Login");

        }
        [CheckSession]
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }
        [CheckSession]
        [HttpPost]
        public ActionResult ChangePassword(string oldPassword, string NewPassword, string RepeatPassword)
        {
            Customer customer = db.Customers.Find(Convert.ToInt32(Session["IDUser"].ToString()));
            if (customer == null)
            {
                return RedirectToAction("Login");
            }
            if (NewPassword != RepeatPassword)
            {
                TempData["Error"] = "Mật khẩu mới không khớp nhau !";
                return View();
            }

            if (customer.Password != Hash.ComputeSha256(oldPassword))
            {
                TempData["Error"] = "Vui lòng kiểm tra lại mật khẩu !";
                return View();
            }

            customer.Password = Hash.ComputeSha256(NewPassword);
            db.Entry(customer).State = EntityState.Modified;
            db.SaveChanges();
            TempData["Success"] = "Đổi mật khẩu thành công !";
            return Redirect("Index");
        }

        public ActionResult Logout()
        {
            Session["FullName"] = null;
            Session["UserName"] = null;            
            Session["IDUser"] = null;            
            return RedirectToAction("Login");
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           Customer customer = db.Customers.Find(id);
            //SavingBook customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserName,Password,FullName,Phone,Birthday,IdentityNumber,ImagePath,Balance,Address,Sex,Email,CreatedAt,UpdatedAt")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserName,Password,FullName,Phone,Birthday,IdentityNumber,ImagePath,Balance,Address,Sex,Email,CreatedAt,UpdatedAt")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    
}
