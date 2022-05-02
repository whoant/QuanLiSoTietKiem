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

namespace QuanLiSoTietKiem.Controllers
{
    public class InterestController : Controller
    {
        private ManageSavingContext db = new ManageSavingContext();

        // GET: Interest
        public ActionResult Index()
        {
            var interests = db.Interests.Include(i => i.Period);
            return View(interests.ToList());
        }

        // GET: Interest/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interest interest = db.Interests.Find(id);
            if (interest == null)
            {
                return HttpNotFound();
            }
            return View(interest);
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
        public ActionResult Create([Bind(Include = "ID,Name,Factor,EffectedAt,PeriodID,CreatedAt,UpdatedAt")] Interest interest)
        {
            if (ModelState.IsValid)
            {
                db.Interests.Add(interest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PeriodID = new SelectList(db.Periods, "ID", "Name", interest.PeriodID);
            return View(interest);
        }

        // GET: Interest/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interest interest = db.Interests.Find(id);
            if (interest == null)
            {
                return HttpNotFound();
            }
            ViewBag.PeriodID = new SelectList(db.Periods, "ID", "Name", interest.PeriodID);
            return View(interest);
        }

        // POST: Interest/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Factor,EffectedAt,PeriodID,CreatedAt,UpdatedAt")] Interest interest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(interest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PeriodID = new SelectList(db.Periods, "ID", "Name", interest.PeriodID);
            return View(interest);
        }

        // GET: Interest/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interest interest = db.Interests.Find(id);
            if (interest == null)
            {
                return HttpNotFound();
            }
            return View(interest);
        }

        // POST: Interest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Interest interest = db.Interests.Find(id);
            db.Interests.Remove(interest);
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
