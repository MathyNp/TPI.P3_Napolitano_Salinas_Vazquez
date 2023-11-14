﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TPI_NapolitanoSalinasVazquez_P3.Data;

#nullable disable

namespace TPI_NapolitanoSalinasVazquez_P3.Migrations
{
    [DbContext(typeof(TPI_NapolitanoSalinasVazquez_P3Context))]
    [Migration("20231114023922_productdatabase")]
    partial class productdatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.13");

            modelBuilder.Entity("TPI_NapolitanoSalinasVazquez_P3.Models.Product", b =>
                {
                    b.Property<int>("productID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("productName")
                        .HasColumnType("TEXT");

                    b.Property<int>("productPrice")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("productState")
                        .HasColumnType("INTEGER");

                    b.Property<int>("productStock")
                        .HasColumnType("INTEGER");

                    b.HasKey("productID");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("TPI_NapolitanoSalinasVazquez_P3.Models.SaleOrderLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("ProductPrice")
                        .HasColumnType("TEXT");

                    b.Property<int>("ProductQuantity")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SaleOrderLineId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("SaleOrderLineId");

                    b.ToTable("SaleOrderLine");
                });

            modelBuilder.Entity("TPI_NapolitanoSalinasVazquez_P3.Models.ShoppingCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CartId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CartUser")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ShoppingCart");
                });

            modelBuilder.Entity("TPI_NapolitanoSalinasVazquez_P3.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserMail")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserRol")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("UserState")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserID");

                    b.ToTable("Users");

                    b.HasDiscriminator<int>("UserRol");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("TPI_NapolitanoSalinasVazquez_P3.Models.Admin", b =>
                {
                    b.HasBaseType("TPI_NapolitanoSalinasVazquez_P3.Models.User");

                    b.Property<DateTime>("lastConnection")
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue(0);

                    b.HasData(
                        new
                        {
                            UserID = 1,
                            UserMail = "admin@admin.com",
                            UserName = "admin",
                            UserPassword = "admin",
                            UserRol = 0,
                            UserState = true,
                            lastConnection = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("TPI_NapolitanoSalinasVazquez_P3.Models.Client", b =>
                {
                    b.HasBaseType("TPI_NapolitanoSalinasVazquez_P3.Models.User");

                    b.Property<int>("UserCartId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("paymentMethod")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasIndex("UserCartId")
                        .IsUnique();

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("TPI_NapolitanoSalinasVazquez_P3.Models.SaleOrderLine", b =>
                {
                    b.HasOne("TPI_NapolitanoSalinasVazquez_P3.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TPI_NapolitanoSalinasVazquez_P3.Models.ShoppingCart", null)
                        .WithMany("saleOrderLines")
                        .HasForeignKey("SaleOrderLineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("TPI_NapolitanoSalinasVazquez_P3.Models.Client", b =>
                {
                    b.HasOne("TPI_NapolitanoSalinasVazquez_P3.Models.ShoppingCart", "UserCart")
                        .WithOne()
                        .HasForeignKey("TPI_NapolitanoSalinasVazquez_P3.Models.Client", "UserCartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserCart");
                });

            modelBuilder.Entity("TPI_NapolitanoSalinasVazquez_P3.Models.ShoppingCart", b =>
                {
                    b.Navigation("saleOrderLines");
                });
#pragma warning restore 612, 618
        }
    }
}