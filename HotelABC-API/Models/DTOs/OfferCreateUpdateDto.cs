using HotelABC_API.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace HotelABC_API.Models.DTOs
{
    public class OfferCreateUpdateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public IFormFile File { get; set; }
    }
}
