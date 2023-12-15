﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WishListApi.Models;

#nullable disable

namespace WishListApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WishListApi.Models.Friend", b =>
                {
                    b.Property<string>("UserId1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId2")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("AcceptDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsUser1Accept")
                        .HasColumnType("bit");

                    b.Property<bool>("IsUser2Accept")
                        .HasColumnType("bit");

                    b.Property<string>("User1Login")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("User2Login")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId1", "UserId2");

                    b.HasIndex("User1Login");

                    b.HasIndex("User2Login");

                    b.ToTable("Friend");
                });

            modelBuilder.Entity("WishListApi.Models.User", b =>
                {
                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Login");

                    b.HasIndex("Email");

                    b.ToTable("User");
                });

            modelBuilder.Entity("WishListApi.Models.Wish", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsComplete")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMaxOne")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Wish");
                });

            modelBuilder.Entity("WishListApi.Models.WishUser", b =>
                {
                    b.Property<string>("AssignedWishId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AssignedUserLogin")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserLogin")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("WishId")
                        .HasColumnType("bigint");

                    b.HasKey("AssignedWishId", "AssignedUserLogin");

                    b.HasIndex("UserLogin");

                    b.HasIndex("WishId");

                    b.ToTable("WishUser");
                });

            modelBuilder.Entity("WishListApi.Models.Friend", b =>
                {
                    b.HasOne("WishListApi.Models.User", "User1")
                        .WithMany()
                        .HasForeignKey("User1Login");

                    b.HasOne("WishListApi.Models.User", "User2")
                        .WithMany()
                        .HasForeignKey("User2Login");

                    b.Navigation("User1");

                    b.Navigation("User2");
                });

            modelBuilder.Entity("WishListApi.Models.Wish", b =>
                {
                    b.HasOne("WishListApi.Models.User", "User")
                        .WithMany("Wishes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WishListApi.Models.WishUser", b =>
                {
                    b.HasOne("WishListApi.Models.User", "User")
                        .WithMany("AssignedTo")
                        .HasForeignKey("UserLogin");

                    b.HasOne("WishListApi.Models.Wish", "Wish")
                        .WithMany("AssignedTo")
                        .HasForeignKey("WishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Wish");
                });

            modelBuilder.Entity("WishListApi.Models.User", b =>
                {
                    b.Navigation("AssignedTo");

                    b.Navigation("Wishes");
                });

            modelBuilder.Entity("WishListApi.Models.Wish", b =>
                {
                    b.Navigation("AssignedTo");
                });
#pragma warning restore 612, 618
        }
    }
}
