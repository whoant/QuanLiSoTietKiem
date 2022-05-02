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
    public class SavingBookController : Controller
    {
        private ManageSavingContext db = new ManageSavingContext();

        // GET: SavingBook
        public ActionResult Index()
        {
            var savingBooks = db.SavingBooks.Include(s => s.Customer).Include(s => s.Interest);
            return View(savingBooks.ToList());
        }

        // GET: SavingBook/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavingBook savingBook = db.SavingBooks.Find(id);
            if (savingBook == null)
            {
                return HttpNotFound();
            }
            return View(savingBook);
        }

        // GET: SavingBook/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "ID", "UserName");
            ViewBag.InterestId = new SelectList(db.Interests, "ID", "Name");
            return View();
        }

        // POST: SavingBook/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DepositAmount,InterestAmount,Type,State,EffectedAt,ExpirationAt,ClosingAt,CustomerId,InterestId,CreatedAt,UpdatedAt")] SavingBook savingBook)
        {
            if (ModelState.IsValid)
            {
                db.SavingBooks.Add(savingBook);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "ID", "UserName", savingBook.CustomerId);
            ViewBag.InterestId = new SelectList(db.Interests, "ID", "Name", savingBook.InterestId);
            return View(savingBook);
        }

        // GET: SavingBook/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavingBook savingBook = db.SavingBooks.Find(id);
            if (savingBook == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "ID", "UserName", savingBook.CustomerId);
            ViewBag.InterestId = new SelectList(db.Interests, "ID", "Name", savingBook.InterestId);
            return View(savingBook);
        }

        // POST: SavingBook/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DepositAmount,InterestAmount,Type,State,EffectedAt,ExpirationAt,ClosingAt,CustomerId,InterestId,CreatedAt,UpdatedAt")] SavingBook savingBook)
        {
            if (ModelState.IsValid)
            {
                db.Entry(savingBook).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "ID", "UserName", savingBook.CustomerId);
            ViewBag.InterestId = new SelectList(db.Interests, "ID", "Name", savingBook.InterestId);
            return View(savingBook);
        }

        // GET: SavingBook/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavingBook savingBook = db.SavingBooks.Find(id);
            if (savingBook == null)
            {
                return HttpNotFound();
            }
            return View(savingBook);
        }

        // POST: SavingBook/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SavingBook savingBook = db.SavingBooks.Find(id);
            db.SavingBooks.Remove(savingBook);
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
