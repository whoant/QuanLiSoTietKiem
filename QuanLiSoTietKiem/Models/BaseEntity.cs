using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLiSoTietKiem.Models
{
    public abstract class BaseEntity
    {
        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}