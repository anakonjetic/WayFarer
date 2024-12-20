﻿using System.ComponentModel.DataAnnotations;

namespace WayFarer.Model
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Image { get; set; }

        public virtual ICollection<Attraction>? Attractions { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
        public virtual ICollection<Itinerary>? Itineraries { get; set; }
        public virtual ICollection<Wishlist>? Wishlists { get; set; }
    }
}
