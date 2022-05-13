using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLiSoTietKiem.Models
{
    public class FormClose : BaseEntity
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("SavingBook")]
        public int SavingBookId { get; set; }

        public virtual SavingBook SavingBook { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Staff")]
        public int StaffId { get; set; }

        public virtual Staff Staff { get; set; }

        public DateTime EffectedAt { get; set; } = DateTime.Now;
    }
}