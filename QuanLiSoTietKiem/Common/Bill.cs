using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLiSoTietKiem.Common
{
    public class Bill
    {
        public string Name { get; set; }

        public string ClosingAt { get; set; }

        public int SavingBookId { get; set; }

        public string DepositAmount { get; set; }

        public string EffectedAt { get; set; }

        public string ExpirationAt { get; set; }

        public string InterestAmount { get; set; }
           
        public string Month { get; set; }
   
    }
}