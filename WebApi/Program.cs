using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApi.DbOperations;

namespace WebApi
{
    public class Program
    {// Uygulama her çalýþtýðýnda ayný veritabanýnýn oluþur
        // Write the initial data to in memory db since the application is up
        public static void Main(string[] args)
        {
            // Uygulamanýn çalýþacaðý host alýnýr
            // Get the IWebHost which will host this application
            var host = CreateHostBuilder(args).Build();
            // Scope içerisine servis katmaný koyulur
            // Find the service layer within our scope
            using (var scope = host.Services.CreateScope())
            {
                // BookStoreDbContext sýnýfýnýn örneði servis katmaný içerisinden alýnýr
                // Get the instance of BookStoreDbContext in our services layer
                var serviceProvider = scope.ServiceProvider;
                // DataGenerator metodu çaðrýlarak örnek data oluþturulur
                // Call the DataGenerator to create sample data
                DataGenerator.Initialize(serviceProvider);
            }
            // Host çalýþtýrýlýr
            // Continue to run the application
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}