using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MiddlewarePracticeWebApi.Middlewares;
using System.Diagnostics;

namespace MiddlewarePracticeWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        { // Uygulama i�erisinde kullan�lacak bile�enlerin-servislerin(s�n�flar, k�t�phaneler, kod par�alar� vb.) eklendi�i, tan�t�ld��� ve ayarlar�n�n verildi�i metottur
            // Servis, belli bir i�i yapmaktan sorumlu s�n�flar, k�t�phaneler, kod par�alar� gibi...
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MiddlewarePracticeWebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline. (Pipeline: Arka arkaya �al��an i�lemler, s�ra �nemlidir)
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Use'lu her �ey bir Middleware'd�r, ara uygulamad�r, y�nlendirme, s�ra belirleyici kavramlard�r. Genel olarak Use ile ba�larlar, ama kural de�ildir.
            // Ortak dil a��s�ndan Use �nemlidir.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MiddlewarePracticeWebApi v1"));
            }

            app.UseHttpsRedirection(); // Http y�nlendirmelerini Https olarak yeniden y�nlendir

            app.UseRouting(); // Route y�nlendirmesini implemnte edecek

            app.UseAuthorization(); // Temel yetkilendirme ekle

            // app.Run() -- Run'dan sonraki ak�� �al��mayacakt�r, K�sa devre olu�turu, Tek Run �al��acakt�r.
            //app.Run(async context => Debug.WriteLine("Middleware 1 �al��t�, k�sa devre yaratt�!"));
            //app.Run(async context => Debug.WriteLine("Middleware 2 k�sa devre olu�tu�u i�in �al��mayacak!"));

            // app.Use() -- Kendi i�ini yapt�ktan sonra next.invoke ile di�er Middleware'a ge�er ve sonras�nda i�lem bitince devam� varsa i�inin bitirir.
            //app.Use(async (context, next) =>
            //    { // Asenkron, ba��ms�z �al��mas�, beklemez
            //        Debug.WriteLine("Middleware 1 - Use'un kendi i�lemi ba�lad�!");
            //        await next.Invoke(); // Burada bekle di�erinin �al��mas�n� bekliyor, onun d�n���n� bekliyor!
            //        Debug.WriteLine("Middleware 1 - Use'un kendi i�eminin kalan�n� bitiriyor!");
            //    });

            //app.Use(async (context, next) =>
            //{
            //    Debug.WriteLine("Middleware 2 - Use'un kendi i�lemi ba�lad�!");
            //    await next.Invoke(); // Burada bekle di�erinin �al��mas�n� bekliyor, onun d�n���n� bekliyor!
            //    Debug.WriteLine("Middleware 2 - Use'un kendi i�eminin kalan�n� bitiriyor!");
            //});

            //app.Use(async (context, next) =>
            //{
            //    Debug.WriteLine("Middleware 3 - Use'un kendi i�lemi ba�lad�!");
            //    await next.Invoke(); // Burada bekle di�erinin �al��mas�n� bekliyor, onun d�n���n� bekliyor!
            //    Debug.WriteLine("Middleware 3 - Use'un kendi i�eminin kalan�n� bitiriyor!");
            //});

            // Output:
            // Middleware 1 - Use'un kendi i�lemi ba�lad�!
            // Middleware 2 - Use'un kendi i�lemi ba�lad�!
            // Middleware 3 - Use'un kendi i�lemi ba�lad�!
            // Middleware 3 - Use'un kendi i�eminin kalan�n� bitiriyor!
            // Middleware 2 - Use'un kendi i�eminin kalan�n� bitiriyor!
            // Middleware 1 - Use'un kendi i�eminin kalan�n� bitiriyor!

            //app.Use(async (context, next) =>
            //{
            //    Debug.WriteLine("Use Middleware tetiklendi!");
            //    await next.Invoke(); // Burada bekle di�erinin �al��mas�n� bekliyor, onun d�n���n� bekliyor!
            //});

            // DemoMiddleware
            app.UseDemoMiddleware();

            // app.Map() -- Route'a g�re Middleware'leri y�netmeyi sa�lar.
            app.Map("/example", internalApp => // "/example" Route'una bir istek gelirse bu Middleware'i �al��t�r
                internalApp.Run(async context =>
                {
                    Debug.WriteLine("/example Route Middleware tetiklendi!"); // https://localhost:44307/example
                    await context.Response.WriteAsync("/example Route Middleware, Context i�erisindeki Response'a mesaj yaz�l�yor!");
                }));

            // app.MapWhen() -- Sadece Route'a g�re yani path'e g�re de�ilde Request i�erisindeki herhangibir parametreye g�re Middleware'leri y�netmeyi sa�lar.
            app.MapWhen(x => x.Request.Method == "GET", internalApp =>
            {
                internalApp.Run(async context =>
                {
                    Debug.WriteLine("MapWhen ile GET iste�i yap�lan Metotlar i�in Middleware tetiklendi!");
                    await context.Response.WriteAsync("MapWhen ile GET iste�i yap�lan Metotlar i�in Middleware tetiklendi ve Context Response'una mesaj yaz�l�yor");
                });
            });

            app.UseEndpoints(endpoints =>
            { // Endpointlere git, ilgili Request'e g�re, gelen path'e g�re Endpoint'i bul ve git
                endpoints.MapControllers();
            });
        }
    }
}