using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WayFarer.Model;

namespace WayFarer.Repository.Configurations
{
    public class ItineraryConfiguration : IEntityTypeConfiguration<Itinerary>
    {
        public void Configure(EntityTypeBuilder<Itinerary> builder)
        {
            builder.ToTable(nameof(Itinerary));
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.StartDate)
               .IsRequired();

            builder.Property(p => p.EndDate)
               .IsRequired();

            builder.Property(p => p.TotalPrice)
               .IsRequired();

            builder.Property(p => p.UserId)
               .IsRequired();

            builder.Property(p => p.CityId)
               .IsRequired();

            builder.Ignore(p => p.User);
            builder.Ignore(p => p.City);
            builder.Ignore(p => p.Attractions);
        }
    }
}
