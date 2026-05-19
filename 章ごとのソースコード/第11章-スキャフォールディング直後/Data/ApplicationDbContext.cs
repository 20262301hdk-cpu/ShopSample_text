using Microsoft.EntityFrameworkCore;
using ShopSample.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ShopSample.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data for Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "食品" },
                new Category { Id = 2, Name = "電化製品" },
                new Category { Id = 3, Name = "書籍" }
            );

            // Seed data for Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "りんご", Price = 200, CategoryId = 1 },
                new Product { Id = 2, Name = "ノートPC", Price = 98000, CategoryId = 2 },
                new Product { Id = 3, Name = "プログラミング入門", Price = 3500, CategoryId = 3 }
            );
        }
    }

}
