using Microsoft.EntityFrameworkCore;
using CrudApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudApp.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .HasMany<Employee>(d => d.Employees)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentCode);

            modelBuilder.Entity<Department>()
                .HasKey(d => d.Code);

            modelBuilder.Entity<Employee>()
                .HasKey(e => e.Code);

            modelBuilder.Entity<Department>()
                .HasIndex(d => d.Name).IsUnique();

        }
    }
}
