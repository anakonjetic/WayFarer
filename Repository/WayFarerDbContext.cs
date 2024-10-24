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


            modelBuilder.Entity<User>().HasData(new User("Srećko","Korkut", DateTime.Now, "skorkut@gmail.com", Gender.Male, Role.Administrator, "caslavBenzoni", "divasGusteglata") { Id = 1 });
            modelBuilder.Entity<City>().HasData(new City("Zagreb", "Zagreb je najljepši grad!") { Id = 1 });
            modelBuilder.Entity<Attraction>().HasData(new Attraction("Bitange i princeze", Category.Pub, 7, 1) { Id = 1});
            modelBuilder.Entity<Review>().HasData(new Review("Najbolje mjesto u gradu, uživali smo u noći pjesnika!", 5, 1) { Id = 1 });
            modelBuilder.Entity<Itinerary>().HasData(new Itinerary(DateTime.Now, DateTime.Now, 1, 0) { Id = 1 });


        }



    }
}
