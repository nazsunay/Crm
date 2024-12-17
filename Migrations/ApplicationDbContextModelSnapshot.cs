﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectDb.Data;

#nullable disable

namespace ProjectDb.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProjectDb.Models.AuthorizedPerson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("AuthorizedPersons");
                });

            modelBuilder.Entity("ProjectDb.Models.City", b =>
                {
                    b.Property<string>("CityCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CityDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CityCode");

                    b.HasIndex("CountryCode");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("ProjectDb.Models.Country", b =>
                {
                    b.Property<string>("CountryCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CountryDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CountryCode");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("ProjectDb.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorizePersonId")
                        .HasColumnType("int");

                    b.Property<string>("AuthorizedPersonName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CityCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DistrictCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FollowUpApprovalDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FollowUpType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdentityNum")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Language")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(17)
                        .HasColumnType("nvarchar(17)");

                    b.Property<string>("Source")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaxNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("CityCode");

                    b.HasIndex("CountryCode");

                    b.HasIndex("DistrictCode");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("ProjectDb.Models.District", b =>
                {
                    b.Property<string>("DistrictCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CityCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DistrictDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DistrictCode");

                    b.HasIndex("CityCode");

                    b.HasIndex("CountryCode");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("ProjectDb.Models.Login", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Logins");
                });

            modelBuilder.Entity("ProjectDb.Models.MeetingNote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Date")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastUpdatedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MeetingTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("RowGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("MeetingTypeId");

                    b.ToTable("MeetingNotes");
                });

            modelBuilder.Entity("ProjectDb.Models.MeetingType", b =>
                {
                    b.Property<int>("MeetingTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MeetingTypeId"));

                    b.Property<string>("MeetingTypeDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MeetingTypeId");

                    b.ToTable("MeetingTypes");
                });

            modelBuilder.Entity("ProjectDb.Models.Sector", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Sectors");
                });

            modelBuilder.Entity("ProjectDb.Models.AuthorizedPerson", b =>
                {
                    b.HasOne("ProjectDb.Models.Customer", "Customers")
                        .WithMany("AuthorizedPersons")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Customers");
                });

            modelBuilder.Entity("ProjectDb.Models.City", b =>
                {
                    b.HasOne("ProjectDb.Models.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryCode")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("ProjectDb.Models.Customer", b =>
                {
                    b.HasOne("ProjectDb.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityCode")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProjectDb.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryCode")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProjectDb.Models.District", "District")
                        .WithMany()
                        .HasForeignKey("DistrictCode")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("City");

                    b.Navigation("Country");

                    b.Navigation("District");
                });

            modelBuilder.Entity("ProjectDb.Models.District", b =>
                {
                    b.HasOne("ProjectDb.Models.City", "City")
                        .WithMany("Districts")
                        .HasForeignKey("CityCode")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProjectDb.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryCode")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("ProjectDb.Models.MeetingNote", b =>
                {
                    b.HasOne("ProjectDb.Models.Customer", "Customers")
                        .WithMany("MeetingNotes")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectDb.Models.MeetingType", "MeetingType")
                        .WithMany()
                        .HasForeignKey("MeetingTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customers");

                    b.Navigation("MeetingType");
                });

            modelBuilder.Entity("ProjectDb.Models.City", b =>
                {
                    b.Navigation("Districts");
                });

            modelBuilder.Entity("ProjectDb.Models.Country", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("ProjectDb.Models.Customer", b =>
                {
                    b.Navigation("AuthorizedPersons");

                    b.Navigation("MeetingNotes");
                });
#pragma warning restore 612, 618
        }
    }
}