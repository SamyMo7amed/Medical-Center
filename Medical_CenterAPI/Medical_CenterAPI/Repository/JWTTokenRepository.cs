using Medical_CenterAPI.Models;
using Medical_CenterAPI.UnitOfWork;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Medical_CenterAPI.Repository
{
    public class JWTTokenRepository : IJWTTokenRepository
    {

        private readonly IUnitOfWork _unitOfWork;   
        private readonly IConfiguration _configuration;
        public JWTTokenRepository(UnitOFWork unitOfWork,IConfiguration configuration) {
            this._unitOfWork = unitOfWork;
            this._configuration = configuration;    
                } 
        public async Task<List<Claim>> GetClaimsOFUserAsync(AppUser appUser)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, appUser.Name));   
            claims.Add(new Claim(ClaimTypes.NameIdentifier,appUser.Id.ToString()));
            IList<string> roles = await _unitOfWork.UserManager.GetRolesAsync(appUser);
            foreach (var role in roles) {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
           
        }

        public async Task<string> GetJWTTokenAsync(AppUser appUser)
        {
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires:DateTime.Now.AddDays(5),
                signingCredentials: await GetSigningCredentialsAsync(),
                claims: await GetClaimsOFUserAsync(appUser));
            string Token = new JwtSecurityTokenHandler().WriteToken(token);

            return Token;
        }


         

        public async Task<SigningCredentials> GetSigningCredentialsAsync()
        {

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            SigningCredentials signingCredentials=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            return  signingCredentials;  
            
        }
    }
}
