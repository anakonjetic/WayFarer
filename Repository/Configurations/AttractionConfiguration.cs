using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WayFarer.Model;

namespace WayFarer.Repository.Configurations
{
    public class AttractionConfiguration : IEntityTypeConfiguration<Attraction>
    {
        public void Configure(EntityTypeBuilder<Attraction> builder)
        {
            builder.ToTable(nameof(Attraction));
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
               .HasMaxLength(50)
               .IsRequired();

            builder.Property(p => p.Category)
               .HasMaxLength(50)
               .IsRequired();

            builder.Property(p => p.Price)
               .IsRequired();

            builder.Property(p => p.Image)
               .HasMaxLength(4000);

            builder.Property(p => p.cityId)
               .IsRequired();

            builder.Ignore(p => p.City);
            builder.Ignore(p => p.Reviews);
        }
    }
}
