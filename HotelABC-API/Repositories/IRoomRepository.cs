using HotelABC_API.Models.Domain;
using HotelABC_API.Models.DTOs;

namespace HotelABC_API.Repositories
{
    public interface IRoomRepository
    {
        Task<List<RoomDto>> GetAllRooms();
        Task<RoomDetailDto?> GetById(Guid Id);
        Task<Room> CreateRoom(Room room);

        Task<Room?> UpdateRoom(Guid Id, Room room);

        Task<Room?> DeleteRoom(Guid Id);

        Task<Room> PatchRoom(Room room, CreateRoomDto createRoomDto);

    }
}
