using System;

namespace WebApi.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpireDate { get; set; }
        // Access Token'in suresi doldugunda kullaniciyi logout etmek istemedigimiz icin kullandigimiz yapidir
        // Refresh Token ile süre bittikce Access Token istenir
    }
}