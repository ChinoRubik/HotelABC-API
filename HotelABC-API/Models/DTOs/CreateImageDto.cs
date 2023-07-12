using System.ComponentModel.DataAnnotations;

namespace HotelABC_API.Models.DTOs
{
    public class CreateImageDto
    {
        [Required]
        public List<IFormFile> File { get; set; }

        [Required]
        public Guid ImageTypeId { get; set; }
    }
}
