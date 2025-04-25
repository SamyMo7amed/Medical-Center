using Medical_CenterAPI.Models;
using Medical_CenterAPI.UnitOfWork;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Medical_CenterAPI.Service
{
    public class JWTTokenService : IJWTTokenService

    {
        private readonly IUnitOfWork _unitOfWork;


        public JWTTokenService(IUnitOfWork unitOfWork)
        {


            _unitOfWork = unitOfWork;
        }


        public async Task<List<Claim>> GetClaimsOFUserAsync(AppUser appUser)
        {
            return await _unitOfWork.JWTTokenRepository.GetClaimsOFUserAsync(appUser);

        }

        public async Task<string> GetJWTTokenAsync(AppUser appUser)
        {
            return await _unitOfWork.JWTTokenRepository.GetJWTTokenAsync(appUser);
        }




        public async Task<SigningCredentials> GetSigningCredentialsAsync()
        {


            return await _unitOfWork.JWTTokenRepository.GetSigningCredentialsAsync();



        }
    }
}
