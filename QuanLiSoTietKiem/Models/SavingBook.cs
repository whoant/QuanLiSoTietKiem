using System;
using System.Collections.Generic;
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
        INTEREST_RECEIVER,
        ROLLOVER_BOTH,
        CLOSING_ACCOUNT
    }
}