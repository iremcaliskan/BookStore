using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleWebApi.Controllers
{
    [ApiController] // Bu Attribute, Controller eylemlerinin bir Response döneceğini taahüt eder
    [Route("api/[controller]s")] // Controller'a tanımlanan Attribute, Web API'ye gelen istekleri yönlendirebiliriz
    // Sınıf bazında tanımlanır
    // Controller içerisinde bir resource ile ilgili eylemler bulunur, bu eylemlere URL içerisinde ortak bir grup ile ulaşırız
    // Url içerisinde çözümlenen Resource adına karşılık gelir
    // Bunu Route ile yaparız. API'ye gelen istekler hangi controller tarafından karşılanacağı Route niteliklerine göre belirlenir
    // Parametre ile bu endpointe bu controller'ın ismiyle erişilir
    // api/[controller]s ulaşılması domain name localhost:xxxx/api/weatherforecasts

    // Controller, benzer eylemleri tanımlamak ve gruplamak için kullanılır
    // Geriye HttpResponse dönen yapılardır = Controller
    // REST Servis Mimarisinde Resource'un karşılığıdır
    // Toplu yönetime olarak sağlar

    // Doğru Controller tasarımı, doğru gruplanmış eylemler, doğru Http Metotları ve Requestleri ile
    // Okunabilir, Genişletilebilir, Yönetilebilir API'lar tasarlanmış olur

    // KaynakController şeklinde isimlendirme kuralı bulunur
    // Default olarak AspNetCore.Mvc.ControllerBase namespace'inde bulunan ControllerBase sınıfından kalıtım-miras alırlar
    public class WeatherForecastController : ControllerBase
    { // Controller'ın Resource'u(kaynağı) WeatherForecast
        private static readonly string[] Summaries = new[]
        { // Veritabanı olmadığı için static readonly bir Array tanımlanmış
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        { // Constructure içerisinde inject ediliyor, dışarıdan bir log sınıfını enjekte ediyor
            // Dependency Injection
            _logger = logger;
        }

        // Controller benzer eylem gruplarını kapsayan sınıflardır
        // Action Metot, Controller'da bulunan Resource üzerine gerçekleştirilebilecek eylemlerdir
        // Resource üzerine yapılabilecek Http Get, Post, Put, Delete eylemlerinin yerine Action Metot denir
        // Normal metottan farkı Http request'leri karşılayıp, servis içerisinde gerekli işlemler tamamlandıktan sonra
        // Http response'ları geri döndüren metotlardır.
        [HttpGet] // Bu Attribute, Geriye liste/array dönen bir Get Metodu
        public IEnumerable<WeatherForecast> Get() // api/WeatherForecasts
        { // WeatherForecast türünde bir Array(IEnumerable) dönüyor
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast // 1 ile 5 arasında random sayı seçer, sayıya göre Array'den veri seçiyor
            {
                Date = DateTime.Now.AddDays(index), // 1-5 arasından gelecek sayı ile bugünün üzerine katarak tarih belirleme
                TemperatureC = rng.Next(-20, 55), // -20 ile 55 arasında random bir ifade
                Summary = Summaries[rng.Next(Summaries.Length)] // Array uzunluğu kadar max olabilecek şekilde bir sayı ve onu indexim yapayım
            })
            .ToArray(); // Array'e çevirme
        }
        // Eylemlere parametre geçmenin birden fazla yolu vardır.
        // En çok kullanılan 3 yöntem FromBody, FromQuery ve FromRoute Attribute'leri kullanılarak yapılanlardır
        // FromBody: Http request'inin body'si içerisinde gönderilen parametreleri okumak için kullanılır - Post ile daha çok
        // FromQuery: Url içerisine gömülen parametreleri okumak için kullanılan attribute dur - QueryString, url sonunda soru işareti sonrası parametreler
        // FromRoute: Endpoint url'i içerisinde gönderilen parametreleri okumak için kullanılır. Yaygın olarak resource'a ait id bilgisi okurken kullanılır
        // api/WeatherForecast/3 : 3 nolu WeatherForecast, Route'dan parametre okunur ve döndürülür

        [HttpGet("tempC/{tempC}")] // api/WeatherForecasts/tempC/{tempC} -- FromQuery
        // https://localhost:xxxxx/api/WeatherForecasts/tempC/0?tempC=0
        public ActionResult<WeatherForecast> GetByTempC([FromQuery] int tempC)
        {
            return new WeatherForecast
            {
                Date = DateTime.Now.AddDays(tempC),
                Summary = Summaries[2],
                TemperatureC = tempC
            };
        }

        [HttpGet("{id}")] // api/WeatherForecasts/{id} -- FromRoute
        // https://localhost:xxxxx/api/WeatherForecasts/1
        public ActionResult<WeatherForecast> GetById(string id)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray()[0];
        }
    }
}
// Aksiyon ifadelerinden kaçınılmalı : Aksiyon ifadelerini sizin yerinize http verb'leri zaten yapacaktır,
// bu nedenle isimlendirme yapılırken aksiyon ifadelerinden kaçınılmalıdır.
// Örnek: /Books/getBooks yerine zaten bu isteği HTTP GET ile yapacağımızdan yalnızca /Books/ olması daha doğru olacaktır.
// Bir endpointi okurken önünde hangi http verb kullanacaksak onunla birlikte okuyarak isimlendireceğimizi düşünmeliyiz.

// Controller yani resource isimlendirmesi çoğul olacak şekilde yapılmalı.
// Doğası gereği aslında bu kaynakların çoğul yani birden fazla olduğunu göz önünde bulundurmalıyız.
// Örnek: Book/ yerine Books/ kullanılmalı.

// GET/Books/getBook/1 X -- GET/Books/1
// GET/Book?name=netcore X -- GET/Books?name=netcore
// POST/Books/create X -- POST/Books
// DELETE/Books/deleteBook/1 X -- DELETE/Books/1
// PUT/Books/updateBook/11 X -- PUT/Books/11

// Postman:
// Postman, API geliştirme için bir işbirliği platformudur.
// Postman'ın özellikleri, bir API oluşturmanın her adımını basitleştirir ve işbirliğini kolaylaştırarak daha iyi API'leri daha hızlı oluşturabilmenizi sağlar.
// Temel özellikleri şu şekildedir:
// Api Client: Postman ile hızlı ve kolay bir şekilde REST ve SOAP istekleri oluşturabilirsiniz. Client(Browser) yerine kullanabilirsiniz.

// Automated Tests: Testler, tekrar tekrar çalışabilen test grupları oluşturularak otomatik hale getirilir.
// Postman; birim testleri, fonksiyonel testler, entegrasyon testleri, uçtan-uca testler, regresyon testleri vb.. dahil olmak üzere birçok test türünü otomatikleştirmek için kullanılabilir.
// Otomatik test, insan hatasını önler ve testi kolaylaştırır.
// CI/CD 'ye pipeline'nına bağlanarak süreçler otomatik hale gelir.
// Testler geçmezse test ortamına çıkmaz veya production'a gönderilmez, performans verim artışı sağlanır. İşler büyüdüğündeotomasyon kaçınılmaz hale gelir.

// Documentation: Postman, dökümanlarınınızı hızlı ve kolay bir şekilde yayınlamanıza olanak tanır.
// Postman, dokümantasyon sayfanızı dinamik örneklerle ve makine tarafından okunabilir talimatlarla doldurmak için örnek requestlerinizi otomatik olarak çeker ve anlamlandırır
// böylece API'nizi dünyanın geri kalanıyla kolayca paylaşabilirsiniz.