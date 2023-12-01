﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TPI_NapolitanoSalinasVazquez_P3.Data;

#nullable disable

namespace TPI_NapolitanoSalinasVazquez_P3.Migrations
{
    [DbContext(typeof(TPI_NapolitanoSalinasVazquez_P3Context))]
    partial class TPI_NapolitanoSalinasVazquez_P3ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.13");

            modelBuilder.Entity("TPI_NapolitanoSalinasVazquez_P3.Models.History", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductIds")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductNames")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Histories");
                });

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

            modelBuilder.Entity("TPI_NapolitanoSalinasVazquez_P3.Models.ShoppingCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("productId")
                        .HasColumnType("INTEGER");

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

                    b.Property<int>("paymentMethod")
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
                            paymentMethod = 0,
                            lastConnection = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("TPI_NapolitanoSalinasVazquez_P3.Models.Client", b =>
                {
                    b.HasBaseType("TPI_NapolitanoSalinasVazquez_P3.Models.User");

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("TPI_NapolitanoSalinasVazquez_P3.Models.History", b =>
                {
                    b.HasOne("TPI_NapolitanoSalinasVazquez_P3.Models.User", null)
                        .WithMany("History")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TPI_NapolitanoSalinasVazquez_P3.Models.User", b =>
                {
                    b.Navigation("History");
                });
#pragma warning restore 612, 618
        }
    }
}
