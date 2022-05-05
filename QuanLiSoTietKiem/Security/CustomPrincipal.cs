using QuanLiSoTietKiem.DAL;
using QuanLiSoTietKiem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace QuanLiSoTietKiem.Security
{
    public class CustomPrincipal : IPrincipal
    {
        private ManageSavingContext db = new ManageSavingContext();
        private Staff Staff;

        public CustomPrincipal(string username)
        {
            var data = db.Staffs.Where(s => s.UserName.Equals(username));
            this.Staff = data.FirstOrDefault();
            this.Identity = new GenericIdentity(username);
        }

        public IIdentity Identity { get; set; }

        public bool IsInRole(string role)
        {
            string[] roles = role.Split(new char[] { ',' });
            return roles.Any(r => this.Staff.Office.ShortName.Contains(r));
        }
    }
}