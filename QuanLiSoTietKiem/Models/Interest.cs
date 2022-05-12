using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLiSoTietKiem.Models
{
    public class Interest : BaseEntity
    {
        public int ID { get; set; }

        [Display(Name = "Lãi suất")]
        public string Name { get; set; }

        [Display(Name = "Hệ số lãi suất")]
        public double Factor { get; set; }

        [Display(Name = "Ngày áp dụng")]
        public DateTime EffectedAt { get; set; } = DateTime.Now;

        [ForeignKey("Period")]
        public int PeriodID { get; set; }

        public virtual Period Period { get; set; }

        public virtual ICollection<SavingBook> SavingBooks { get; set; }
    }
}