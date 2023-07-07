using AutoMapper;
using HotelABC_API.Models.DTOs;
using HotelABC_API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelABC_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;
        private readonly IMapper mapper;

        public ImagesController(IImageRepository imageRepository, IMapper mapper)
        {
            this.imageRepository = imageRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var imagesDomain = await imageRepository.GetAllImages();
            return Ok(mapper.Map<List<ImageDto>>(imagesDomain));
        }

        [HttpDelete]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            var image = await imageRepository.DeleteImage(Id);
            if (image == null)
            {
                return NotFound();
            }
            return Ok(image);
        }
    }
}
