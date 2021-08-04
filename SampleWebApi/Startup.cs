using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace SampleWebApi
{
    public class Startup
    { // Ayarlarýn yapýldýðý sýnýftýr
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {// Uygulama içerisinde kullanýlacak bileþenlerin-servislerin(sýnýflar, kütüphaneler, kod parçalarý vb.) eklendiði, tanýtýldýðý ve ayarlarýnýn verildiði metottur
            // Servis, belli bir iþi yapmaktan sorumlu sýnýflar, kütüphaneler, kod parçalarý gibi...
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SampleWebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline. (Pipeline: Arka arkaya çalýþan iþlemler, sýra önemlidir)
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        { // Uygulamaya gelen HTTP isteklerinin hangi aþamalardan geçerek Http cevaplarýnýn oluþturulacaðýnýn belirtildiði metottur
            if (env.IsDevelopment())
            { //  Environment - Ortam Development mý?
                app.UseDeveloperExceptionPage();
                app.UseSwagger(); // API Requestlerini dokumente etmek için, hýzlýca tetikleyebileceðimiz bir arayüz sunan uygulama gibi
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SampleWebApi v1"));
                // Swagger UI, oluþturduðumuz API'lar ile ilgili bilgileri görselleþtirmemiz ve otomatik dökümantasyon oluþturabilmemize yarayan yardýmcý bir arayüzdür.
                // Bu arayüz sayesinde Web API'de hangi resource'lara sahip olduðumuzu, resourcelarla ilgili hangi eylemleri yapabileceðimizle ilgili bir dökümantasyon oluþturmuþ oluruz.
                // Bir .Net Core Web API projesi yarattýðýmýzda proje içerisine varsayýlan olarak Swagger UI eklentisi eklenmiþ olarak gelir, eðer oluþturma anýnda iþaretlediysek, Open Api.
                // Swagger UI içerisinde bir eylemle ilgili olarak temel iki çeþit bilgi bulunur.
                // Parameters: Http Put ve Http Post(genelde Body) çaðrýmlarýnýn beklediði parametreleri gördüðümüz yerdir. FromQuery, FromRoute'dan spesifik Resource'a ait bilgi göndermek için...
                // Responses: Http talebine karþýlýk olarak nasýl bir HTTP response'u oluþturabileceðini, response içerisinde hangi tipte bir data döneceðini detaylý olarak görebiliriz.
                // Swagger UI aracýlýðý ile eylemlerin beklediði parametreleri kolay bir þekilde doldurarak örnek çaðrýmlar yapabilir, dönen cevaplarý gözlemleyebiliriz.
                // Controller'larýn Endpoint'lerini toplu bir yerden görmeyi(eylemler) ve hýzlýca bu Endpoint'leri tetiklemeye olanak saðlar.
            }// Production kýsmý için Swagger olmadýðý için Development ortamý içersinde yer alýr

            app.UseHttpsRedirection(); // Http girildiðinde Https'e yönlendirme saðlar
            // Middleware, ara yazýlýmlar, ara katman yazýlýmlarý kullanýlarak uygulama için pipeline oluþturulur.
            app.UseRouting(); // Yönlendirmeler, Controllerlardan yönlendirme

            app.UseAuthorization(); // Microsoft'un kendi Authorization'ý kullanýlýyor

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Http Requestleri sunucaya geldiði anda nasýl maplenecek detayýnýn verildiði kýsým
            });
        }
    }
}