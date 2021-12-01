using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebApi.DbOperations;
using WebApi.Middlewares;
using WebApi.Services;

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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Token:Issuer"], // yayinlayici
                    ValidAudience = Configuration["Token:Audience"], // hedef kitle
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"])),
                    ClockSkew = TimeSpan.Zero // Token'i ureten sunucunun zamani ile kullanicilarin zamani farkli ise, bizde 0
                };
            });

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });

            // DbContext sinifi servis olarak uygulamaya tanitilir ve belirtilen ayarlar kullanilir
            services.AddDbContext<BookStoreDbContext>(options => options.UseInMemoryDatabase(databaseName: "BookStoreDB"));
            
            // Servis eklemeleri:
            //services.AddSingleton<interface,class>(); // Uygulamanin calismasindan durmasina kadar gecen surede yalnizca bir nesne uretir ve hep o nesne kullanilir
            //services.AddScoped<interface,class>(); // Bir Http Request boyunca yalnizca bir kez nesne olusturulur ve kullanilir, Response olustugu an omru biter. Her requestte yeni bir instance uretilir
            //services.AddTransient<interface,class>(); // Container tarafindan her seferinde yeniden olusturulur
            
            services.AddAutoMapper(Assembly.GetExecutingAssembly()); 
            // AutoMapper calisan assemblylerini yani configlerini alarak sisteme tanitir

            services.AddSingleton<ILoggerService, ConsoleLogger>();
            //services.AddSingleton<ILoggerService, DBLogger>();

            services.AddScoped<IBookStoreDbContext>(provider => provider.GetService<BookStoreDbContext>()); 
            // Her Request'e istinaden response donene kadar context nesnesi uretip, response ile sonlandirir
            // Yeni Request te tekrar bir context olusur
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

            // Kimlik dogrulamadan yetkilendirme mumkun degildir
            // Middleware sirasinin yanlis olmasi hataya sebep olacaktir
            app.UseAuthentication();

            app.UseAuthorization();

            // Request Endpoint'e dusmeden once
            app.UseCustomExceptionMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}