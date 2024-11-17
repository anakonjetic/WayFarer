using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WayFarer.Model;

namespace WayFarer.Repository.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable(nameof(Review));
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Comment)
               .HasMaxLength(1000)
               .IsRequired();

            builder.Property(p => p.Rating)
               .IsRequired();

            builder.Property(p => p.UserId)
                .IsRequired();

            builder.Property(p => p.CityId)
                .IsRequired(false);

            builder.Property(p => p.AttractionId)
                .IsRequired(false);

            builder.Ignore(p => p.User);
            builder.Ignore(p => p.City);
            builder.Ignore(p => p.Attraction);
        }
    }
}
