using APIUsuario.DTO.Model;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
//using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IO;

namespace APIUsuario.Services
{
    public static class TokenService
    {
        private static IConfigurationRoot _configuration;

        private static IConfigurationRoot Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    _configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .Build();
                }
                return _configuration;
            }
        }


        public static string GenerateToken(UsuarioDTO user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var key = Encoding.ASCII.GetBytes(Configuration["Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor { 
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.email),
                    new Claim(ClaimTypes.Name, user.Nome),
                    new Claim(ClaimTypes.Role, user.email) //Como não criei Grupo de Roles no banco, por enquanto vamos testar com o email
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
