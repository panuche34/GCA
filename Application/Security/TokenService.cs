using Core.Entity;
using Core.VO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Security
{
    public class TokenService
    {
        public static string TYPE_HEADER_AUTHORIZATION = "Authorization";
        public static string TYPE_BEARER = "Bearer ";
        public static string TYPE_JWT = "JWToken";
        public static string SECRET = "R1JJTExPIEFCUkFDQUYgQ09OVEFET1I=";
        public static string COOKIE_NAME = "ABRACAFCONTADORCAIXAS";

        public static TokenVO GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SECRET);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("userId", user.Id.ToString()),
            }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var vo = new TokenVO()
            {
                AccessToken = tokenHandler.WriteToken(token),
                TokenType = "Bearer",
                ExpiresIn = token.ValidTo.Ticks
            };
            return vo;
        }
    }
}
