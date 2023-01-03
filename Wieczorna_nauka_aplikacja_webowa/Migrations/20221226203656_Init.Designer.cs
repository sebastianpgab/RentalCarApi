﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Wieczorna_nauka_aplikacja_webowa.Entities;

namespace Wieczorna_nauka_aplikacja_webowa.Migrations
{
    [DbContext(typeof(RentalCarDbContext))]
    [Migration("20221226203656_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Wieczorna_nauka_aplikacja_webowa.Entities.Address", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Wieczorna_nauka_aplikacja_webowa.Entities.RentalCar", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<bool>("Abroad")
                        .HasColumnType("bit");

                    b.Property<long>("AddressID")
                        .HasColumnType("bigint");

                    b.Property<bool>("Advance")
                        .HasColumnType("bit");

                    b.Property<string>("ContactEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AddressID")
                        .IsUnique();

                    b.ToTable("RentalCars");
                });

            modelBuilder.Entity("Wieczorna_nauka_aplikacja_webowa.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<DateTime?>("DateOfBrith")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nationality")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Wieczorna_nauka_aplikacja_webowa.Entities.Vehicle", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<DateTime>("FirstRegistration")
                        .HasColumnType("datetime2");

                    b.Property<bool>("HasFourWheelDrive")
                        .HasColumnType("bit");

                    b.Property<bool>("HasGas")
                        .HasColumnType("bit");

                    b.Property<string>("HorsePower")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("RegistrationNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("RentalCarId")
                        .HasColumnType("bigint");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RentalCarId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("Wieczorna_nauka_aplikacja_webowa.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Wieczorna_nauka_aplikacja_webowa.Entities.RentalCar", b =>
                {
                    b.HasOne("Wieczorna_nauka_aplikacja_webowa.Entities.Address", "Address")
                        .WithOne("RentalCar")
                        .HasForeignKey("Wieczorna_nauka_aplikacja_webowa.Entities.RentalCar", "AddressID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Wieczorna_nauka_aplikacja_webowa.Entities.User", b =>
                {
                    b.HasOne("Wieczorna_nauka_aplikacja_webowa.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Wieczorna_nauka_aplikacja_webowa.Entities.Vehicle", b =>
                {
                    b.HasOne("Wieczorna_nauka_aplikacja_webowa.Entities.RentalCar", "RentalCar")
                        .WithMany("Vehicles")
                        .HasForeignKey("RentalCarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RentalCar");
                });

            modelBuilder.Entity("Wieczorna_nauka_aplikacja_webowa.Entities.Address", b =>
                {
                    b.Navigation("RentalCar");
                });

            modelBuilder.Entity("Wieczorna_nauka_aplikacja_webowa.Entities.RentalCar", b =>
                {
                    b.Navigation("Vehicles");
                });
#pragma warning restore 612, 618
        }
    }
}
