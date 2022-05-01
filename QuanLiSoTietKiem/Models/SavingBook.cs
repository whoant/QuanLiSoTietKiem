using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLiSoTietKiem.Models
{
    public class SavingBook
    {
        public int ID { get; set; }

        public long Deposit { get; set; }

        public long Interest { get; set; }

        public TypeSavingBook Type { get; set; }

        [DefaultValue(StateSavingBook.PENDING)]
        public StateSavingBook State { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ExpirationAt { get; set; }

        public DateTime ClosingAt { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

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