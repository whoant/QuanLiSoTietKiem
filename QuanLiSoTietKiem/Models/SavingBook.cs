using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLiSoTietKiem.Models
{
    public class SavingBook : BaseEntity
    {
        public int ID { get; set; }

        [Display(Name = "Số tiền gửi")]
        [Required]
        public long DepositAmount { get; set; }

        [Display(Name = "Số tiền lãi")]
        public long InterestAmount { get; set; }

        [Display(Name = "Loại tài khoản")]
        [Required]
        public TypeSavingBook Type { get; set; }

        [DefaultValue(StateSavingBook.PENDING)]
        public StateSavingBook State { get; set; }

        [Display(Name = "Ngày mở")]
        public DateTime EffectedAt { get; set; } = DateTime.Now;

        [Display(Name = "Ngày hết hạn")]
        public DateTime ExpirationAt { get; set; }

        [Display(Name = "Ngày đóng")]
        public DateTime? ClosingAt { get; set; } = null;

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        [ForeignKey("Interest")]
        public int InterestId { get; set; }

        public virtual Interest Interest { get; set; }

    }

    public enum StateSavingBook
    {
        PENDING,
        FINISHED,
        ON_TIME,
        PROCESSING
    }

    public enum TypeSavingBook
    {
        [Display(Name= "Nhận lãi. Gửi gốc sang kỳ hạn mới")]
        INTEREST_RECEIVER,
        [Display(Name = "Gửi cả gốc và lãi sang kỳ hạn mới ")]
        ROLLOVER_BOTH,
        [Display(Name = "Đóng tài khoản. Nhận cả gốc và lãi")]
        CLOSING_ACCOUNT
    }
}