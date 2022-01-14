using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Infrastructure
{
    public class PharmacyDbContext: DbContext
    {
        public DbSet<Medicine> Medicine { get; set; }
        public DbSet<Stores> Stores { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Cheques> Cheques { get; set; }
        public DbSet<Baskets> Baskets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Pharmacy.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
                .HasMany<Baskets>(s => s.Baskets)
                .WithOne(s => s.Users)
                .HasForeignKey(s => s.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Medicine>()
                .HasMany<Baskets>(g => g.Baskets)
                .WithOne(s => s.Medicine)
                .HasForeignKey(s => s.MedicineID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Stores>()
                .HasMany<Cheques>(g => g.Cheques)
                .WithOne(s => s.Stores)
                .HasForeignKey(s => s.StoresID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Baskets>()
                .HasMany<Cheques>(g => g.Cheques)
                .WithOne(s => s.Baskets)
                .HasForeignKey(s => s.BasketsID)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
