
using HotelABC_API.Models.Domain;

namespace HotelABC_API.Repositories
{
    public interface IImageRepository
    {
        Task<List<Image>> GetAllImages(string type_image = "room");
        Task<Image?> DeleteImage(Guid Id);
        Task<Image> UploadImage(Image image);
    }
}
