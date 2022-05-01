using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLiSoTietKiem.Models
{
    public class Interest
    {
        public int ID { get; set; }

        [Display(Name = "Lãi suất")]
        public string Name { get; set; }

        [Display(Name = "Hệ số lãi suất")]
        public double Factor { get; set; }

        public DateTime createdAt { get; set; }

        [ForeignKey("Period")]
        public int PeriodID { get; set; }

        public Period Period { get; set; }

        public ICollection<SavingBook> SavingBooks { get; set; }
    }
}