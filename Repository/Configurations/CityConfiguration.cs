using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WayFarer.Model;

namespace WayFarer.Repository.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable(nameof(City));
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
               .HasMaxLength(50)
               .IsRequired();

            builder.Property(p => p.Description)
               .HasMaxLength(50)
               .IsRequired();

            builder.Ignore(p => p.Attractions);
            builder.Ignore(p => p.Reviews);
        }
    }
}
