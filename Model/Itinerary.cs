using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WayFarer.Model
{
    public class Itinerary
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User? User { get; set; }

        [ForeignKey(nameof(City))]
        public int CityId { get; set; }
        public virtual City? City { get; set; }

        public virtual ICollection<Attraction>? Attractions { get; set; }

        public string? DailyPlans { get; set; }
    }
}
