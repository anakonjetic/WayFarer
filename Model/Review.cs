using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WayFarer.Model
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }

        [ForeignKey("User")]
        public int userId { get; set; }
        public virtual User? User { get; set; }

        [ForeignKey("City")]
        public int cityId { get; set; }
        public virtual City? City { get; set; }

        [ForeignKey("Attraction")]
        public int attractionId { get; set; }
        public virtual Attraction? Attraction { get; set; }

        public Review(string comment, int rating, int userId)
        {
            Comment = comment;
            Rating = rating;
            this.userId = userId;
        }
    }
}
