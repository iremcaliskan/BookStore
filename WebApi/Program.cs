using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApi.DbOperations;

namespace WebApi
{
    public class Program
    {// Uygulama her �al��t���nda ayn� veritaban�n�n olu�ur
        // Write the initial data to in memory db since the application is up
        public static void Main(string[] args)
        {
            // Uygulaman�n �al��aca�� host al�n�r
            // Get the IWebHost which will host this application
            var host = CreateHostBuilder(args).Build();
            // Scope i�erisine servis katman� koyulur
            // Find the service layer within our scope
            using (var scope = host.Services.CreateScope())
            {
                // BookStoreDbContext s�n�f�n�n �rne�i servis katman� i�erisinden al�n�r
                // Get the instance of BookStoreDbContext in our services layer
                var serviceProvider = scope.ServiceProvider;
                // DataGenerator metodu �a�r�larak �rnek data olu�turulur
                // Call the DataGenerator to create sample data
                DataGenerator.Initialize(serviceProvider);
            }
            // Host �al��t�r�l�r
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