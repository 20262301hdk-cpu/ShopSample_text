using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using ShopSample.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ShopSample.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);  // 追加（Identityテーブルの設定に必要）

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
                new Product { Id = 3, Name = "プログラミング入門", Price = 3500, CategoryId = 3 },
                new Product { Id = 4, Name = "みかん", Price = 150, CategoryId = 1 },
                new Product { Id = 5, Name = "イヤホン", Price = 5000, CategoryId = 2 },
                new Product { Id = 6, Name = "C#入門", Price = 2800, CategoryId = 3 }
            );

            // Seed data for Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1,
                    Name = "田中太郎",
                    Email = "tanaka@example.com",
                    RegisteredDate = new DateTime(2024, 4, 1)
                },
                new Customer
                {
                    Id = 2,
                    Name = "鈴木花子",
                    Email = "suzuki@example.com",
                    RegisteredDate = new DateTime(2024, 6, 15)
                },
                new Customer
                {
                    Id = 3,
                    Name = "佐藤健一",
                    Email = "sato@example.com",
                    RegisteredDate = new DateTime(2024, 9, 20)
                }
            );

            // Seed data for Orders
            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, CustomerId = 1, OrderDate = new DateTime(2024, 10, 1) },
                new Order { Id = 2, CustomerId = 2, OrderDate = new DateTime(2024, 10, 5) },
                new Order { Id = 3, CustomerId = 1, OrderDate = new DateTime(2024, 11, 10) },
                new Order { Id = 4, CustomerId = 3, OrderDate = new DateTime(2024, 11, 20) }
            );

            // Seed data for OrderDetails
            modelBuilder.Entity<OrderDetail>().HasData(
                // Order 1（田中太郎）: りんご x3, ノートPC x1
                new OrderDetail { Id = 1, OrderId = 1, ProductId = 1, Quantity = 3, UnitPrice = 200 },
                new OrderDetail { Id = 2, OrderId = 1, ProductId = 2, Quantity = 1, UnitPrice = 98000 },
                // Order 2（鈴木花子）: プログラミング入門 x1, C#入門 x1
                new OrderDetail { Id = 3, OrderId = 2, ProductId = 3, Quantity = 1, UnitPrice = 3500 },
                new OrderDetail { Id = 4, OrderId = 2, ProductId = 6, Quantity = 1, UnitPrice = 2800 },
                // Order 3（田中太郎）: みかん x10, イヤホン x2
                new OrderDetail { Id = 5, OrderId = 3, ProductId = 4, Quantity = 10, UnitPrice = 150 },
                new OrderDetail { Id = 6, OrderId = 3, ProductId = 5, Quantity = 2, UnitPrice = 5000 },
                // Order 4（佐藤健一）: りんご x5, C#入門 x1
                new OrderDetail { Id = 7, OrderId = 4, ProductId = 1, Quantity = 5, UnitPrice = 200 },
                new OrderDetail { Id = 8, OrderId = 4, ProductId = 6, Quantity = 1, UnitPrice = 2800 }
            );
        }
    }

}
