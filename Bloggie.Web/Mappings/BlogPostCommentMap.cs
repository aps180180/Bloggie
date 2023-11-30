using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bloggie.Web.Mappings
{
    public class BlogPostCommentMap : IEntityTypeConfiguration<BlogPostComment>
    {
        public void Configure(EntityTypeBuilder<BlogPostComment> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(x => x.Id);
            
            builder.Property("Description")
                .HasColumnName("Description")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(300)
                .IsRequired();
            
            builder.Property("BlogPostId")
                .HasColumnName("BlogPostId")
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property("UserId")
                .HasColumnName("UserId")
                .HasColumnType("uniqueidentifier")
                .IsRequired();
            
            builder.Property("DateAdded")
                .HasColumnName("DateAdded")
                .HasColumnType("Datetime")
                .IsRequired();



        }
    }
}
