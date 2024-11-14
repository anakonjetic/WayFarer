﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WayFarer.Repository;

#nullable disable

namespace WayFarer.Migrations
{
    [DbContext(typeof(WayFarerDbContext))]
    [Migration("20241109185346_UserIsActive")]
    partial class UserIsActive
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.35")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WayFarer.Model.Attraction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Category")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("cityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("cityId");

                    b.ToTable("Attraction", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = 4,
                            Name = "Bitange i princeze",
                            Price = 7m,
                            cityId = 1
                        });
                });

            modelBuilder.Entity("WayFarer.Model.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("City", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Zagreb je najljepši grad!",
                            Image = "https://wallpapercave.com/wp/wp2333635.jpg",
                            Name = "Zagreb"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Pariz je grad ljubavi!",
                            Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT79lMmLbkyF2Dj2u1pNmWrjlUZfAjDQak0VA&s",
                            Name = "Pariz"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Gospić je najveći grad u Europi!",
                            Image = "https://www.mare-vrbnik.com/public/uploads/photos/articles/_gospic.jpg",
                            Name = "Gospić"
                        });
                });

            modelBuilder.Entity("WayFarer.Model.Itinerary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Itinerary", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CityId = 1,
                            EndDate = new DateTime(2024, 11, 9, 19, 53, 46, 330, DateTimeKind.Local).AddTicks(4825),
                            StartDate = new DateTime(2024, 11, 9, 19, 53, 46, 330, DateTimeKind.Local).AddTicks(4823),
                            TotalPrice = 0m,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("WayFarer.Model.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("attractionId")
                        .HasColumnType("int");

                    b.Property<int>("cityId")
                        .HasColumnType("int");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Review", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Comment = "Najbolje mjesto u gradu, uživali smo u noći pjesnika!",
                            Rating = 5,
                            attractionId = 0,
                            cityId = 0,
                            userId = 1
                        });
                });

            modelBuilder.Entity("WayFarer.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Gender")
                        .HasMaxLength(20)
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("Role")
                        .HasMaxLength(20)
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateOfBirth = new DateTime(2024, 11, 9, 19, 53, 46, 330, DateTimeKind.Local).AddTicks(4553),
                            Email = "skorkut@gmail.com",
                            Gender = 0,
                            IsActive = true,
                            Name = "Srećko",
                            Password = "divasGusteglata",
                            Role = 0,
                            Surname = "Korkut",
                            Username = "caslavBenzoni"
                        },
                        new
                        {
                            Id = 2,
                            DateOfBirth = new DateTime(2024, 11, 9, 19, 53, 46, 330, DateTimeKind.Local).AddTicks(4713),
                            Email = "ignacijefuchs@gmail.com",
                            Gender = 0,
                            IsActive = true,
                            Name = "Vatroslav",
                            Password = "goriArena123",
                            Role = 1,
                            Surname = "Lisinski",
                            Username = "ignacijeFux"
                        });
                });

            modelBuilder.Entity("WayFarer.Model.Attraction", b =>
                {
                    b.HasOne("WayFarer.Model.City", "City")
                        .WithMany("Attractions")
                        .HasForeignKey("cityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("WayFarer.Model.Itinerary", b =>
                {
                    b.HasOne("WayFarer.Model.City", "City")
                        .WithMany("Itineraries")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("WayFarer.Model.City", b =>
                {
                    b.Navigation("Attractions");

                    b.Navigation("Itineraries");
                });
#pragma warning restore 612, 618
        }
    }
}
