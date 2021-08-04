using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SampleWebApi
{
    public class Program
    { // Uygulama için gerekli ayarlarý içerir, Ýlk olarak Program.cs çalýþýr.
        // Ayný zamanda .Net5 bir console uygulamasýdýr. Console uygulamalarýnda ilk olarak Program.cs çalýþýr.
        // Main metodundan çalýþmaya baþlar C# uygulamalarýndaki gibi.
        // Uygulama içerisinde kullanýlacak ayarlar Startup.cs'de oluþturulup buraya bildirilmesi gerekir.
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run(); // CreateHostBuilder metodu Startup sýnýfýnýn config dosyasý olduðunu belirtir,
            // Host oluþturarak bir sunucuyu ayaða kaldýrýr, sunucuda ayarlarýný Startup sýnýfýnda alýr. 
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}