using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WishListApi.Context;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<File> Files { get; set; }

    public virtual DbSet<Friend> Friends { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VisibilityStatus> VisibilityStatuses { get; set; }

    public virtual DbSet<Wish> Wishes { get; set; }

    public virtual DbSet<WishContribute> WishContributes { get; set; }

    public virtual DbSet<WishStatus> WishStatuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:Default");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<File>(entity =>
        {
            entity.HasKey(e => e.FileId).HasName("PK__File__6F0F98BF6A859819");

            entity.ToTable("File");

            entity.Property(e => e.FileName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FilePath)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FileType)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

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
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RegisterDate).HasColumnType("datetime");

            entity.HasOne(d => d.AvatarImageNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.AvatarImage)
                .HasConstraintName("FK__User__AvatarImag__5165187F");
        });

        modelBuilder.Entity<VisibilityStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Visibili__3214EC07556E0425");

            entity.ToTable("VisibilityStatus");

            entity.HasIndex(e => e.Status, "UQ__Visibili__3A15923F92B05598").IsUnique();

            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Wish>(entity =>
        {
            entity.HasKey(e => e.WishId).HasName("PK__Wish__64BA62A1C611824F");

            entity.ToTable("Wish", "wishlist");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Deadline).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 4)");

            entity.HasOne(d => d.ImageNavigation).WithMany(p => p.Wishes)
                .HasForeignKey(d => d.Image)
                .HasConstraintName("FK__Wish__Image__6E01572D");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Wishes)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Wish__Status__6EF57B66");

            entity.HasOne(d => d.User).WithMany(p => p.Wishes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Wish__UserId__6D0D32F4");

            entity.HasOne(d => d.VisibilityNavigation).WithMany(p => p.Wishes)
                .HasForeignKey(d => d.Visibility)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Wish__Visibility__6FE99F9F");
        });

        modelBuilder.Entity<WishContribute>(entity =>
        {
            entity.HasKey(e => e.WishContributeId).HasName("PK__WishCont__1403072AF13353FE");

            entity.ToTable("WishContribute", "wishlist");

            entity.Property(e => e.Contribution).HasColumnType("decimal(10, 4)");

            entity.HasOne(d => d.User).WithMany(p => p.WishContributes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WishContr__UserI__73BA3083");

            entity.HasOne(d => d.Wish).WithMany(p => p.WishContributes)
                .HasForeignKey(d => d.WishId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WishContr__WishI__72C60C4A");
        });

        modelBuilder.Entity<WishStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__WishStat__3214EC073FDD225B");

            entity.ToTable("WishStatus", "wishlist");

            entity.HasIndex(e => e.Status, "UQ__WishStat__3A15923FE6B92A3E").IsUnique();

            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
