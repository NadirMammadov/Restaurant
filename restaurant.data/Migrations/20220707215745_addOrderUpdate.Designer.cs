﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using restaurant.data.Concrete.EfCore;

#nullable disable

namespace restaurant.data.Migrations
{
    [DbContext(typeof(ShopContext))]
    [Migration("20220707215745_addOrderUpdate")]
    partial class addOrderUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.5");

            modelBuilder.Entity("CategoryFood", b =>
                {
                    b.Property<int>("CategoriesCategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FoodsFoodId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CategoriesCategoryId", "FoodsFoodId");

                    b.HasIndex("FoodsFoodId");

                    b.ToTable("CategoryFood");
                });

            modelBuilder.Entity("restaurant.entity.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("restaurant.entity.CartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CartId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FoodId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("FoodId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("restaurant.entity.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("restaurant.entity.Food", b =>
                {
                    b.Property<int>("FoodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("FoodId");

                    b.ToTable("Foods");
                });

            modelBuilder.Entity("restaurant.entity.FoodCategory", b =>
                {
                    b.Property<int>("FoodId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.HasKey("FoodId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("FoodCategories");
                });

            modelBuilder.Entity("restaurant.entity.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ConversationId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("OrderNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("OrderState")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PaymentId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("restaurant.entity.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("FoodId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OrderId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FoodId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("CategoryFood", b =>
                {
                    b.HasOne("restaurant.entity.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("restaurant.entity.Food", null)
                        .WithMany()
                        .HasForeignKey("FoodsFoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("restaurant.entity.CartItem", b =>
                {
                    b.HasOne("restaurant.entity.Cart", "Cart")
                        .WithMany("CartItems")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("restaurant.entity.Food", "Food")
                        .WithMany()
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Food");
                });

            modelBuilder.Entity("restaurant.entity.FoodCategory", b =>
                {
                    b.HasOne("restaurant.entity.Category", "Category")
                        .WithMany("FoodCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("restaurant.entity.Food", "Food")
                        .WithMany("FoodCategories")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Food");
                });

            modelBuilder.Entity("restaurant.entity.OrderItem", b =>
                {
                    b.HasOne("restaurant.entity.Food", "Food")
                        .WithMany()
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("restaurant.entity.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Food");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("restaurant.entity.Cart", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("restaurant.entity.Category", b =>
                {
                    b.Navigation("FoodCategories");
                });

            modelBuilder.Entity("restaurant.entity.Food", b =>
                {
                    b.Navigation("FoodCategories");
                });

            modelBuilder.Entity("restaurant.entity.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
