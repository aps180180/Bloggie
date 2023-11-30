using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bloggie.Web.Mappings
{
    public class BlogPostMap : IEntityTypeConfiguration<BlogPost>
    {
        public void Configure(EntityTypeBuilder<BlogPost> builder)
        {
            builder.ToTable("Posts");
            builder.HasKey(x => x.Id);
            builder.Property("Heading")
                .HasColumnName("Heading")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(200)
                .IsRequired();
            builder.Property("PageTitle")
                .HasColumnName("PageTitle")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property("Content")
                .HasColumnName("Content")
                .HasColumnType("NVARCHAR(max)")
                .HasMaxLength(1000)
                .IsRequired();
            
            builder.Property("ShortDescription")
                .HasColumnName("ShortDescription")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property("FeaturedImageUrl")
                .HasColumnName("FeaturedImageUrl")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property("UrlHandle")
                .HasColumnName("UrlHandle")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property("PublishedDate")
                .HasColumnName("PublishedDate")
                .HasColumnType("DateTime")
                .IsRequired();

            builder.Property("Author")
                .HasColumnName("Author")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property("Visible")
                .HasColumnName("Visible")
                .IsRequired();

            //relacionamento um para muitos de Blogposts para Tags 
            builder.HasMany(bp => bp.Tags)
                .WithOne(tag => tag.BlogPost)
                .HasForeignKey(tag => tag.BlogPostId);

            //relacionamento um para muitos de Blogposts para BlogPostLikes 
            builder.HasMany(bp => bp.Likes)
                .WithOne(like => like.BlogPost)
                .HasForeignKey(like => like.BlogPostId);


            //relacionamento um para muitos de Blogposts para BlogPostComments 
            builder.HasMany(bp => bp.Comments)
                .WithOne(comments => comments.BlogPost)
                .HasForeignKey(comments => comments.BlogPostId);


        }
    }
}
