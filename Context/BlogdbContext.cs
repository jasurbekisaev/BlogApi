using BlogApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Context
{
    public class BlogdbContext : DbContext
    {
        public BlogdbContext(DbContextOptions<BlogdbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<SavedPosts> SavedPosts { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName);
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserBlogs)
                .WithOne(u => u.User)
                .HasForeignKey(b => b.UserId);
            modelBuilder.Entity<Blog>()
                .HasKey(b => b.BlogId);
            modelBuilder.Entity<Blog>()
                .HasMany(b => b.BlogPosts)
                .WithOne(p => p.Blog)
                .HasForeignKey(p => p.BlogId);
            modelBuilder.Entity<User>()
                .HasMany(u => u.SavedPosts)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId);
            modelBuilder.Entity<Post>()
                .HasMany(p => p.PostLikes)
                .WithOne(l => l.Post)
                .HasForeignKey(l => l.PostId);
            modelBuilder.Entity<Post>()
                .HasMany(p => p.SavedPosts)
                .WithOne(l => l.Post)
                .HasForeignKey(l => l.PostId);
        }
    }
}
