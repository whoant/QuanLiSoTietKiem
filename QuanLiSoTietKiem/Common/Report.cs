using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLiSoTietKiem.Common
{
    public class Report
    {

        public Report(string n)
        {
            this.Name = n;
            this.In = 0;
            this.Out = 0;
            this.Diff = 0;
        }

        public string Name { get; set; }

        public long In { get; set; }

        public long Out { get; set; }

        public long Diff { get; set; }
    }
}