using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;
using WebApi.DbOperations;
using WebApi.Middlewares;

namespace WebApi
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
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });

            // DbContext s�n�f� servis olarak uygulamaya tan�t�l�r ve belirtilen ayarlar kullan�l�r.
            services.AddDbContext<BookStoreDbContext>(options => options.UseInMemoryDatabase(databaseName: "BookStoreDB"));
            // Servis eklemeleri:
            //services.AddSingleton<interface,class>(); // Uygulaman�n �al��mas�ndan durmas�na kadar ge�en s�rede yaln�zca bir nesne �retir ve hep o nesne kullan�l�r.
            //services.AddScoped<interface,class>(); // Bir Http Request boyunca yaln�zca bir kez nesne olu�turulup ve kullan�l�r, Response olu�tu�u an �mr� biter. Her requestte yeni bir instance �retilir.
            //services.AddTransient<interface,class>(); // Container taraf�ndan her seferinde yeniden olu�turulur.
            services.AddAutoMapper(Assembly.GetExecutingAssembly()); // AutoMapper �al��an assemblylerini yani configlerini alarak sisteme tan�t�l�r
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // Request Endpoint'e d��meden �nce
            app.UseCustomExceptionMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}