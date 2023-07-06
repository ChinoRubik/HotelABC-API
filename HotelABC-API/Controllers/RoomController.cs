using AutoMapper;
using Azure;
using HotelABC_API.Models.Domain;
using HotelABC_API.Models.DTOs;
using HotelABC_API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;

namespace HotelABC_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository roomRepository;
        private readonly IMapper mapper;

        public RoomController(IRoomRepository roomRepository, IMapper mapper)
        {
            this.roomRepository = roomRepository;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roomsDtos = await roomRepository.GetAllRooms();

            return Ok(roomsDtos);
        }
        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetOne([FromRoute] Guid Id)
        {
            var room = await roomRepository.GetById(Id);
            if (room == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<RoomDto>(room));
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateRoom([FromForm] CreateRoomDto createRoomDto)
        {
            ValidateFileUpload(createRoomDto);

            if (ModelState.IsValid)
            {
                var roomDomain = mapper.Map<Room>(createRoomDto);
                await roomRepository.CreateRoom(roomDomain);

                var roomDto = mapper.Map<RoomDto>(roomDomain);
                foreach (var file in createRoomDto.File)
                {
                    var imageDomain = new Image
                    {
                        File = file,
                        // Room Id
                        RelativeRelationId = roomDomain.Id,
                        //Image Type Id
                        ImageTypeId = Guid.Parse("3897b275-7a3f-4a84-a620-105b9b0eb89a"),
                    };
                    var imageUploadedDomain = await roomRepository.UploadImage(imageDomain);
                    if (roomDto.Images == null)
                    {
                        roomDto.Images = new List<ImageDto> {mapper.Map<ImageDto>(imageUploadedDomain) };

                    }
                    else
                    {
                        roomDto.Images.Add(mapper.Map<ImageDto>(imageUploadedDomain));
                    }
                    
                }

                return Ok(roomDto);
            }

            return BadRequest();   
        }

        [HttpPut]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateRoom([FromRoute] Guid Id, [FromForm] CreateRoomDto updateRoomDto)
        {
            ValidateFileUpload(updateRoomDto);

            if (ModelState.IsValid)
            {
                var roomDomain = await roomRepository.UpdateRoom(Id, mapper.Map<Room>(updateRoomDto));
                if (roomDomain == null)
                {
                    return NotFound();
                }

                var roomDto = mapper.Map<RoomDto>(roomDomain);

                foreach (var file in updateRoomDto.File)
                {
                    var imageDomain = new Image
                    {
                        File = file,
                        RelativeRelationId = roomDomain.Id,
                        ImageTypeId = Guid.Parse("3897b275-7a3f-4a84-a620-105b9b0eb89a"),
                    };
                    var imageUploadedDomain = await roomRepository.UploadImage(imageDomain);
                    if (roomDto.Images == null)
                    {
                        roomDto.Images = new List<ImageDto> { mapper.Map<ImageDto>(imageUploadedDomain) };

                    }
                    else
                    {
                        roomDto.Images.Add(mapper.Map<ImageDto>(imageUploadedDomain));
                    }

                }
                return Ok(roomDto);


            }
            return BadRequest();
        }

        [HttpPatch]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> PatchRoom([FromRoute] Guid Id, [FromBody] JsonPatchDocument<CreateRoomDto> patchDocument)
        {
            var roomDomain = await roomRepository.GetById(Id);
            if (roomDomain == null)
            {
                return NotFound();
            }
            var roomDto = mapper.Map<CreateRoomDto>(roomDomain);
            patchDocument.ApplyTo(roomDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await roomRepository.PatchRoom(roomDomain, roomDto);
            return Ok(roomDto);
        }

        [HttpDelete]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteRoom([FromRoute] Guid Id)
        {
            var roomDomain = await roomRepository.DeleteRoom(Id);
            if (roomDomain == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<RoomDto>(roomDomain));
        }


        private void ValidateFileUpload(CreateRoomDto imageUploadRequestDto)
        {
            var allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };

            foreach (var file in imageUploadRequestDto.File)
            {
                if (!allowedExtension.Contains(Path.GetExtension(file.FileName)))
                {
                    ModelState.AddModelError("file", "Unsupported file extension");
                }

                if (file.Length > 10485760)
                {
                    ModelState.AddModelError("File", "File size more than 10MB, please upload a smaller file");
                }
            }
        }
    }
}
