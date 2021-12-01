using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApi.DbOperations;

namespace WebApi
{
    public class Program
    {// Uygulama her calistiginda ayni veritabanini olusturur
        // Write the initial data to in memory db since the application is up
        public static void Main(string[] args)
        {
            // Uygulamanin calisacagi host alinir
            // Get the IWebHost which will host this application
            var host = CreateHostBuilder(args).Build();
            // Scope icerisine servis katmani koyulur
            // Find the service layer within our scope
            using (var scope = host.Services.CreateScope())
            {
                // BookStoreDbContext sinifinin orengi servis katmani icerisinden alinir
                // Get the instance of BookStoreDbContext in our services layer
                var serviceProvider = scope.ServiceProvider;
                // DataGenerator metodu cagrilarak ornek data olusturulur
                // Call the DataGenerator to create sample data
                DataGenerator.Initialize(serviceProvider);
            }
            // Host calistirilir
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