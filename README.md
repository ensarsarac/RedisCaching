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

İlk olarak Asp.Net WEB API proje oluşturuyoruz. Amacımız redis sunucusunu test etmek olduğu için basit bir yapı ile MSSQL server bağlıyorum ve birkaç test verisi gönderiyorum.

![image](https://github.com/user-attachments/assets/207f2b8b-95c7-4f81-a429-5b7fd853d9e4)

















