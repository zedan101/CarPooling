﻿// <auto-generated />
using System;
using Carpool.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarPool.DataLayer.Migrations
{
    [DbContext(typeof(CarPoolContext))]
    partial class CarPoolContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CarPool.DataLayer.Models.BookedRide", b =>
                {
                    b.Property<int>("SlNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SlNo"));

                    b.Property<string>("RideId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("SlNo");

                    b.HasIndex("RideId");

                    b.HasIndex("UserId");

                    b.ToTable("BookedRide");

                    b.HasData(
                        new
                        {
                            SlNo = 1,
                            RideId = "abc@123",
                            UserId = "gh1"
                        },
                        new
                        {
                            SlNo = 2,
                            RideId = "abc@123",
                            UserId = "gh1"
                        });
                });

            modelBuilder.Entity("CarPool.DataLayer.Models.OfferedRide", b =>
                {
                    b.Property<string>("RideId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AvailableSeats")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Time")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RideId");

                    b.HasIndex("UserId");

                    b.ToTable("OfferedRide");

                    b.HasData(
                        new
                        {
                            RideId = "abc@123",
                            AvailableSeats = 2,
                            Date = new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Price = 100.0,
                            Time = 1,
                            UserId = "gh1"
                        });
                });

            modelBuilder.Entity("CarPool.DataLayer.Models.RideLocation", b =>
                {
                    b.Property<int>("SerialNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SerialNo"));

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RideId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("SequenceNum")
                        .HasColumnType("int");

                    b.HasKey("SerialNo");

                    b.HasIndex("RideId");

                    b.ToTable("RideLocation");

                    b.HasData(
                        new
                        {
                            SerialNo = 1,
                            Location = "Delhi",
                            RideId = "abc@123",
                            SequenceNum = 0
                        },
                        new
                        {
                            SerialNo = 2,
                            Location = "Mumbai",
                            RideId = "abc@123",
                            SequenceNum = 1
                        },
                        new
                        {
                            SerialNo = 3,
                            Location = "Nagpur",
                            RideId = "abc@123",
                            SequenceNum = 2
                        });
                });

            modelBuilder.Entity("CarPool.DataLayer.Models.UserEntity", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfileImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            UserId = "gh1",
                            Name = "Nitish",
                            Password = "Nitish%%",
                            ProfileImage = "hkdhk",
                            UserEmail = "nitish@132"
                        });
                });

            modelBuilder.Entity("CarPool.DataLayer.Models.BookedRide", b =>
                {
                    b.HasOne("CarPool.DataLayer.Models.OfferedRide", "OfferedRide")
                        .WithMany("BookedRides")
                        .HasForeignKey("RideId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarPool.DataLayer.Models.UserEntity", "User")
                        .WithMany("BookedRides")
                        .HasForeignKey("UserId");

                    b.Navigation("OfferedRide");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CarPool.DataLayer.Models.OfferedRide", b =>
                {
                    b.HasOne("CarPool.DataLayer.Models.UserEntity", "UserEntity")
                        .WithMany("OfferedRides")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("CarPool.DataLayer.Models.RideLocation", b =>
                {
                    b.HasOne("CarPool.DataLayer.Models.OfferedRide", "OfferRide")
                        .WithMany("Locations")
                        .HasForeignKey("RideId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OfferRide");
                });

            modelBuilder.Entity("CarPool.DataLayer.Models.OfferedRide", b =>
                {
                    b.Navigation("BookedRides");

                    b.Navigation("Locations");
                });

            modelBuilder.Entity("CarPool.DataLayer.Models.UserEntity", b =>
                {
                    b.Navigation("BookedRides");

                    b.Navigation("OfferedRides");
                });
#pragma warning restore 612, 618
        }
    }
}
