using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Country> Countries { get; set; }

        public DbSet<HolidayForYear> HolidayForYears { get; set; }

        public DbSet<HolidayName> HolidayNames { get; set; }

        public DbSet<Day> Days { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Map entity to table
            modelBuilder.Entity<Country>().ToTable("Countries");
            modelBuilder.Entity<HolidayName>().ToTable("HolidayNames");
            modelBuilder.Entity<HolidayForYear>().ToTable("HolidayForYears");
            modelBuilder.Entity<Day>().ToTable("Days");
        }
    }
}
