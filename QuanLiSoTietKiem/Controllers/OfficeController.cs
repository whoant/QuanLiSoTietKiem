using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLiSoTietKiem.Common;
using QuanLiSoTietKiem.DAL;
using QuanLiSoTietKiem.Helpers;
using QuanLiSoTietKiem.Models;
using QuanLiSoTietKiem.Security;

namespace QuanLiSoTietKiem.Controllers
{

    [CustomAuthorize(Roles = "quan_tri_vien")]
    public class OfficeController : Controller
    {
        private ManageSavingContext db = new ManageSavingContext();

        // GET: Office
        public ActionResult Index()
        {

            List<OfficeCount> officeCounts = db.Offices.Select(office => new OfficeCount
            {
                Name = office.Name,
                Count = office.Staffs.Count()
            }).ToList();

 
            ViewData["officeCounts"] = officeCounts;
            return View();
        }


        // GET: Office/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Office/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")] Office office)
        {
            office.ShortName = StringHelper.UrlFriendly(office.Name);
            if (ModelState.IsValid)
            {
                db.Offices.Add(office);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(office);
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
