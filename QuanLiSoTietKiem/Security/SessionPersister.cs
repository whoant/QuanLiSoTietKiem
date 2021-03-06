using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLiSoTietKiem.Security
{
    public static class SessionPersister
    {
        static string usernameSessionVar = "UserName";

        public static string UserName
        {
            get
            {
                if (HttpContext.Current == null) return string.Empty;
                var sessionVar = HttpContext.Current.Session[usernameSessionVar];
                if (sessionVar != null) return sessionVar as string;
                return null;
            }
            set
            {
                HttpContext.Current.Session[usernameSessionVar] = value;
            }
        }
    }
}