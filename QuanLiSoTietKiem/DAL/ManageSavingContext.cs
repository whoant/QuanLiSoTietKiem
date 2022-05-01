using QuanLiSoTietKiem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QuanLiSoTietKiem.DAL
{
    public class ManageSavingContext : DbContext
    {
        public ManageSavingContext()
           : base("name=ManageSavingConnection")
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<SavingBook> SavingBooks { get; set; }

    }
}