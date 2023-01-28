using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using restaurant.entity;

namespace restaurant.data.Concrete.EfCore
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions options):base (options)
        {
            
        }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<FoodCategory> FoodCategories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlite("Data Source=shopDb");
        // }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FoodCategory>()
                .HasKey(t => new { t.FoodId, t.CategoryId });
            modelBuilder.Entity<FoodCategory>()
                .HasOne(pc => pc.Food)
                .WithMany(p => p.FoodCategories)
                .HasForeignKey(pc => pc.FoodId);
            
            modelBuilder.Entity<FoodCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.FoodCategories)
                .HasForeignKey(pc => pc.CategoryId);


        }
    }
}