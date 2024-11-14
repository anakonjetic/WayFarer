using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace WayFarer.Model
{
    public class ItineraryFormViewModel
    {
        [Required]
        public int CityId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "TotalPrice must be a positive value.")]
        public decimal TotalPrice { get; set; }

        [Required]
        public int UserId { get; set; }

        // [BindNever] to exclude field from validation
        [BindNever]
        public List<City>? Cities { get; set; }
    }

}
