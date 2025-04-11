using Microsoft.EntityFrameworkCore;
using EFCoreWebAPI.Models;

namespace EFCoreWebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectEmployee> ProjectEmployees { get; set; }
        public DbSet<Salary> Salaries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<Department>()
                .Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Employee>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Salary)
                .WithOne(s => s.Employee)
                .HasForeignKey<Salary>(s => s.EmployeeId);

            modelBuilder.Entity<Project>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Project>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<ProjectEmployee>()
                .HasKey(pe => new { pe.ProjectId, pe.EmployeeId });

            modelBuilder.Entity<ProjectEmployee>()
                .HasOne(pe => pe.Project)
                .WithMany(p => p.ProjectEmployees)
                .HasForeignKey(pe => pe.ProjectId);

            modelBuilder.Entity<ProjectEmployee>()
                .HasOne(pe => pe.Employee)
                .WithMany(e => e.ProjectEmployees)
                .HasForeignKey(pe => pe.EmployeeId);

            modelBuilder.Entity<Salary>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Salary>()
                .Property(s => s.EmployeeId)
                .IsRequired();

            modelBuilder.Entity<Salary>(entity =>
            {
                entity.Property(s => s.Amount)
                      .HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Department>().HasData(
               new Department { Id = 1, Name = "Software Development" },
               new Department { Id = 2, Name = "Finance" },
               new Department { Id = 3, Name = "Accountant" },
               new Department { Id = 4, Name = "HR" }
           );

            modelBuilder.Entity<Project>().HasData(
                new Project { Id = 1, Name = "Project A" },
                new Project { Id = 2, Name = "Project B" }
            );
        }
    }
}