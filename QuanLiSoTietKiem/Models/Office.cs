using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLiSoTietKiem.Models
{
    public class Office : BaseEntity
    {
        public int ID { get; set; }

        public string ShortName { get; set; }

        [Display(Name = "Tên chức vụ")]
        public string Name { get; set; }

        public virtual ICollection<Staff> Staffs { get; set; }
    }
}