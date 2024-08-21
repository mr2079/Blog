using Article.Domain.Articles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Article.Infrastructure.Data.Configurations;

public class ArticleConfiguration : IEntityTypeConfiguration<ArticleEntity>
{
    public void Configure(EntityTypeBuilder<ArticleEntity> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.AuthorId)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(a => a.ImageName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(a => a.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(a => a.Tag)
            .HasMaxLength(255)
            .HasConversion(a => a!.Value, v => new Tag(v));

        builder
            .HasOne(a => a.Category)
            .WithMany(c => c.Articles)
            .HasForeignKey(a => a.CategoryId);
    }
}