using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLiSoTietKiem.DAL;
using QuanLiSoTietKiem.Models;
using QuanLiSoTietKiem.Security;

namespace QuanLiSoTietKiem.Controllers
{
    [CustomAuthorize(Roles = "giam_doc")]
    public class PeriodController : Controller
    {
        private ManageSavingContext db = new ManageSavingContext();

        // GET: Period
        public ActionResult Index()
        {
            return View(db.Periods.ToList());
        }

    
        // GET: Period/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Period/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Month")] Period period)
        {
            if (ModelState.IsValid)
            {
                if (db.Periods.Count(p => p.Month == period.Month) == 0)
                {
                    db.Periods.Add(period);
                    db.SaveChanges();
                    TempData["Success"] = "Tạo thành công !";
                    return RedirectToAction("Index");
                }
                TempData["Error"] = "Kì hạn này đã tồn tại !";
            }

            return View(period);
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
