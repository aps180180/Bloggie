using Bloggie.Web.Mappings;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Data
{
    public class BloggieDbContext : DbContext
    {
        public BloggieDbContext(DbContextOptions<BloggieDbContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;
        public DbSet<BlogPostLike> BlogPostLike { get; set; } = null!;
        public DbSet<BlogPostComment> Comments { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TagMap());
            modelBuilder.ApplyConfiguration(new BlogPostMap());
            modelBuilder.ApplyConfiguration(new BlogPostCommentMap());

        }
    }
}

