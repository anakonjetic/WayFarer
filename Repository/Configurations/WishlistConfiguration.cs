using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WayFarer.Model;

namespace WayFarer.Repository.Configurations
{
    public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            builder.ToTable(nameof(Wishlist));

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.HasIndex(w => new { w.UserId, w.CityId }).IsUnique();  // Unikatni par (UserId, CityId)

            builder.HasOne(w => w.User)
                   .WithMany(u => u.Wishlists)  // Svaki korisnik može imati više stavki na wishlistu
                   .HasForeignKey(w => w.UserId)  // Strani ključ za User
                   .OnDelete(DeleteBehavior.Restrict);  // Onemogući brisanje korisnika ako postoji wishlist stavka

            builder.HasOne(w => w.City)
                   .WithMany(c => c.Wishlists)  // Svaki grad može biti na wishlistu više korisnika
                   .HasForeignKey(w => w.CityId)  // Strani ključ za City
                   .OnDelete(DeleteBehavior.Restrict);  // Onemogući brisanje grada ako postoji na wishlistu
        }
    }
}
