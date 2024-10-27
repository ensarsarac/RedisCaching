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
