using QuanLiSoTietKiem.Common;
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
    [CustomAuthorize(Roles = "giam_doc")]
    public class ReportController : Controller
    {
        private ManageSavingContext db = new ManageSavingContext();

        
        // GET: Report
        public ActionResult Index(string day, string month, string year)
        {
            if (day == null || month == null || year == null)
            {
                return View();
            }

            int d = Int32.Parse(day);
            int m = Int32.Parse(month);
            int y = Int32.Parse(year);

            DateTime firstDay = new DateTime(y, m, d);
            DateTime lastDay = firstDay.AddDays(1);

            var listSavingsBooksIn = db.SavingBooks.Where(savingBook => savingBook.EffectedAt >= firstDay && savingBook.EffectedAt <= lastDay && savingBook.State == Models.StateSavingBook.PENDING).ToList();
            var listSavingsBooksOut = db.SavingBooks.Where(savingBook => savingBook.ClosingAt >= firstDay && savingBook.ClosingAt <= lastDay && savingBook.State == Models.StateSavingBook.ON_TIME).ToList();


            Report interestReceiver = new Report("Nhận lãi. Gửi gốc sang kỳ hạn mới");
            Report rolloverBoth = new Report("Gửi cả gốc và lãi sang kì hạn mới");
            Report closingAccount = new Report("Đóng tài khoản. Nhận cả gốc và lãi");

            foreach (var sv in listSavingsBooksIn)
            {
                switch (sv.Type) {
                    case TypeSavingBook.INTEREST_RECEIVER:
                        interestReceiver.In += sv.DepositAmount;
                        break;
                    case TypeSavingBook.ROLLOVER_BOTH:
                        rolloverBoth.In += sv.DepositAmount;
                        break;
                    case TypeSavingBook.CLOSING_ACCOUNT:
                        closingAccount.In += sv.DepositAmount;
                        break;
                }
            }

            foreach (var sv in listSavingsBooksOut)
            {
                switch (sv.Type)
                {
                    case TypeSavingBook.INTEREST_RECEIVER:
                        interestReceiver.Out += sv.DepositAmount;
                        break;
                    case TypeSavingBook.ROLLOVER_BOTH:
                        rolloverBoth.Out += sv.DepositAmount;
                        break;
                    case TypeSavingBook.CLOSING_ACCOUNT:
                        closingAccount.Out += sv.DepositAmount;
                        break;
                }
            }

            interestReceiver.Diff = interestReceiver.In - interestReceiver.Out;
            rolloverBoth.Diff = rolloverBoth.In - rolloverBoth.Out;
            closingAccount.Diff = closingAccount.In - closingAccount.Out;


            ViewBag.interestReceiver = interestReceiver;
            ViewBag.rolloverBoth = rolloverBoth;
            ViewBag.closingAccount = closingAccount;
            ViewBag.Day = day;
            ViewBag.Month = month;
            ViewBag.Year = year;

            return View();
        }


    }
}
