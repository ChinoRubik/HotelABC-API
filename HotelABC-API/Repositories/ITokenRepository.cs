using Microsoft.AspNetCore.Identity;

namespace HotelABC_API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
