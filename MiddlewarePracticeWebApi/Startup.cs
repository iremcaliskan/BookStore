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
        { // Uygulama içerisinde kullanýlacak bileþenlerin-servislerin(sýnýflar, kütüphaneler, kod parçalarý vb.) eklendiði, tanýtýldýðý ve ayarlarýnýn verildiði metottur
            // Servis, belli bir iþi yapmaktan sorumlu sýnýflar, kütüphaneler, kod parçalarý gibi...
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MiddlewarePracticeWebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline. (Pipeline: Arka arkaya çalýþan iþlemler, sýra önemlidir)
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Use'lu her þey bir Middleware'dýr, ara uygulamadýr, yönlendirme, sýra belirleyici kavramlardýr. Genel olarak Use ile baþlarlar, ama kural deðildir.
            // Ortak dil açýsýndan Use önemlidir.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MiddlewarePracticeWebApi v1"));
            }

            app.UseHttpsRedirection(); // Http yönlendirmelerini Https olarak yeniden yönlendir

            app.UseRouting(); // Route yönlendirmesini implemnte edecek

            app.UseAuthorization(); // Temel yetkilendirme ekle

            // app.Run() -- Run'dan sonraki akýþ çalýþmayacaktýr, Kýsa devre oluþturu, Tek Run çalýþacaktýr.
            //app.Run(async context => Debug.WriteLine("Middleware 1 çalýþtý, kýsa devre yarattý!"));
            //app.Run(async context => Debug.WriteLine("Middleware 2 kýsa devre oluþtuðu için çalýþmayacak!"));

            // app.Use() -- Kendi iþini yaptýktan sonra next.invoke ile diðer Middleware'a geçer ve sonrasýnda iþlem bitince devamý varsa iþinin bitirir.
            //app.Use(async (context, next) =>
            //    { // Asenkron, baðýmsýz çalýþmasý, beklemez
            //        Debug.WriteLine("Middleware 1 - Use'un kendi iþlemi baþladý!");
            //        await next.Invoke(); // Burada bekle diðerinin çalýþmasýný bekliyor, onun dönüþünü bekliyor!
            //        Debug.WriteLine("Middleware 1 - Use'un kendi iþeminin kalanýný bitiriyor!");
            //    });

            //app.Use(async (context, next) =>
            //{
            //    Debug.WriteLine("Middleware 2 - Use'un kendi iþlemi baþladý!");
            //    await next.Invoke(); // Burada bekle diðerinin çalýþmasýný bekliyor, onun dönüþünü bekliyor!
            //    Debug.WriteLine("Middleware 2 - Use'un kendi iþeminin kalanýný bitiriyor!");
            //});

            //app.Use(async (context, next) =>
            //{
            //    Debug.WriteLine("Middleware 3 - Use'un kendi iþlemi baþladý!");
            //    await next.Invoke(); // Burada bekle diðerinin çalýþmasýný bekliyor, onun dönüþünü bekliyor!
            //    Debug.WriteLine("Middleware 3 - Use'un kendi iþeminin kalanýný bitiriyor!");
            //});

            // Output:
            // Middleware 1 - Use'un kendi iþlemi baþladý!
            // Middleware 2 - Use'un kendi iþlemi baþladý!
            // Middleware 3 - Use'un kendi iþlemi baþladý!
            // Middleware 3 - Use'un kendi iþeminin kalanýný bitiriyor!
            // Middleware 2 - Use'un kendi iþeminin kalanýný bitiriyor!
            // Middleware 1 - Use'un kendi iþeminin kalanýný bitiriyor!

            //app.Use(async (context, next) =>
            //{
            //    Debug.WriteLine("Use Middleware tetiklendi!");
            //    await next.Invoke(); // Burada bekle diðerinin çalýþmasýný bekliyor, onun dönüþünü bekliyor!
            //});

            // DemoMiddleware
            app.UseDemoMiddleware();

            // app.Map() -- Route'a göre Middleware'leri yönetmeyi saðlar.
            app.Map("/example", internalApp => // "/example" Route'una bir istek gelirse bu Middleware'i çalýþtýr
                internalApp.Run(async context =>
                {
                    Debug.WriteLine("/example Route Middleware tetiklendi!"); // https://localhost:44307/example
                    await context.Response.WriteAsync("/example Route Middleware, Context içerisindeki Response'a mesaj yazýlýyor!");
                }));

            // app.MapWhen() -- Sadece Route'a göre yani path'e göre deðilde Request içerisindeki herhangibir parametreye göre Middleware'leri yönetmeyi saðlar.
            app.MapWhen(x => x.Request.Method == "GET", internalApp =>
            {
                internalApp.Run(async context =>
                {
                    Debug.WriteLine("MapWhen ile GET isteði yapýlan Metotlar için Middleware tetiklendi!");
                    await context.Response.WriteAsync("MapWhen ile GET isteði yapýlan Metotlar için Middleware tetiklendi ve Context Response'una mesaj yazýlýyor");
                });
            });

            app.UseEndpoints(endpoints =>
            { // Endpointlere git, ilgili Request'e göre, gelen path'e göre Endpoint'i bul ve git
                endpoints.MapControllers();
            });
        }
    }
}