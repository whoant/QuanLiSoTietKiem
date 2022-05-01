using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLiSoTietKiem.Models
{
    public class Period
    {
        public int ID { get; set; }

        [Display(Name = "Kì hạn")]
        public string Name { get; set; }

        [Display(Name = "Tháng")]
        public int Month { get; set; }

        public ICollection<Interest> Interests { get; set; }
    }
}