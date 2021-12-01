using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebApi.Entities;
using WebApi.TokenOperations.Models;

namespace WebApi.TokenOperations
{
    public class TokenHandler
    {
        public IConfiguration Configuration { get; set; }

        public TokenHandler(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Token CreateAccessToken(User user)
        {
            Token tokenModel = new();

            // Guvenlik anahtarinin simetrigi alinir
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));
            
            // Sifrelenmis kimlik oluturulur
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            // Token'in sona erme/gecerlilik suresi belirtilir
            tokenModel.Expiration = DateTime.Now.AddMinutes(15);

            // Token ozellikleri ile olusturulur
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: Configuration["Token:Issuer"],
                audience: Configuration["Token:Audience"],
                expires: tokenModel.Expiration,
                notBefore: DateTime.Now, // Token uretildigi andan itibaren ne zaman devreye girsin
                signingCredentials: signingCredentials);

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            // Token yaratilir
            tokenModel.AccessToken = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
            tokenModel.RefreshToken = CreateRefreshToken();

            return tokenModel;
        }

        public string CreateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}