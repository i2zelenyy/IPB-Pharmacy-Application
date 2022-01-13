﻿using Microsoft.EntityFrameworkCore;
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
                .HasMany<Baskets>(g => g.Baskets)
                .WithOne(s => s.Users)
                .HasForeignKey(s => s.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Medicine>()
                .HasMany<Baskets>(g => g.Baskets)
                .WithOne(s => s.Medicine)
                .HasForeignKey(s => s.MedicineID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
