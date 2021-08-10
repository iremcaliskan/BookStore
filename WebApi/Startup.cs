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

            // DbContext sýnýfý servis olarak uygulamaya tanýtýlýr ve belirtilen ayarlar kullanýlýr.
            services.AddDbContext<BookStoreDbContext>(options => options.UseInMemoryDatabase(databaseName: "BookStoreDB"));
            // Servis eklemeleri:
            //services.AddSingleton<interface,class>(); // Uygulamanýn çalýþmasýndan durmasýna kadar geçen sürede yalnýzca bir nesne üretir ve hep o nesne kullanýlýr.
            //services.AddScoped<interface,class>(); // Bir Http Request boyunca yalnýzca bir kez nesne oluþturulup ve kullanýlýr, Response oluþtuðu an ömrü biter. Her requestte yeni bir instance üretilir.
            //services.AddTransient<interface,class>(); // Container tarafýndan her seferinde yeniden oluþturulur.
            services.AddAutoMapper(Assembly.GetExecutingAssembly()); // AutoMapper çalýþan assemblylerini yani configlerini alarak sisteme tanýtýlýr
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

            // Request Endpoint'e düþmeden önce
            app.UseCustomExceptionMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}