using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLiSoTietKiem.Common
{
    public class InterestCommon
    {
        public int Id { get; set; }

        public double Factor { get; set; }

        public int DepositId { get; set; }

        public int Month { get; set; }
    }
}