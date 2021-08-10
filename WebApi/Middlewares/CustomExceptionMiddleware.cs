using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using WebApi.Services;

namespace WebApi.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;

        public CustomExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            try
            {
                string message = "[Request]   HTTP " + context.Request.Method + " - " + context.Request.Path;
                _loggerService.Write(message);
                //Debug.WriteLine(message);
                //[Request] HTTP GET - / swagger / swagger - ui - bundle.js.map
                //[Request] HTTP GET - / swagger / swagger - ui - standalone - preset.js.map

                //[Request] HTTP GET - / api / Books == Kitaplar Listelenmiş
                //[Request] HTTP POST - /api/Books == Kitap eklenmiş
                //[Request] HTTP GET - /api/Books/1 == 1 id'li kitap görüntülenmiş
                //[Request] HTTP DELETE - /api/Books/1  == 1 id'li kitap silinmiş
                //[Request] HTTP PUT - /api/Books/2 == 2 id'li kitaba güncelleme gelmiş

                await _next.Invoke(context);
                watch.Stop();

                message = "[Response]  HTTP " + context.Request.Method + " - " + context.Request.Path + " responded " + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + " ms";
                _loggerService.Write(message);
                //Debug.WriteLine(message);
                //[Request] HTTP GET - /swagger/swagger-ui-standalone-preset.js.map
                //[Request] HTTP GET - / swagger / swagger - ui - bundle.js.map
                //[Response] HTTP GET - / swagger / swagger - ui - bundle.js.map responded 404 in 29,1005 ms
                //[Response] HTTP GET - / swagger / swagger - ui - standalone - preset.js.map responded 404 in 4,6827 ms

                //[Request] HTTP GET - /api/Books == Kitap listeleme isteği gelmiş
                //[Response] HTTP GET - / api / Books responded 200 in 210,0343 ms == Kitap Listeleme isteğine 200(OK) ile cevap verilmiş 210 milisaniye içerisinde. ( 1000 msec = 1sec)

                //[Request] HTTP GET - /api/Books/1 == 1 Id'li kitabı görüntüleme isteği gelmiş
                //[Response] HTTP GET - / api / Books / 1 responded 200 in 46,0014 ms  == 1 Id'li kitabı görüntüleme isteğine 200(OK) ile cevap verilmiş 46 ms içerisinde gibi.

                //[Request] HTTP POST - / api / Books
                //[Response] HTTP POST - / api / Books responded 200 in 27,8816 ms

                //[Request] HTTP PUT - /api/Books/7
                //[Response] HTTP PUT - / api / Books / 7 responded 200 in 3,986 ms

                //[Request] HTTP DELETE - /api/Books/7
                //[Response] HTTP DELETE - / api / Books / 7 responded 200 in 11,6071 ms
            }
            catch (Exception ex)
            {
                watch.Stop(); // Invoke'ta hata olursa saat durmayacak, durduruldu
                await HandleException(context, ex, watch);
            }
        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = "[Error]     HTTP " + context.Request.Method + " - " + context.Request.Path + " responded " + context.Response.StatusCode
                + " with Error Message: " + ex.Message + " in " + watch.Elapsed.TotalMilliseconds + " ms.";
            //Debug.WriteLine(message);
            _loggerService.Write(message);

            var result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);
            return context.Response.WriteAsync(result);
            // Debug:
            //[Error]   HTTP POST - / api / Books responded 500 with Error Message: Validation failed: 
            // --Model.GenreId: 'Model Genre Id' değeri '0' değerinden büyük olmalı.Severity: Error
            //-- Model.PageCount: 'Model Page Count' değeri '0' değerinden büyük olmalı.Severity: Error
            //-- Model.PublishDate.Date: 'Model Publish Date Date', '10.08.2021 00:00:00' değerinden küçük olmalı.
            //Severity: Error in 5432,5455 ms.

            // Swagger:
            //{
            //  "error": "Validation failed: \r\n
            //  -- Model.GenreId: 'Model Genre Id' değeri '0' değerinden büyük olmalı. Severity: Error\r\n
            //  -- Model.PageCount: 'Model Page Count' değeri '0' değerinden büyük olmalı. Severity: Error\r\n
            //  -- Model.PublishDate.Date: 'Model Publish Date Date', '10.08.2021 00:00:00' değerinden küçük olmalı.
            //  Severity: Error"
            //}
        }
    }

    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}