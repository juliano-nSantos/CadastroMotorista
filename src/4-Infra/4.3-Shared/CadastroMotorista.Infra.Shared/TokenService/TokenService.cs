using CadastroMotorista.Infra.Shared.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CadastroMotorista.Infra.Shared.TokenService
{
    public class TokenService
    {
        public static Token GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("teste");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var getToken = tokenHandler.CreateToken(tokenDescriptor);

            Token token = new Token()
            {
                Created = DateTime.Now,
                Expiration = DateTime.Now.AddHours(24),
                AccessToken = tokenHandler.WriteToken(getToken)
            };

            return token;
        }
    }
}