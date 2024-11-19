namespace WayFarer.Model
{
    public class Wishlist
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CityId { get; set; }

        public virtual User? User { get; set; }
        public virtual City? City { get; set; }
    }

}
