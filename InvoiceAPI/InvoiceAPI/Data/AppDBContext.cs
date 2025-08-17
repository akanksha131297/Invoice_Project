using System;
using InvoiceAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoiceAPI.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
        public DbSet<Invoice> Invoices { get; set; } = null!;
        public DbSet<InvoiceLines> InvoiceLines { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Invoice primary key configuration
            modelBuilder.Entity<Invoice>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<Invoice>()
                .Property(i => i.Id)
                .ValueGeneratedOnAdd(); // Auto-generate Id

            // InvoiceLines primary key configuration
            modelBuilder.Entity<InvoiceLines>()
                .HasKey(il => il.Id);

            modelBuilder.Entity<InvoiceLines>()
                .Property(il => il.Id)
                .ValueGeneratedOnAdd(); // Auto-generate Id

            // Relationship
            modelBuilder.Entity<Invoice>()
                .HasMany(i => i.Lines)
                .WithOne(il => il.Invoice)
                .HasForeignKey(il => il.InvoiceId);
        }

    }
}


