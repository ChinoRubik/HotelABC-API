using HotelABC_API.Data;
using HotelABC_API.Libs;
using HotelABC_API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace HotelABC_API.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly HotelDbContext hotelDbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly Utils utils;

        public ImageRepository(HotelDbContext hotelDbContext, IWebHostEnvironment webHostEnvironment)
        {
            this.hotelDbContext = hotelDbContext;
            this.webHostEnvironment = webHostEnvironment;
            this.utils = new Utils(webHostEnvironment);
        }

        public async Task<Image?> DeleteImage(Guid Id)
        {
            var imageDomain = await hotelDbContext.Images.FirstOrDefaultAsync(item => item.Id == Id);
            if (imageDomain == null)
            {
                return null;
            }
            utils.DeleteImageFromFolder(imageDomain.FilePath);
          
            hotelDbContext.Images.Remove(imageDomain);
            await hotelDbContext.SaveChangesAsync();

            return imageDomain;
        }

        public async Task<List<Image>> GetAllImages()
        {
            //var typeImageId = await hotelDbContext.ImageTypes.FirstOrDefaultAsync(item => item.Type == "room");
            //var imagesDomain = await hotelDbContext.Images.Where(item => item.ImageTypeId == typeImageId).ToListAsync();
            var imagesDomain = await hotelDbContext.Images
                .Join(
                    hotelDbContext.ImageTypes,
                    image => image.ImageTypeId,
                    imageType => imageType.Id,
                    (image, imageType) => new { Image = image, ImageType = imageType }
                )
                .Where(joinResult => joinResult.ImageType.Type == "room")
                .Select(joinResult => joinResult.Image)
                .ToListAsync();

            return imagesDomain;
        }
    }
}
