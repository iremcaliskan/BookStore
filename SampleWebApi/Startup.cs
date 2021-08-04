using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace SampleWebApi
{
    public class Startup
    { // Ayarlar�n yap�ld��� s�n�ft�r
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {// Uygulama i�erisinde kullan�lacak bile�enlerin-servislerin(s�n�flar, k�t�phaneler, kod par�alar� vb.) eklendi�i, tan�t�ld��� ve ayarlar�n�n verildi�i metottur
            // Servis, belli bir i�i yapmaktan sorumlu s�n�flar, k�t�phaneler, kod par�alar� gibi...
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SampleWebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline. (Pipeline: Arka arkaya �al��an i�lemler, s�ra �nemlidir)
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        { // Uygulamaya gelen HTTP isteklerinin hangi a�amalardan ge�erek Http cevaplar�n�n olu�turulaca��n�n belirtildi�i metottur
            if (env.IsDevelopment())
            { //  Environment - Ortam Development m�?
                app.UseDeveloperExceptionPage();
                app.UseSwagger(); // API Requestlerini dokumente etmek i�in, h�zl�ca tetikleyebilece�imiz bir aray�z sunan uygulama gibi
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SampleWebApi v1"));
                // Swagger UI, olu�turdu�umuz API'lar ile ilgili bilgileri g�rselle�tirmemiz ve otomatik d�k�mantasyon olu�turabilmemize yarayan yard�mc� bir aray�zd�r.
                // Bu aray�z sayesinde Web API'de hangi resource'lara sahip oldu�umuzu, resourcelarla ilgili hangi eylemleri yapabilece�imizle ilgili bir d�k�mantasyon olu�turmu� oluruz.
                // Bir .Net Core Web API projesi yaratt���m�zda proje i�erisine varsay�lan olarak Swagger UI eklentisi eklenmi� olarak gelir, e�er olu�turma an�nda i�aretlediysek, Open Api.
                // Swagger UI i�erisinde bir eylemle ilgili olarak temel iki �e�it bilgi bulunur.
                // Parameters: Http Put ve Http Post(genelde Body) �a�r�mlar�n�n bekledi�i parametreleri g�rd���m�z yerdir. FromQuery, FromRoute'dan spesifik Resource'a ait bilgi g�ndermek i�in...
                // Responses: Http talebine kar��l�k olarak nas�l bir HTTP response'u olu�turabilece�ini, response i�erisinde hangi tipte bir data d�nece�ini detayl� olarak g�rebiliriz.
                // Swagger UI arac�l��� ile eylemlerin bekledi�i parametreleri kolay bir �ekilde doldurarak �rnek �a�r�mlar yapabilir, d�nen cevaplar� g�zlemleyebiliriz.
                // Controller'lar�n Endpoint'lerini toplu bir yerden g�rmeyi(eylemler) ve h�zl�ca bu Endpoint'leri tetiklemeye olanak sa�lar.
            }// Production k�sm� i�in Swagger olmad��� i�in Development ortam� i�ersinde yer al�r

            app.UseHttpsRedirection(); // Http girildi�inde Https'e y�nlendirme sa�lar
            // Middleware, ara yaz�l�mlar, ara katman yaz�l�mlar� kullan�larak uygulama i�in pipeline olu�turulur.
            app.UseRouting(); // Y�nlendirmeler, Controllerlardan y�nlendirme

            app.UseAuthorization(); // Microsoft'un kendi Authorization'� kullan�l�yor

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Http Requestleri sunucaya geldi�i anda nas�l maplenecek detay�n�n verildi�i k�s�m
            });
        }
    }
}