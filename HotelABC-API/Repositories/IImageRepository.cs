
using HotelABC_API.Models.Domain;

namespace HotelABC_API.Repositories
{
    public interface IImageRepository
    {
        Task<List<Image>> GetAllImages();
        Task<Image?> DeleteImage(Guid Id);
    }
}
