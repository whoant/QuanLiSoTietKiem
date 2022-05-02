using QuanLiSoTietKiem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace QuanLiSoTietKiem.DAL
{
    public class ManageSavingContext : DbContext
    {
        public ManageSavingContext()
           : base("name=ManageSavingConnection")
        {
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync()
        {
            AddTimestamps();
            return await base.SaveChangesAsync();
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow; // current datetime

                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedAt = now;
                }
                ((BaseEntity)entity.Entity).UpdatedAt = now;
            }
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<SavingBook> SavingBooks { get; set; }

        public DbSet<Interest> Interests { get; set; }

        public DbSet<Period> Periods { get; set; }

        public DbSet<Staff> Staffs { get; set; }

        public DbSet<Office> Offices { get; set; }

        public DbSet<FormCreate> FormCreates { get; set; }

        public DbSet<FormClose> FormCloses { get; set; }

    }
}