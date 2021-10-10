using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using SuperBrandsTest.Entities.Models;
namespace SuperBrandsTest.Entities
{
    public class BrandDBContext : DbContext
    {
        public BrandDBContext(DbContextOptions<BrandDBContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Brand>().HasIndex(b => b.Name).IsUnique();

            modelBuilder.Entity<Size>().HasIndex(s => new { s.BrandSize, s.BrandId }).IsUnique();
            modelBuilder.Entity<Size>().HasIndex(s => new { s.RussianSize, s.BrandId }).IsUnique();
            modelBuilder.Entity<Size>().Property(s => s.BrandSize).IsRequired();
            modelBuilder.Entity<Size>().Property(s => s.RussianSize).IsRequired();

            modelBuilder.Entity<Product>().Property(p => p.BrandId).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.SizeId).IsRequired();
        }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Product> Products { get; set; } 
    }
}
