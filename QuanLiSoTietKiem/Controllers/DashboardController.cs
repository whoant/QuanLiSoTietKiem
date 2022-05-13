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
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}