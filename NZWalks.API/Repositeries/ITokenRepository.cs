using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repositeries
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
