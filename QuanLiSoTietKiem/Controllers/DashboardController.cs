using QuanLiSoTietKiem.DAL;
using QuanLiSoTietKiem.Models;
using QuanLiSoTietKiem.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLiSoTietKiem.Controllers
{
    [CustomAuthorize(Roles = "nhan_vien,giam_doc,quan_tri_vien")]
    public class DashboardController : Controller
    {
        private ManageSavingContext db = new ManageSavingContext();

        // GET: Dashboard
        public ActionResult Index()
        {
            ViewBag.countCustomer = db.Customers.Count();
            ViewBag.countSavingBook = db.SavingBooks.Count();
            ViewBag.countInvalidSavingBook = db.SavingBooks.Count(s => s.State == StateSavingBook.FINISHED || s.State == StateSavingBook.ON_TIME);
            ViewBag.countValidSavingBook = db.SavingBooks.Count(s => s.State == StateSavingBook.PENDING || s.State == StateSavingBook.PROCESSING);
            return View();
        }
    }
}