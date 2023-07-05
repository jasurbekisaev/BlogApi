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

        DbSet<Blog> Blogs { get; set; }
        DbSet<Like> Likes { get; set; }
        DbSet<SavedPosts> SavedPosts { get; set; }
        DbSet<Post> Posts { get; set; }
        DbSet<User> Users { get; set; }

    }
}
