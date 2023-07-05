using AutoMapper;
using HotelABC_API.Data;
using HotelABC_API.Models.Domain;
using HotelABC_API.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System;

namespace HotelABC_API.Repositories
{
    public class SQLRoomRepository : IRoomRepository
    {
        private readonly HotelDbContext hotelDbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;

        public SQLRoomRepository(HotelDbContext hotelDbContext, IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            this.hotelDbContext = hotelDbContext;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
        }

        public async Task<Room> CreateRoom(Room room)
        {
            await hotelDbContext.Rooms.AddAsync(room);
            await hotelDbContext.SaveChangesAsync();
            return room;
        }

        public async Task<Room?> DeleteRoom(Guid Id)
        {
            var roomDomain = await hotelDbContext.Rooms.Include(room => room.Images).FirstOrDefaultAsync(item => item.Id == Id);
            if (roomDomain == null)
            {
                return null;
            }
            foreach (var image in roomDomain.Images)
            {
                DeleteImageFromFolder(image.FilePath);
            }
            hotelDbContext.Rooms.Remove(roomDomain);
            await hotelDbContext.SaveChangesAsync();
            return (roomDomain);
        }

        public async Task<List<RoomDto>> GetAllRooms()
        {
            var rooms = await hotelDbContext.Rooms
                .Include(room => room.Images)
                .ToListAsync();
            var roomsDto = mapper.Map<List<RoomDto>>(rooms);
            //foreach (var room in roomsDto)
            //{
            //    var imagesDomain = await hotelDbContext.Images
            //        .Where(item => item.RelativeRelationId == room.Id)
            //        .Select(item => new ImageDto
            //        {
            //            FilePath = item.FilePath,
            //        })
            //        .ToListAsync();
            //    room.Images = imagesDomain;
            //}
            return roomsDto;
        }

        public async Task<Room?> GetById(Guid Id)
        {
            var room = await hotelDbContext.Rooms
                .Include(room => room.Images)
                .FirstOrDefaultAsync(item => item.Id == Id);
            return room;
        }

        public async Task<Room?> UpdateRoom(Guid Id, Room room)
        {
            var roomDomain = await hotelDbContext.Rooms.FirstOrDefaultAsync(item => item.Id == Id);
            if (roomDomain == null)
            {
                return null;
            }
            roomDomain.Name = room.Name;
            roomDomain.Description = room.Description;
            roomDomain.Characteristics = room.Characteristics;
            roomDomain.Price = room.Price;
            await hotelDbContext.SaveChangesAsync();

            return roomDomain;
        }

        public async Task<Image> UploadImage(Image image)
        {
            string unique_name = GenerateUniqueFileName();
            string extensionImage = Path.GetExtension(image.File.FileName);
            // store files in our app... images folder
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{unique_name}{extensionImage}");

            //Upload Image to local Path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);


            // https://localhost:1234/images/image.jpg
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{unique_name}{extensionImage}";
            image.FilePath = urlFilePath;

            // Add Image to the images table
            await hotelDbContext.Images.AddAsync(image);
            await hotelDbContext.SaveChangesAsync();

            return image;
        }

        public void DeleteImageFromFolder(string image_name)
        {
            string[] segments = image_name.Split('/');

            string folderPath = Path.Combine(webHostEnvironment.ContentRootPath, "Images");
            string filePath = Path.Combine(folderPath, segments[segments.Length - 1]);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        private string GenerateUniqueFileName()
        {
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            return timestamp;
        }
    }
}
