using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLiSoTietKiem.Models
{
    public class Customer : BaseEntity
    {
        public int ID { get; set; }

        [Display(Name = "Tài khoản")]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Họ tên")]
        public string FullName { get; set; }

        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }

        [Display(Name = "Ngày sinh nhật")]
        public DateTime Birthday { get; set; }

        [Display(Name = "Số chứng minh thư")]
        public string IdentityNumber { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Số dư")]
        public long Balance { get; set; }

        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Display(Name = "Giới tính")]
        public SexType Sex { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        public virtual ICollection<SavingBook> SavingBooks { set; get; }
    }
}