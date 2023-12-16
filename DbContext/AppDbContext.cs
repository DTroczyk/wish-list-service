using Microsoft.EntityFrameworkCore;

namespace WishListApi.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> User { get; set; } = null!;
        public DbSet<Wish> Wish { get; set; } = null!;
        public DbSet<WishUser> WishUser { get; set; } = null!;
        public DbSet<Friend> Friend { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(e => e.Wishes).WithOne(e => e.User).HasForeignKey(e => e.UserId).IsRequired();
            modelBuilder.Entity<User>().HasIndex(e => e.Email);

            modelBuilder.Entity<WishUser>().HasKey(e => new { e.AssignedWishId, e.AssignedUserLogin });

            modelBuilder.Entity<Friend>().HasKey(e => new { e.UserId1, e.UserId2 });
        }
    }
}