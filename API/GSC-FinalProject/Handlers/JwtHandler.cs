using GSC_FinalProject.Configuration;
using GSC_FinalProject.Dto;
using GSC_FinalProject.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GSC_FinalProject.Handlers
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtOptions _jwtOptions;

        public JwtHandler(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public string GenerateToken(UserDTO user, Role rol)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = GetClaims(user, rol);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return token;
        }

        public SigningCredentials GetSigningCredentials()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));

            return new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        }

        public List<Claim> GetClaims(UserDTO user, Role rol)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, rol.ToString())
            };
        }

        public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                claims: claims,
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                signingCredentials: signingCredentials);

            return tokenOptions;
        }
    }
}
