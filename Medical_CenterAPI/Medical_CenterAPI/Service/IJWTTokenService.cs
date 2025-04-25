using Medical_CenterAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Medical_CenterAPI.Service
{
    public interface IJWTTokenService
    {

        Task<List<Claim>> GetClaimsOFUserAsync(AppUser appUser);


        Task<string> GetJWTTokenAsync(AppUser appUser);





        Task<SigningCredentials> GetSigningCredentialsAsync();
  


    }
}
