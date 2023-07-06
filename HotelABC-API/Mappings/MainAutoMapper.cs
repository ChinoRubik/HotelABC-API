﻿using AutoMapper;
using HotelABC_API.Models.Domain;
using HotelABC_API.Models.DTOs;

namespace HotelABC_API.Mappings
{
    public class MainAutoMapper : Profile
    {
        public MainAutoMapper()
        {
            // origin - destination
            CreateMap<Room, RoomDto>().ReverseMap();
            //CreateMap<RoomDto, Room>().ReverseMap();
            CreateMap<CreateRoomDto, Room>().ReverseMap();
            CreateMap<Room, RoomDetailDto>().ReverseMap();
            CreateMap<RoomDetailDto, Room>().ReverseMap();
            CreateMap<Room, CreateRoomDto>().ReverseMap();
            CreateMap<Image, ImageDto>().ReverseMap();
        }
    }
}
