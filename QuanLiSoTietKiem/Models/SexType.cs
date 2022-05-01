using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLiSoTietKiem.Models
{
    public enum SexType
    {
        [Display(Name = "Nam")]
        MALE,
        [Display(Name = "Nữ")]
        FEMALE
    }
}