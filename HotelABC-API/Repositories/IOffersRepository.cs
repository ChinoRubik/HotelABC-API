using HotelABC_API.Models.Domain;
using HotelABC_API.Models.DTOs;

namespace HotelABC_API.Repositories
{
    public interface IOffersRepository
    {
        Task<Offer> CreateOffer(Offer offer);

        Task<Offer?> UpdateOffer(Guid Id, Offer offer);

        Task<Offer?> DeleteOffer(Guid Id);

        Task<List<OfferDto>> GetOffers();
    }
}
