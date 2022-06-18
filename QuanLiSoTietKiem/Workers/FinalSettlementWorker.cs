using Hangfire;
using QuanLiSoTietKiem.Common;
using QuanLiSoTietKiem.DAL;
using QuanLiSoTietKiem.Helpers;
using QuanLiSoTietKiem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QuanLiSoTietKiem.Workers
{
    public static class FinalSettlementWorker
    {
        private static ManageSavingContext db = new ManageSavingContext();

        public static void Handle()
        {
            var savingBooks = db.SavingBooks.Where(s => s.ExpirationAt >= DateTime.Now && s.State == StateSavingBook.PENDING);
            foreach (var s in savingBooks)
            {
                s.ClosingAt = DateTime.Now;
                s.State = StateSavingBook.FINISHED;
                Bill bill = new Bill
                {
                    SavingBookId = s.ID,
                    Name = s.Customer.FullName,
                    DepositAmount = String.Format("{0:0,0} đ", s.DepositAmount),
                    InterestAmount = String.Format("{0:0,0} đ", s.InterestAmount),
                    EffectedAt = s.EffectedAt.ToString("dd/MM/yyy"),
                    ClosingAt = s.ClosingAt?.ToString("dd/MM/yyy"),
                    Month = s.Interest.Period.Month + " %/năm"
                };

                string html = GeneratorPdf.BillHtml(bill);

                BackgroundJob.Enqueue(() => MailHelper.SendEmail(s.Customer.Email, "Tất toán thành công !", $"Tất toán thành công", GeneratorPdf.Generator(html)));

                db.Entry(s).State = EntityState.Modified;
                db.SaveChanges();


            }
        }
    }
}