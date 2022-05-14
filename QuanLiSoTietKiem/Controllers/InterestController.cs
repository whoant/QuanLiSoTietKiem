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

    //[CustomAuthorize(Roles = "giam_doc")]
    public class InterestController : Controller
    {
        private ManageSavingContext db = new ManageSavingContext();

        // GET: Interest
        public ActionResult Index()
        {
            //var interests = db.Interests.Include(i => i.Period);
            
            var interests = db.Interests.GroupBy(g => g.PeriodID).Select(x => x.OrderByDescending(y => y.CreatedAt).FirstOrDefault()).ToList();
            return View(interests);
        }

       
        // GET: Interest/Create
        public ActionResult Create()
        {
            ViewBag.PeriodID = new SelectList(db.Periods, "ID", "Name");
            return View();
        }

        // POST: Interest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Factor,EffectedAt,PeriodID")] Interest interest)
        {
            if (ModelState.IsValid)
            {
                db.Interests.Add(interest);
                db.SaveChanges();
                TempData["Success"] = "Thêm lãi suất thành công !";
                return RedirectToAction("Index");
            }

            ViewBag.PeriodID = new SelectList(db.Periods, "ID", "Name", interest.PeriodID);
            TempData["Error"] = "Thêm lãi suất thất bại !";
            return View(interest);
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
