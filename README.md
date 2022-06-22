# SecureSoftwareDevelopment
 2020 SAÜ | Secure Software Development

https://www.youtube.com/watch?v=rOqFUdbTsbc

# RAPOR
“Virtual Library” uygulaması bir sanal kütüphane programıdır. Kütüphanedeki kitapların yazar, tür, sayfa sayısı gibi bilgilerini içermektedir. Ayrıca kitapların ödünç alınma geçmişini tutmaktadır. Kütüphane sadece öğrencilere hizmet verdiğinden kütüphane personeli ya da yöneticiler istekte bulunamaz.

- Uygulamada veritabanından doğrulama yapmak üzere giriş ekranı, yeni kullanıcı oluşturmak için ise kayıt ekranı bulunmaktadır. 
- Giriş ve kayıt olma ekranı dışındaki sayfalara yetkisiz erişim engellenmiştir.
- Uygulamada öğrenciler, kütüphane personeli ve yönetici rolleri mevcuttur. Öğrenciler; uygulama üzerinden kitap isteğinde bulunabilirler. Önceki taleplerini ve taleplerin durumunu görebilirler. Kütüphane personeli; gelen talepleri uygunluğuna göre onaylar ya da reddeder. Ayrıca kütüphanedeki kitapları düzenler ve yeni kitap ekleyebilir. Yönetici ise; kayıtlı kullanıcıların yetkilerini değiştirme hakkına sahiptir.
- Yetkisiz erişimleri denetlemek için oturum değişkenleri kullanılmıştır.
- Proje Web Servis ve Mobil Uygulama olmak üzere iki aşamadan oluşmaktadır. Uygulamalar .NET kullanılarak geliştirilmiştir. Web Servis için ASP.NET, Mobil Uygulama için ise Xamarin.Android tercih edilmiştir. Veriler SQLite veritabanında tutulmuştur. İlgili sayfalarda ekle/sil/güncelle işlemleri yapılmaktadır.
- Mobil uygulama ile sunucu arasındaki haberleşme asenkron olarak yapılmaktadır.
- Proje kodları https://github.com/ebayraktar/SecureSoftwareDevelopment adresine gönderilmiştir.
- Projenin güvensiz hali [OWASP TOP 10](https://owasp.org/www-project-top-ten/) listesinde bulunan `Injection`, `Sensitive Data Exposure` ve `Broken Access Control` başlıklı güvenlik risklerini içermektedir. 
- Güvenli hale getirilen projede uygulamaların bilinen hataları giderilmiştir.

# Gereksinimler
> Öğrencilerin sistem üzerinden kitap talebinde bulunması ve bu kitapların kütüphane personeli tarafından temin edilmesi amaçlanmıştır. Kitapların ad, yazar bilgisi, tür, puan, sayfa sayısı gibi bilgileri vardır.

> Öğrenci kitaplar sayfasına girdiğinde bütün kitapları görür,

> Kitaplar sayfasında bir kitap seçildiğinde o kitaba ait özellikler ve kitabın ödünç alma geçmişi listelenir,

> Öğrenci kitabı talep edebilir,

> Önceki taleplerinin durumunu görebilir,

> Talep edilen kitap kütüphane personelinin listesine düşer,

> Personel uygun görürse isteği onaylar, uygun görmediği taktirde reddeder,

> Personel kitap bilgilerini düzenler, kitap siler, kütüphaneye kitap ekler,

> Yönetici kayıtlı üyelerin bilgilerini görür, onların yetkilerini değiştirebilir.

# Giriş Noktaları
- Giriş ekranı
- Kayıt olma ekranı
- Kitap ekleme/düzenleme ekranı
- Kitap arama ekranı

# Varlıklar
1.	Öğrenciler, Kitaplar, Kitap geçmişleri,
2.	Kullanıcı bilgileri,
3.	Öğrenci bilgileri,
4.	Mobil uygulama,
5.	Web servis,
6.	Veritabanı sistemi,
7.	Öğrenci Veritabanı okuma erişimi,
8.	Kütüphane personeli/Admin veritabanı okuma/yazma erişimi,
9.	Kullanıcı oluşturma yeteneği,
10.	Kitap oluşturma yeteneği

# Güven seviyeleri 
|No|Adı|Açıklaması|
|--|---|------------|
|1.|Anonim Kullanıcı|Virtual Library uygulamasına bağlanan ancak daha oturum açmamış kullanıcıları tanımlar|
|2.|Geçersiz Kullanıcı|Geçerli olmayan giriş bilgileriyle oturum açmaya çalışan kullanıcıları tanımlar|
|3.|Öğrenci|Geçerli öğrenci bilgileriyle oturum açmış ve ana sayfaya yönlendirilmiş kullanıcıları kapsar|
|4.|Kütüphane Personeli|Geçerli kütüphane personeli bilgileriyle oturum açan ve yetkilendirilen personeldir|
|5.|Yönetici|Geçerli yönetici bilgileriyle oturum açan ve yetkilendirilen personeldir|

# Bağımlılıklar
Proje Web Servis ve Mobil Uygulama olmak üzere iki aşamadan oluşmaktadır. Uygulamalar .NET kullanılarak geliştirilmiştir. Web Servis için ASP.NET, Mobil Uygulama için ise Xamarin.Android tercih edilmiştir. Veriler SQLite veritabanında tutulmuştur. 


# İncelenen Güvenlik Zaafiyetleri

## 1. SQL Injection
SQL, NoSQL, OS ve LDAP enjeksiyonu gibi enjeksiyon hataları, güvenilmeyen veriler bir komutun veya sorgunun parçası olarak bir yorumlayıcıya gönderildiğinde ortaya çıkar. Saldırganın düşman verileri, yorumlayıcıyı istenmeyen komutlar yürütmeye veya doğru yetkilendirme olmadan verilere erişmeye yönlendirebilir.
#### A. Zaafiyet kullanımı
Uygulamanın giriş ekranındaki giriş noktalarından `“ ' OR 1=1; -- “` ifadesi gönderildiğinde yetkisiz girişe izin verecek şekilde güvenlik açığı vardı. Bu zaafiyetin sebebi web servis tarafında gönderilen kullanıcı bilgilerinin doğrudan sorguya alınmasıydı. 

`string query = $"SELECT * FROM USERS WHERE userName='" + loginModel.Username + "' AND password ='" + password + "';";`

`user = Constants.Connection.Query<Users>(query).FirstOrDefault();`

#### B. Giderilme Yöntemi
Web servisin bağlı olduğu veritabanı SQLite veritabanı olduğundan, veritabanı işlemlerini SQLite kütüphanesi ile yapabiliriz. Sorguyu elle yazmak yerine kullanıcı adı ve parola bilgisini tablo bilgisi ile kütüphaneye verdiğimizde ilgili tabloda aradığımız verinin olup olmadığını ve veriyi bize döndürmektedir. Kütüphane kullanımı bu zaafiyeti gidermeye yardımcı olmaktadır.

`user = Constants.Connection.Table<Users>().Where(x => 
 x.UserName.Equals(loginModel.Username) & 
 x.Password.Equals(password)).FirstOrDefault();`

## 2. Sensitive Data Exposure
Birçok web uygulaması ve API, finansal, sağlık ve kişisel bilgi gibi hassas verileri düzgün bir şekilde korumaz. Saldırganlar, kredi kartı sahtekarlığı, kimlik hırsızlığı veya diğer suçları yürütmek için zayıf korunan bu verileri çalabilir veya değiştirebilir. Hassas veriler, beklemede veya taşınırken şifreleme gibi ekstra koruma olmadan tehlikeye girebilir ve tarayıcıyla değiş tokuş edildiğinde özel önlemler gerektirir.

#### A. Zaafiyet Kullanımı
Web servis kayıt işlemlerinde kullanıcının bilgisini açık bir şekilde saklamaktaydı. Veritabanına erişimi olan kimse verileri çok rahat inceleyebilir ve kötüye kullanabilir.

#### B. Giderilme Yöntemi
Web serviste kayıt işlemleri yapılırken kullanıcının parolasını tek yönlü olarak şifreleyecek bir kütüphane kullanıldı. Login işlemlerinde kullanıcıdan alınan parola aynı anahtar ile tekrar şifrelendiğinde veritabanındaki şifreli değer ile eşleşiyorsa parola doğru kabul ediliyor. Burada parolayı çözüp kontrol etmek söz konusu değil. Dolayısıyla kullanıcının parolası resetlenmek istenirse şifre çözülemediğinden ya tek seferlik parola oluşturulur ya da kod gönderilebilir.

## 3. Broken Access Control
Kimliği doğrulanmış kullanıcıların ne yapmasına izin verildiğine ilişkin kısıtlamalar genellikle uygun şekilde uygulanmaz. Saldırganlar, diğer kullanıcıların hesaplarına erişme, hassas dosyaları görüntüleme, diğer kullanıcıların verilerini değiştirme, erişim haklarını değiştirme gibi yetkisiz işlevlere ve / veya verilere erişmek için bu kusurlardan yararlanabilir.

#### A. Zaafiyet Kullanımı
Uygulamaya giriş yapan her kullanıcı yönetici paneline erişebilmekteydi. Öğrenci olarak giriş yapmış bir kullanıcı bu panelden kolaylıkla kendi yetkilerini yükseltebilmekte, kitap ekleyip/silebilmekteydi.

#### B. Giderilme Yöntemi
Kullanıcıların giriş jeton bilgilerine yetkileri eklendi. Jeton bilgisine bakılarak kullanıcının girmeye yetkisi olmadığı sayfalara girişi engellendi ve kullanıcıya bilgi verildi.
