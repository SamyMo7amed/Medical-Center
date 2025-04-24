using Medical_CenterAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Medical_CenterAPI.Repository
{
    public interface IJWTTokenRepository
    {
        Task<string > GetJWTTokenAsync(AppUser appUser);

    }
}
