using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WayFarer.Model;

namespace WayFarer.Repository.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
               .HasMaxLength(50)
               .IsRequired();

            builder.Property(p => p.Surname)
               .HasMaxLength(100)
               .IsRequired();

            builder.Property(p => p.DateOfBirth)
               .IsRequired();

            builder.Property(p => p.Role)
               .HasMaxLength(20)
               .IsRequired();

            builder.Property(p => p.Gender)
               .HasMaxLength(20)
               .IsRequired();

            builder.Property(p => p.Email)
               .HasMaxLength(50)
               .IsRequired();

            builder.Property(p => p.Username)
               .HasMaxLength(50)
               .IsRequired();

            builder.Property(p => p.Password)
               .HasMaxLength(30)
               .IsRequired();

            builder.Ignore(p => p.Itineraries);
            builder.Ignore(p => p.Reviews);
        }
    }
}
