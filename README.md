Bu proje Çoklu Sunuculu Nesnelerin İnterneti Cihazları için Adli Bilişim ve Makine Öğrenmesi Teknikleri ile Suç Tespitinin Yapılmasının amaçlandığı bir çalışmadır.  
Proje kaynak kodlarının nasıl çalıştırılabileceğini hazırlanmış olan ASCIIDOC kullanılarak hazırlanmış olan README.pdf dosyasını indirerek öğrenebilirsiniz. Buna ek olarak geliştirilmiş yazılım mimarisi, öne sürülen model (IoT Cihaz Modeli, Desktp Uygulaması ve CNN modeli) ile ilgili detaylı bilgileri depo içerisinde bulunan Tez dökümanından edinebilirsiniz.

Yasin Emir Kutlu


//AscııDoc kodları

= ÇOKLU SUNUCULU NESNELERİN İNTERNETİ İÇİN DİJİTAL ADLİ BİLİŞİM VE MAKİNE ÖĞRENMESİ TEKNİKLERİ İLE SUÇ TESPİTİNİN YAPILMASI PROJESİ KURULUM KILAVUZU

* Bu döküman Çoklu Sunuculu Nesnelerin İnterneti Cihazları için Adli Bilişim ve Makine Öğrenmesi Teknikleri ile Suç Tespitinin Yapılmasının gerçekleştirildiği proje için kaynak kodlarının çalıştırılabilmesine yönelik hazırlanmıştır.

== Giriş

* Gerçekleştirilmiş olan proje temelde 3 ana kısımdan oluşmaktadır. Bu kısımlar sırasıyla bulut platformu, C# programlama dili kullanılarak geliştirilmiş masaüstü uygulaması ve son olarak derin öğrenme optimizasyonunu barındıran Evrişimsel Sinir Ağı modelidir.Bu parçaların sırasıyla görevleri aşağıdaki gibidir.

*Firebase Real Time Database :* Masaüstü uygulaması aracılığıyla eklenecek olan IoT cihazlarının kullanıcı bilgileri dahil olmak üzere verilerinin saklandığı veritabanıdır.

image::Firebase.png[900,900]


*Masaüstü Uygulaması :* IoT cihaz bilgilerinin doğrudan bulut platformdaki veritabanı ile etkileşimini sağlayan uygulamadır. Bu uygulamanın temelde belli başlı görevleri vardır. Bu görevler aşağıda belirtilmiştir.

. IoT cihaz sahibi kullanıcıların hesap açma ve login olma işlemleri.

. IoT cihaz bilgilerinin sunuculara kayıt edilmesi işlemleri.

. IoT cihazlarının soruşturmasını gerçekleştirecek resmi kurumların hesap açma ve login olma işlemleri.

. IoT cihazlarının harita üzerinde takip edilmesi.

. IoT cihazlarından elde edilmiş resim öğelerinin analiz sonuçlarının gösterilmesi.Masaüstü uygulamasının sağ tarafında bulunan textbox ve checkboxların yardımı ile bulut ortamına kayıtlı olan IoT cihazlarının taşıdığı resim öğelerinin analizi yapılabilmektedir.

şeklinde özetlenmektedir.

image::CNNSonucDesktop.png[900,900]

*Evrişimsel Sinir Ağı Modeli:* Sinir ağı modelinin kaba yapısı şekilde gösterilmiştir.

image::CNNModel.png[1000,1000]

Pyhton programlama dili kullanılarak geliştirilmiş olan CNN modeli ile IoT cihazlarından elde edilmiş resim verileri model tarafından kullanılarak, resimlerdeki öğeler için tahmin işleminin gerçekleştirildiği bölümdür. Bu bölümde CNN modeli temelde iki özelliği yerine getirmektedir.

. Resim Öğelerinden Özellik Çıkartımı
. Sınıflandırma

== Materyaller

* Bu bölümde projenin yazılımsal paydaşlarından olan Firebase veritabanı, C# kodunun çalıştırıldığı Visual Studio IDE'si ve CNN modelinin pyhton programlama dili kullanılarak geliştirildiği Jupyter Notebook geliştirme ortamının nasıl çalıştırılacağı anlatılmıştır.

=== Firebase Real Time Database

* Projenin temel taşlarından bir taneside verilerin saklandığı veritabanıdır. Gerçekleştirilen çalışmada önceden de belirtildiği gibi veritabanı olarak Google Firebase kullanılmıştır. Projenin geliştirilecek olan yeni versiyonları için farklı bir veritabanı modeli kullanılmak istenir veya varolan model degiştirilmek istenirse https://firebase.google.com/docs/database sitesinden gerekli değişiklikler yapılabilir.

* Öte yandan Firebase veritabanının API Keyleri masaüstü uygulaması için yazılmış kodların içine gömülmüştür. Herhangi bir değişikliğe ihtiyaç yoktur.

image::APIKey.png[900,900]

=== Visual Studio

* Projenin geliştirilmesi aşamasında hayata geçirilen öğelerden bir taneside masaüstü uygulamasıdır. Öncelikle, proje kaynak kodlarının derlenebilmesi için Visual Studio geliştirme ortamının bilgisayarınızda hazır olması gereklidir. Eğer bilgisayarınızda Visual Studio 2019 IDE'si yoksa https://visualstudio.microsoft.com/tr/downloads/ adresinden Visual Studio'yu indirmelisiniz.

image::VisualStudio.png[900,900]

* Visual Studio'yu bilgisayarınıza indirip kurulumunu yaptıktan sonra geliştirilmiş olan kod kullanıma hazırdır. GitHub deposu içerisinde bulunan *Clone* butonuna basıp, *Open in Visual Studio* butonu yardımıyla kod geliştirme ortamında çalıştırılabilir hale gelmektedir.


image::OpenStudio.png[900,900]

=== Jupyter Notebook

* Projenin 3. önemli ayağıda derin öğrenme tekniklerinin optimizasyonudur. Bu aşamada *Pyhton* programlama dili kullanılarak IoT cihazlarından elde edilen resimleri sınıflandırabilme kabiliyetine sahip bir CNN modeli tasarlanmıştır.

* Bu aşamada *Pyhton* tabanlı yazılmış kodların derlenmesi için *Jupyter Notebook* kullanılmıştır. Jupyter Notebook, çeşitli programlama dilleri için etkileşimli bir ortam sağlayan açık kaynak kodlu bir programdır. Fakat, geliştirilmiş olan projeyi derlemek istediklerinde zorunlu şekilde Jupyter notebook kullanmak zorunda değillerdir. Diğer pyhton IDE'leri de kaynak kodların çalıştırılmasında kullanılabilir. (Örn. PyCharm)

* Kodların derlenmesi işleminden önce **Keras ** isimli *sinir ağı kütüphanesinin* import edildiğinden emin olunuz.

==== Jupyter Notebook Kurulumu

* Jupyter notebok tek başına yüklenebilir bir platformdur ancak kullanıcılardan alınmış tavsiyeler üzerine Anaconda dağıtımını Pyhton 3.6 ile indirmek daha uygun olacaktır. Böylelikle daha zengin bir Pyhton geliştirme ortamı elde edilecektir. Anaconda dağıtımını indirmek için gerekli link https://www.anaconda.com/products/individual dir.

* Gerekli indirmeler ve kurulumlar tamamlandıktan sonra Jupyter notebook'u *CMD ekranında "Jupyter Notebook"* command ile açabilirsiniz.

image::CMDJupyter.png[900,900]

* Devamında açılan sayfada ise yazılmış olan Pyhton kodlarını derleyebilir veya üzerinde geliştirme yapabilirsiniz.

==== IoT Cihaz Kayıt Dosyaları

* Jupyter Notebook üzerindeki Pyhton dosyaları çalıştırıldıktan sonra CNN modeli ile sınıflandırılan resimlerin analiz sonuçları herbir cihaz için oluşturulmuş .txt uzantılı dosyalara yazılmaktadır. CNN modelinin sonuçlarının doğru bir şekilde masaüstü uygulamasına iletilebilmesi için IoT cihaz dosyalarının GitHub deposu üzerinde indirilip doğru dosya yolunun predict.ipynb dosyasına yazılması gerekmektedir.

* IoT cihazlarının analiz dosyaları GitHub deposu içerisinde IoTDevices klasörü içerisinde bulunmaktadır.

image::IoTAnalizDosyaları.png[900,900]


Proje kaynak kodları ve gerekli dökümanlar için Github linki : https://github.com/yasinemirkutlu44/Forensics-for-IoT-Multiple-Servers

*Yasin Emir Kutlu*

*150202040*



