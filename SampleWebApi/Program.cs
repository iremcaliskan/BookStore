using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SampleWebApi
{
    public class Program
    { // Uygulama i�in gerekli ayarlar� i�erir, �lk olarak Program.cs �al���r.
        // Ayn� zamanda .Net5 bir console uygulamas�d�r. Console uygulamalar�nda ilk olarak Program.cs �al���r.
        // Main metodundan �al��maya ba�lar C# uygulamalar�ndaki gibi.
        // Uygulama i�erisinde kullan�lacak ayarlar Startup.cs'de olu�turulup buraya bildirilmesi gerekir.
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run(); // CreateHostBuilder metodu Startup s�n�f�n�n config dosyas� oldu�unu belirtir,
            // Host olu�turarak bir sunucuyu aya�a kald�r�r, sunucuda ayarlar�n� Startup s�n�f�nda al�r. 
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}