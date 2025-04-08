using Assignment2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Assignment2.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasConversion(
                        v => v.ToString(),
                        v => (Gender)Enum.Parse(typeof(Gender), v));
                entity.Property(e => e.BirthPlace).IsRequired().HasMaxLength(100);
            });
        }
    }
}