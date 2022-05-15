using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hangfire;
using QuanLiSoTietKiem.DAL;
using QuanLiSoTietKiem.helpers;
using QuanLiSoTietKiem.Helpers;
using QuanLiSoTietKiem.Models;
using QuanLiSoTietKiem.Security;

namespace QuanLiSoTietKiem.Controllers
{
    [CustomAuthorize(Roles = "nhan_vien")]
    public class CustomerController : Controller
    {
        private ManageSavingContext db = new ManageSavingContext();

        // GET: Customer
        public ActionResult Index(string search)
        {
            if (search != null)
            {
                var customers = db.Customers.Where(customer => customer.IdentityNumber.Contains(search)).ToList();
                return View(customers);
            }

            return View(db.Customers.ToList());
        }

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
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

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserName,FullName,Phone,Birthday,IdentityNumber,Address,Sex,Email")] Customer customer, HttpPostedFileBase ImagePath)
        {
            if (ModelState.IsValid)
            {
                string password = StringHelper.RandomString(10);
                string encryptPassword = Hash.ComputeSha256(password);
                customer.Password = encryptPassword;

                BackgroundJob.Enqueue(() => MailHelper.SendEmail(customer.Email, "Tạo tài khoản thành công !", $"Chúc mừng {customer.FullName}, bạn đã tạo tài khoản thành công <br/>Mật khẩu mặt định của bạn: <strong>{password}</strong>"));

                if (ImagePath != null)
                {
                    string fileName = StringHelper.RandomString(10) + ".jpg";
                    customer.ImagePath = fileName;
                    string path = Path.Combine(Server.MapPath("/Images"), fileName);
                    ImagePath.SaveAs(path);
                }
                db.Customers.Add(customer);
                db.SaveChanges();
                TempData["Success"] = "Tạo tài khoản thành công !";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Tạo tài khoản thất bại !";
            return View(customer);
        }

        // GET: Customer/Edit/5
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

        // POST: Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserName,FullName,Phone,Birthday,IdentityNumber,ImagePath,Address,Sex,Email")] Customer customer, HttpPostedFileBase ImagePath)
        {
            if (ModelState.IsValid)
            {
                if (ImagePath != null)
                {
                    string fileName = StringHelper.RandomString(10) + ".jpg";
                    customer.ImagePath = fileName;
                    string path = Path.Combine(Server.MapPath("/Images"), fileName);
                    ImagePath.SaveAs(path);
                }
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "Cập nhập thông tin khách hàng thành công !";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Cập nhập thông tin khách hàng thất bại !";
            return View(customer);
        }


        public ActionResult ResetPassword(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            string password = StringHelper.RandomString(10);
            string encryptPassword = Hash.ComputeSha256(password);
            customer.Password = encryptPassword;

            BackgroundJob.Enqueue(() => MailHelper.SendEmail(customer.Email, "Quên mật khẩu", $"Mật khẩu mới của bạn: <strong>{password}</strong>"));

            db.Entry(customer).State = EntityState.Modified;
            db.SaveChanges();
            TempData["Success"] = "Reset mật khẩu thành công !";
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