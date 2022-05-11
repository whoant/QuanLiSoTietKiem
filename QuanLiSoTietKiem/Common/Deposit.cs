using QuanLiSoTietKiem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLiSoTietKiem.Common
{
    public class Deposit
    {
        public string Name { get; set; }

        public TypeSavingBook Type { get; set; }

        public int SavingBookId { get; set; }

        public string DepositAmount { get; set; }

        public string EffectedAt { get; set; }

        public string ExpirationAt { get; set; }

        public string TypeDeposit { get; set; }

        public string InterestAmount { get; set; }

        public string TotalAmount { get; set; }
    }

}