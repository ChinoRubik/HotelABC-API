using HotelABC_API.Models.Domain;

namespace HotelABC_API.Repositories
{
    public interface IOffersRepository
    {
        Task<Offer> CreateOffer(Offer offer);

        Task<Offer> UpdateOfferRelation(Guid Id, Offer offer);

        Task DeleteOffer(Offer offer);

        Task<List<Offer>> GetOffers();
    }
}
