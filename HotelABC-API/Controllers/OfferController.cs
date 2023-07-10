using AutoMapper;
using HotelABC_API.Libs;
using HotelABC_API.Models.Domain;
using HotelABC_API.Models.DTOs;
using HotelABC_API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HotelABC_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IOffersRepository offersRepository;
        private readonly IMapper mapper;
        private readonly IImageRepository imageRepository;
        private readonly ILogger<OfferController> logger;
        private readonly Utils utils;

        public OfferController(IOffersRepository offersRepository, IMapper mapper, IWebHostEnvironment webHostEnvironment,
            IImageRepository imageRepository, ILogger<OfferController> logger)
        {
            this.offersRepository = offersRepository;
            this.mapper = mapper;
            this.imageRepository = imageRepository;
            this.logger = logger;
            this.utils = new Utils(webHostEnvironment);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var offersDomain = await offersRepository.GetOffers();
            return Ok(mapper.Map<List<OfferDto>>(offersDomain));
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateOffer([FromForm] OfferCreateUpdateDto offerCreateUpdate)
        {
            //IFormFile fileForm = offerCreateUpdate.File;
            //logger.LogInformation("=====================================================================");
            //logger.LogInformation(JsonSerializer.Serialize(offerCreateUpdate));

            //utils.ValidateFileUpload(new List<IFormFile> { fileForm }, ModelState);
            //if (ModelState.IsValid)
            //{
            //    logger.LogInformation("===================================================================== entre aqui");
            //    var offerDomain = mapper.Map<Offer>(offerCreateUpdate);
            //    Offer offerSaved = await offersRepository.CreateOffer(offerDomain);
            //    logger.LogInformation(JsonSerializer.Serialize(offerSaved));

            //    var newOfferId = offerDomain.Id;
            //    var imageDomain = new Image
            //    {
            //        File = fileForm,
            //        // Offer Id
            //        RelativeRelationId = newOfferId,
            //        //Image Type Id
            //        ImageTypeId = Guid.Parse("8929b4bf-5be3-4002-8ad6-b9f46f782f16"),
            //    };
            //    var imageUploaded = await imageRepository.UploadImage(imageDomain);
            //    logger.LogInformation("===================================================================== uploading image");
            //    logger.LogInformation(JsonSerializer.Serialize(imageUploaded));

            //    await offersRepository.UpdateOfferRelation(offerDomain.Id, offerDomain);

            //    var offerDto = mapper.Map<OfferDto>(offerDomain);
            //    offerDto.image = mapper.Map<ImageDto>(imageUploaded);

            //    return Ok(offerDto);
            //}
            return BadRequest();
        }
    }
}
