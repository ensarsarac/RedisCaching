<h3>Redis Caching</h3>
<hr>
<h5>Uygulamanın Amacı</h5>
<p>Redis ile önbellekleme kullanarak verilere daha hızlı erişim sağlamak ve Redis sunucularının yedeğini alarak olası kesintilerde sorunsuz bir şekilde çalışmayı sürdürmek.</p>
<h5>Kullanılan Teknolojiler</h5>
<p>Redis, Asp.Net Core, MSSQL, Caching, Replication, Sentinel, Docker, Redis Insight</p>
<br>
<hr>

<h5>Redis Caching Nedir ?</h5>

<p>Verilere daha hızlı erişim sağlamak amacıyla verilerin bellekte saklanmasına caching denir.</p>

<h5>Redis Caching Çeşitleri</h5>

<p><h6>In-Memory Caching: </h6>Uygulamanın çalıştığı bilgisayarın RAM'inde caching yapar.</p> 
<p><h6>Distributed Caching: </h6>Veriler birden fazla makinede önbelleğe alınarak daha güvenilir bir yöntem sağlanır.</p> 

<h5>Redis Caching Süreleri</h5>

<p><h6>Absolute Time: </h6>Verilerin bellekte ne kadar süreyle kalacağını belirler.</p> 
<p><h6>Sliding Time: </h6>Örneğin, bir veri için mutlak süre (absolute time) 30 saniye, kayma süresi (sliding time) ise 5 saniye olarak ayarlandığında, eğer veriye 5 saniye boyunca erişim olmazsa bellekten silinir; ancak sürekli erişim sağlanırsa 30 saniye sonra silinir.</p> 

<p>Proje içerisinde 2 türlü basit cache yöntemi mevcuttur.</p>
<hr>

<h5>Replication Nedir</h5>

<p>Sunucudaki verilerin güvenliği için bir kopyasını saklayarak veri kaybı gibi durumların önüne geçilir.</p>
<p>Master ve Slave sunucuları bulunur. Master sunuculardaki veriler Slave sunuculara aktarılır ve aralarında sürekli bir bağlantı sağlanır. Bağlantı kesildiğinde, otomatik olarak yeniden bağlantı kurulmaya çalışılır ve veri aktarımı devam eder. Bir Master sunucunun birden fazla Slave sunucusu olabilir ve bu Slave sunucular yalnızca salt okunur (read-only) modda tutulur.</p>
<hr>

<h5>Sentinel Nedir</h5>

<p>Redis sunucusu arızalandığında, Redis Sentinel servisi devreye girerek farklı bir sunucu üzerinden kesintisiz hizmet sağlanmasını mümkün kılar. Redis Sentinel, master-slave replikasyon sistemi üzerinde çalışır ve otomatik olarak master sunucunun sağlığını kontrol eder. Eğer master sunucuda bir sorun tespit edilirse, sağlıklı bir slave sunucuyu master olarak atar; böylece eski master sunucu slave, eski slave sunucu ise master olur. Sentinel tarafından gerçekleştirilen bu işleme "Failover" denir.</p>


<p>Tek bir Sentinel sunucusu, master sunucu arızalandığında hangi slave sunucunun yerine geçeceğine karar verir. Birden fazla Sentinel sunucusu ise aralarından bir lider seçerek, slave sunucusunu belirleme yetkisini lider Sentinel'e devreder.</p>

<h6>Neden Kullanılır ?</h6>
<ul>
  <li>Redis sunucusu arızalandığı zaman</li>
  <li>Redis sunucusu üzerinde bakım ve güncelleme yapıldığında</li>
  <li>Yüksek trafik olduğu zamanlarda</li>
  <li>Yedekleme olduğu süreçlerde </li>
</ul>

<hr>

<h5>RedisCaching.Example Projesi</h5>

İlk olarak bir ASP.NET Web API projesi oluşturuyoruz. Amacımız Redis sunucusunu test etmek olduğu için basit bir yapı ile MSSQL Server'a bağlanıyorum ve birkaç test verisi ekliyorum.

![image](https://github.com/user-attachments/assets/207f2b8b-95c7-4f81-a429-5b7fd853d9e4)

Sonrasında Microsoft.Extensions.Caching.StackExchangeRedis kütüphanesini yüklüyorum. Redis sunucusunu Docker üzerinden kuracağımız için ayrıca bir Docker ağı oluşturuyorum; böylece Sentinel uygulamasını da bu ağ üzerinde gerçekleştirebileceğim.

![image](https://github.com/user-attachments/assets/be2b4bab-5549-4234-b415-c72531f59a91)

Ardından Redis Sunucusunu oluştuyorum.

![image](https://github.com/user-attachments/assets/7b6954a6-ccd1-4d9c-a633-bf8c16b6431c)

Görüldüğü üzere docker üzerinde redis sunucumuzun kurulumunu gerçekleştirdik.

![image](https://github.com/user-attachments/assets/93afbbc8-4695-4e3b-a7c2-8aaee8f3e639)

Artık .Net Core tarafında bağlantı kurmalıyız. AppSetting.json üzerinde Redis değerlerini girdik.

![image](https://github.com/user-attachments/assets/1d754860-1394-4ce2-8eb9-83cec186842e)

Sonrasında program.cs içerisinde gerekli servisleri ekledik.

![image](https://github.com/user-attachments/assets/ab43dce1-ce10-45bd-9871-437fb2ccb1e3)

Not: Redis Cache crud servislerine uygulama içerisinden ulaşabilirsiniz.

API'ye istek yaptığımızda, ilk olarak Redis sunucusuna bakılıyor. Eğer veri orada yoksa, veri veritabanından alınıyor ve ardından Redis sunucusuna aktarılıyor.

![image](https://github.com/user-attachments/assets/1e2a0105-ca28-411e-b206-1e59951dd14e)

SQL'den çekilen süre ile Redis sunucusu üzerinden alınan süreler.

![image](https://github.com/user-attachments/assets/581a1099-bb3b-430b-8a23-f4897d59c1c8)

Cachleme işlemi başarılı bir şekilde çalıştı. Artık redis master sunucusunun yedeğini alalım. 2 adet slave sunucu oluşturacağım.
Dikakt edilmesi gereken yerler slave sunucuların portlarının değişmesidir.

![image](https://github.com/user-attachments/assets/5f292907-410d-4fc5-8422-b074612bf0c5)

2 tane slave sunucusu oluşturduk.

![image](https://github.com/user-attachments/assets/bf3db826-fe69-4dc7-975a-3c21a2128bf6)

Master sunucumuzun yedeğini almış olduk.

![image](https://github.com/user-attachments/assets/d69ab292-163c-488e-bc5b-cebe8e945fc2)

Şimdi, master sunucusunda herhangi bir sorun oluştuğunda kesinti olmaması için iki adet Redis Sentinel sunucusu kuracağız.

















