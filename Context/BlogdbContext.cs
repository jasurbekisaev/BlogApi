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

    }
}
