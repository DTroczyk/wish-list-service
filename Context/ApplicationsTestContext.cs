using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WishListApi.Context;

public partial class ApplicationsTestContext : DbContext
{
    public ApplicationsTestContext()
    {
    }

    public ApplicationsTestContext(DbContextOptions<ApplicationsTestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Friend> Friends { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:Default");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Friend>(entity =>
        {
            entity.HasKey(e => e.FriendId).HasName("PK__Friend__A2CF658315924530");

            entity.ToTable("Friend");

            entity.HasIndex(e => new { e.UserId1, e.UserId2 }, "UQ__Friend__A87B8623115A6F16").IsUnique();

            entity.Property(e => e.AcceptDate).HasColumnType("datetime");

            entity.HasOne(d => d.UserId1Navigation).WithMany(p => p.FriendUserId1Navigations)
                .HasForeignKey(d => d.UserId1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Friend__UserId1__5535A963");

            entity.HasOne(d => d.UserId2Navigation).WithMany(p => p.FriendUserId2Navigations)
                .HasForeignKey(d => d.UserId2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Friend__UserId2__5629CD9C");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4CE10A2FDF");

            entity.ToTable("User");

            entity.HasIndex(e => e.Login, "UQ__User__5E55825B5647E0A8").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__User__A9D105CA247D7645").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RegisterDate).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
