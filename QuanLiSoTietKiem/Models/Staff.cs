using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLiSoTietKiem.Models
{
    public class Staff : BaseEntity
    {
        public int ID { get; set; }

        [Display(Name = "Tài khoản")]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Tên")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }

        [Display(Name = "Ngày sinh")]
        public DateTime Birthday { get; set; }

        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Display(Name = "Giới tính")]
        public SexType Sex { get; set; }


        [ForeignKey("Office")]
        public int OfficeId { get; set; }

        public virtual Office Office { get; set; }

    }

}