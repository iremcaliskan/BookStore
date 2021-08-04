using System.ComponentModel.DataAnnotations.Schema;

namespace EntityViewModelDtoPractices
{
    public class Student
    {
        // scalar properties : primitive tipleri içerir(Veri tiplerinin hepsi .Net'in sunduğu)
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string DateOfBirth { get; set; }
        public string Photo { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }

        // reference navigation property : primitive olmayan kendi ürettiğimiz type'lerı yani complex objeleri içerir
        public Grade Grade { get; set; } //  Grade Student için bir referans tablo, FK
        // EF Student tablosu içerisinde GradeId ismiyle bir FK tutarak bu iki tabloyu birbirine bağlar.
    }
}

// Db - Code köprüsü : ORM Aracı
// Entity Framework içindeki bir entity esasında veritabanı tablosuyla eşleşen bir sınıftır.
// Bu sınıf, DbContext sınıfına DbSet <TEntity> türü özelliği olarak dahil edilmelidir.
// EF her entity sınıfını bir tabloyla ve bir entity sınıfın her özelliğini veritabanındaki bir tablo kolonuna eşler.

// Bir entity sınıfında 2 tip property yani özellik bulunabilir. Bunlar:
// Scalar Property: Primitive type olan field'lar olarak düşünebilirsiniz. Db de data tutan kolonlara karşılık gelir.
// Students tablosundan yola çıkarsak her bir skalar property için tablo kolonları : StudentId, StudentName, DateOfBirth, Photo, Height, Weight.

// Navigation Property: Navigation Property bir entity ile başka bir entity arasında olan ilişkiyi temsil eder.
// Reference Navigation Property: Entity'nin başka bir entity'yi property olarak barındırması anlamına gelir.
// EntityFramework bu 2 tabloyu birbirine Foreign Key ile bağlar.

/* ViewModel
 * View model kullanıcıya gösterilecek olan verinin modelidir diyebiliriz.
 * Genelde UI bazında tanımlanır. Yani bir View'ın bir ViewModel'i olması beklenir. Birden fazla olmamalılıdır.
 * Bir ViewModel de birden fazla View'da kullanılmamalıdır. Bir işleve özel olmalıdır.
 * Çünkü ViewModel'lerin kullanılma motivativasyonu gereksiz bilginin client'a indirilmemesidir.
 * Yani bir view'da kullanılmayacak olan veri API'dan geri döndürülmemelir.
 * Uygulama içerisinde sadece adminin görmesi gereken bilgiyi yetkisi düz kullanıcının client'ına indirmek doğru değildir.
 * Bu veri güvenliği açığına neden olur!
 * Peki birden fazla view için aynı ViewModel'in kullanıldığını düşünelim. Bize ne dezavantaj sağlar?
 * View'lardan birinde extra bir alan gösterilmesi gerektiğinde bu bilgiyi ViewModel'e eklersek birden fazla view da bu veriyi 
 * döndürmüş oluruz. Aslında istemediğimiz bir veriyi kontrolsüzce client'a taşımamıza neden olur. Bu hassas bir veri de olabilir.
 * Üstelik işler çığırından çıktığında bunu tespit etmekte çok güçleşir.
 * O nedenle ViewModel'ler view bazında dikkatli şekilde kullanılmalıdır.
 * 
 * Disiplinli bir şekilde en küçük bir ekran için bile varolan ViewModel kullanılmadan yeni bir ViewModel yapılarak kullanılmalıdır.
 * Böyle yapılmazsa hem performans hemde güvenlik sorunu oluşturur. Koca bir objeyi dönmenin gereği yoktur.
 * 
 * Bir diğer kritik nokta ise entity'e veri map'lemek(post, put) için kullanılan model ile UI'a veri dönmek için kullanılan modelin 
 * aynı olmasıdır. Bu kullanımda fazladan verinin expose edilmesine yani client'a indirilmesine neden olur.
 * Bu yaklaşımda veri güvenliği sorunlarına neden olacağı için kaçınılmalıdır.
 * 
 * ViewModelleri son kullanıcıya veri dönmek dışında kullanmamamız gerekir. Her zaman tek bir amaç için tek bir View için kullanılmalıdır.
 * Son kullanıcı request'ine döndürülecek verileri maplemek için kullanılmalıdır.
 */

/* DTO (Data Transfer Object)
 * DTO yani Data Transfer Object. View model(Client'a) ve DTO(Server İletişimi için) kullanımı çok karıştırılan 2 kavramdır.
 * View model son kullanıcıya gösterilecek veriyi döndürmek için kullanılırken,
 * Dto uygulama katmanları arasında veriyi transfer etmek için kullanılır.
 * 
 * Genel olarak database den gelen veriyi source olarak kullanır.
 * DTO da asıl amaç katmanları arasındaki yapılabilecek call - çağrım sayılarını azaltmaktır. Bir katmanda elimizde var olan data 
 * diğer katmanda kullanılacak ise, veriyi tasımak diğer katmanda yeniden çağrım yapmamak için anlamlı bir çözümdür.
 * Herbir çağrım dış kaynak kullanımına, maaliyete sebep olur. Db'de olsa, Third parti uygulama olsa dahi.
 * 
 * Dto'lar nerdeyse hiç davranış içermezler.(Business Logic içermezler) Veriyi olduğu gibi ileten Dumb objelerdir.
 * 
 * ViewModeller ise davranış içerir. Formatlamalar yapılır.
 * 
 * Code tekrarından kaçmak için aynı modeli kullanmamalıyız. Dependency Injection daha büyük bir sorun oluşturur.
 */