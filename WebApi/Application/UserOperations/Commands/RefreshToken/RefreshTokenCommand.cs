using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using WebApi.DbOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.Commands.RefreshToken
{
    public class RefreshTokenCommand
    {
        public string RefreshToken { get; set; }

        private readonly IBookStoreDbContext _context;
        private readonly IConfiguration _configuration;

        public RefreshTokenCommand(IBookStoreDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public Token Handle()
        {
            var user = _context.Users.FirstOrDefault(x => x.RefreshToken == RefreshToken && x.RefreshTokenExpireDate > DateTime.Now);
            if (user is null)
            {
                throw new InvalidOperationException("Refresh token is not valid!");
            }

            TokenHandler handler = new(_configuration);
            var token = handler.CreateAccessToken(user);

            // Uzun soluklu kullanici islemlerinin suresi bitmeden yapilmasini saglar, surekli token'i yeniler
            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
            _context.SaveChanges();

            return token;
        }
    }
}