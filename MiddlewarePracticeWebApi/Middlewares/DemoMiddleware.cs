using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MiddlewarePracticeWebApi.Middlewares
{
    public class DemoMiddleware
    {
        private readonly RequestDelegate _next;
        public DemoMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Debug.WriteLine("Demo Middleware");
            await _next.Invoke(context);
            Debug.WriteLine("Bye Middleware!");
        }
    }
    static public class DemoMiddlewareExtension
    { // app. ile çağrılabilen bir extension yazılması gereklidir, Application tarafından tetiklenebilir olması için extension static metot gereklidir
        public static IApplicationBuilder UseDemoMiddleware(this IApplicationBuilder builder) // IApplicationBuilder'ı genişletecek
        {
            return builder.UseMiddleware<DemoMiddleware>();
        }
    }
}