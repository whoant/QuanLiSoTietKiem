using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace QuanLiSoTietKiem.Models
{
    public class Customer
    {
        public int ID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public string Phone { get; set; }

        public DateTime Birthday { get; set; }

        public string IdentityNumber { get; set; }

        [DefaultValue(0)]
        public long Balance { get; set; }

        public string Address { get; set; }

        public Gender Sex { get; set; }

        public string Email { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}