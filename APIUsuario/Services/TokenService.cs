using APIUsuario.DTO.Model;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace APIUsuario.Services
{
    public static class TokenService
    {
        public static string GenerateToken(UsuarioDTO user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes("ROD2705OLI1983DA12345LUZ");
            var key = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor { 
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.email),
                    new Claim(ClaimTypes.Name, user.Nome),
                    new Claim(ClaimTypes.Role, user.email) //Como não criei Roles no banco, por enquanto vamos testar com o email
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
