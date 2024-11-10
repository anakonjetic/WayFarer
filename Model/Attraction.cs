using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WayFarer.Model.Enum;

namespace WayFarer.Model
{
    public class Attraction
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }

        [ForeignKey("City")]
        public int CityId { get; set; }
        public virtual City? City { get; set; }

        public virtual ICollection<Review>? Reviews { get; set; }

    /*    public Attraction(string name, Category category, decimal price, int cityId)
        {
            Name = name;
            Category = category;
            Price = price;
            this.cityId = cityId;
        }*/

    }
}
