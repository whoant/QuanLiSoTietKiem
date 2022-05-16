using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using QuanLiSoTietKiem.Common;
using QuanLiSoTietKiem.DAL;
using QuanLiSoTietKiem.Helpers;
using QuanLiSoTietKiem.Models;
using QuanLiSoTietKiem.Security;

namespace QuanLiSoTietKiem.Controllers
{
    [CustomAuthorize(Roles = "nhan_vien")]
    public class SavingBookController : Controller
    {
        private ManageSavingContext db = new ManageSavingContext();

        // GET: SavingBook
        public ActionResult Index(int CustomerId)
        {
            var customer = db.Customers.Find(CustomerId);
            var savingBooks = db.SavingBooks.Where(savingbook => savingbook.CustomerId == CustomerId);
            ViewBag.customer = customer;
            return View(savingBooks.ToList());
        }

        // GET: z
        public ActionResult Details(int CustomerId, int? id)
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
        public ActionResult Create(int CustomerId)
        {
            var customer = db.Customers.Find(CustomerId);
            ViewBag.customer = customer;
            var interests = db.Interests.GroupBy(g => g.PeriodID).Select(x => x.OrderByDescending(y => y.CreatedAt).FirstOrDefault());
            ViewBag.InterestId = interests.Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Period.Month + " tháng | " + x.Factor + " % năm" });
            ViewBag.Interests = JsonConvert.SerializeObject(interests.Select(s => new InterestCommon { Id = s.ID, Factor = s.Factor, DepositId = s.PeriodID, Month = s.Period.Month}), Formatting.Indented,
            new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
            return View();
        }

        // POST: SavingBook/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepositAmount,Type,InterestId")] SavingBook savingBook, int CustomerId)
        {
            Customer customer = db.Customers.Find(CustomerId);
            if (customer == null)
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                Interest interest = db.Interests.Find(savingBook.InterestId);
                savingBook.ExpirationAt = DateTime.Now.AddMonths(interest.Period.Month).AddDays(-1);
                savingBook.InterestAmount = (long)(savingBook.DepositAmount * interest.Factor / 100 / 12 * interest.Period.Month);
                savingBook.CustomerId = CustomerId;
                SavingBook newSavingBook = db.SavingBooks.Add(savingBook);
                db.SaveChanges();
                FormCreate formCreate = new FormCreate
                {
                    SavingBookId = newSavingBook.ID,
                    StaffId = Convert.ToInt32(Session["ID"])
                };
                db.FormCreates.Add(formCreate);
                db.SaveChanges();
                TempData["Success"] = "Tạo tài khoản tiết kiệm thành công !";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Tạo tài khoản tiết kiệm thất bại !";
            ViewBag.CustomerId = new SelectList(db.Customers, "ID", "UserName", savingBook.CustomerId);
            ViewBag.InterestId = new SelectList(db.Interests, "ID", "Name", savingBook.InterestId);
            return View(savingBook);
        }


        public ActionResult Export(int CustomerId, int id)
        {
            Customer customer = db.Customers.Find(CustomerId);
            SavingBook savingBook = db.SavingBooks.Where(s => s.ID == id && s.CustomerId == CustomerId).First();
            if (savingBook == null)
            {
                return RedirectToAction("Index");
            }

            Deposit deposit = new Deposit
            {
                SavingBookId = savingBook.ID,
                Name = savingBook.Customer.FullName,
                Type = savingBook.Type,
                DepositAmount = String.Format("{0:0,0} đ", savingBook.DepositAmount),
                InterestAmount = String.Format("{0:0,0} đ", savingBook.InterestAmount),
                TotalAmount = String.Format("{0:0,0} đ", savingBook.DepositAmount + savingBook.InterestAmount),
                TypeDeposit = savingBook.Interest.Period.Month + " tháng / " + savingBook.Interest.Factor + " % năm",
                EffectedAt = savingBook.EffectedAt.ToString("dd/MM/yyy"),
                ExpirationAt = savingBook.ExpirationAt.ToString("dd/MM/yyy"),
            };

            string html = GeneratorPdf.DepositHtml(deposit);

            return new FileContentResult(GeneratorPdf.Generator(html), "application/pdf");
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
