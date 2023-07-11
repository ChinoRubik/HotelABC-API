using HotelABC_API.Models.Domain;

namespace HotelABC_API.Models.DTOs
{
    public class OfferDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string? imagePath { get; set; }
    }
}
