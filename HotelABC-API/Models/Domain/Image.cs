using System.ComponentModel.DataAnnotations.Schema;

namespace HotelABC_API.Models.Domain
{
    public class Image
    {
        public Guid Id { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        public string FilePath { get; set; }
        public Guid RelativeRelationId { get; set; }
        public Guid ImageTypeId { get; set; }



        //Possible navegation in future
        public Room Room { get; set; }
    }
}
