using Microsoft.EntityFrameworkCore;
using WayFarer.Model;
using WayFarer.Model.Enum;
using WayFarer.Repository.Configurations;

namespace WayFarer.Repository
{
    public class WayFarerDbContext : DbContext
    {
        public DbSet<User>? User { get; set; }
        public DbSet<Itinerary>? Itinerary { get; set; }
        public DbSet<City>? City { get; set; }
        public DbSet<Attraction>? Attraction { get; set; }
        public DbSet<Review>? Review { get; set; }

        public WayFarerDbContext(DbContextOptions<WayFarerDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("WayFarerDbContext");
            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ItineraryConfiguration());
            modelBuilder.ApplyConfiguration(new CityConfiguration());
            modelBuilder.ApplyConfiguration(new AttractionConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());

            modelBuilder.Entity<Itinerary>()
                .HasOne(i => i.City)
                .WithMany(c => c.Itineraries)
                .HasForeignKey(i => i.CityId);

            modelBuilder.Entity<Attraction>()
               .HasOne(i => i.City)
               .WithMany(c => c.Attractions)
               .HasForeignKey(i => i.CityId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.City)
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.CityId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Attraction)
                .WithMany(a => a.Reviews)
                .HasForeignKey(r => r.AttractionId);

            modelBuilder.Entity<User>().HasData(new User("Srećko", "Korkut", DateTime.Now, "skorkut@gmail.com", Gender.Male, Role.Administrator, "caslavBenzoni", "divasGusteglata") { Id = 1 });
            modelBuilder.Entity<User>().HasData(new User("Vatroslav", "Lisinski", DateTime.Now, "ignacijefuchs@gmail.com", Gender.Male, Role.Basic, "ignacijeFux", "goriArena123") { Id = 2 });
            modelBuilder.Entity<City>().HasData(new City{ Id = 1, Name = "Zagreb", Description = "Zagreb je najljepši grad!", Image = "https://wallpapercave.com/wp/wp2333635.jpg"});
            modelBuilder.Entity<City>().HasData(new City{ Id = 2, Name = "Pariz", Description = "Pariz je grad ljubavi!", Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT79lMmLbkyF2Dj2u1pNmWrjlUZfAjDQak0VA&s" });
            modelBuilder.Entity<City>().HasData(new City{ Id = 3, Name = "Gospić", Description = "Gospić je najveći grad u Europi!", Image = "https://www.mare-vrbnik.com/public/uploads/photos/articles/_gospic.jpg" });
            modelBuilder.Entity<Attraction>().HasData(new Attraction { Id = 1, Name = "Bitange i princeze", Category = Category.Pub, Price = 7, CityId = 1  });
            modelBuilder.Entity<Review>().HasData(new Review { Id = 1, Comment = "Najbolje mjesto u gradu, uživali smo u noći pjesnika!", Rating = 5, CityId = 0, AttractionId = 0});
            modelBuilder.Entity<Itinerary>().HasData(new Itinerary{ Id = 1, StartDate = DateTime.Now, EndDate = DateTime.Now, UserId = 1, TotalPrice = 0, CityId = 1});

        }
    }
}
