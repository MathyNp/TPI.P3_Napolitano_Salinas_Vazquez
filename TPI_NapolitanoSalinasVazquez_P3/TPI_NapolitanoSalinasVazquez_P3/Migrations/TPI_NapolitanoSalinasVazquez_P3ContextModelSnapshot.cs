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

            modelBuilder.Entity("TPI_NapolitanoSalinasVazquez_P3.Models.Product", b =>
                {
                    b.Property<int>("productID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("productName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("productPrice")
                        .HasColumnType("INTEGER");

                    b.Property<int>("productStock")
                        .HasColumnType("INTEGER");

                    b.HasKey("productID");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("TPI_NapolitanoSalinasVazquez_P3.Models.User", b =>
                {
                    b.Property<int>("userID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("userMail")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("userName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("userPassword")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("userRol")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("userID");

                    b.ToTable("User");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("TPI_NapolitanoSalinasVazquez_P3.Models.Admin", b =>
                {
                    b.HasBaseType("TPI_NapolitanoSalinasVazquez_P3.Models.User");

                    b.Property<DateTime>("lastConnection")
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("TPI_NapolitanoSalinasVazquez_P3.Models.Client", b =>
                {
                    b.HasBaseType("TPI_NapolitanoSalinasVazquez_P3.Models.User");

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("paymentMethod")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("Client");
                });
#pragma warning restore 612, 618
        }
    }
}
