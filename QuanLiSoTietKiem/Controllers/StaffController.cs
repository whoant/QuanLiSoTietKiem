using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
    [CustomAuthorize(Roles = "quan_tri_vien")]
    public class StaffController : Controller
    {
        private ManageSavingContext db = new ManageSavingContext();

        // GET: Staff
        public ActionResult Index()
        {
            var staffs = db.Staffs.Include(s => s.Office);
            return View(staffs.ToList());
        }

        // GET: Staff/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // GET: Staff/Create
        public ActionResult Create()
        {
            ViewBag.OfficeId = new SelectList(db.Offices, "ID", "Name");
            return View();
        }

        // POST: Staff/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserName,Name,Email,Phone,Birthday,Address,Sex,OfficeId")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                string password = StringHelper.RandomString(10);
                BackgroundJob.Enqueue(() => MailHelper.SendEmail(staff.Email, "Tạo tài khoản thành công !", $"Mật khẩu mặt định của bạn là <strong>{password}</strong>"));
                
                staff.Password = Hash.ComputeSha256(password);
                db.Staffs.Add(staff);
                db.SaveChanges();
                TempData["Success"] = "Tạo tài khoản thành công ! Vui lòng kiểm tra mail để tài khoản gửi về";
                return RedirectToAction("Index");
            }

            ViewBag.OfficeId = new SelectList(db.Offices, "ID", "Name", staff.OfficeId);
            return View(staff);
        }

        // GET: Staff/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            ViewBag.OfficeId = new SelectList(db.Offices, "ID", "Name", staff.OfficeId);
            return View(staff);
        }

        // POST: Staff/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserName,Name,Email,Phone,Birthday,Address,Sex,OfficeId")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staff).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "Cập nhập thông tin thành công !";
                return RedirectToAction("Index");
            }
            ViewBag.OfficeId = new SelectList(db.Offices, "ID", "Name", staff.OfficeId);
            return View(staff);
        }

        public ActionResult ResetPassword(int id)
        {
            Staff staff = db.Staffs.Find(id);
            string password = StringHelper.RandomString(10);
            BackgroundJob.Enqueue(() => MailHelper.SendEmail(staff.Email, "Tạo tài khoản thành công !", $"Mật khẩu mặt định của bạn là <strong>{password}</strong>"));

            staff.Password = Hash.ComputeSha256(password);
            db.Entry(staff).State = EntityState.Modified;
            db.SaveChanges();
            TempData["Success"] = "Cập nhập mật khẩu thành công ! Vui lòng kiểm tra mail để tài khoản gửi về";
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
