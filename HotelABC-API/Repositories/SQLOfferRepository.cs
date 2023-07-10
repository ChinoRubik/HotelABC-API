using AutoMapper;
using HotelABC_API.Data;
using HotelABC_API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace HotelABC_API.Repositories
{
    public class SQLOfferRepository : IOffersRepository
    {
        private readonly HotelDbContext dbContext;

        public SQLOfferRepository(HotelDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Offer> CreateOffer(Offer offer)
        {
            await dbContext.Offers.AddAsync(offer);
            await dbContext.SaveChangesAsync();
            return offer;
        }

        public Task DeleteOffer(Offer offer)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Offer>> GetOffers()
        {
            var offerDomain = await dbContext.Offers.ToListAsync();
            return offerDomain;
        }

        public async Task<Offer> UpdateOfferRelation(Guid Id, Offer offer)
        {

            throw new NotImplementedException();
        }
    }
}
