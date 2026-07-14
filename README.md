# 🎟️ TicketBox - Modern Etkinlik Yönetim Sistemi

**TicketBox**, etkinlik yönetimi, biletleme süreçleri ve kullanıcı etkileşimlerini uçtan uca yönetmek için geliştirilmiş, **Clean Architecture** prensipleriyle kurgulanmış profesyonel bir etkinlik platformudur. Sistem, gelişmiş veri analitiği ve yapay zeka destekli yönetim paneli ile veriye dayalı kararlar almanızı sağlar.

## 🚀 Proje Mimarisi & Teknoloji Yığını

Proje, modülerlik, ölçeklenebilirlik ve sürdürülebilirlik odaklı aşağıdaki modern teknolojilerle inşa edilmiştir:

*   **Backend:** .NET 8.0, ASP.NET Core
*   **Architecture:** Clean Architecture, N-Tier, CQRS (MediatR), Specification Pattern
*   **Database:** SQL Server, Entity Framework Core
*   **AI Integration:** 
    *   **Tavily AI:** Satış verileri analitiği ve stratejik raporlama.
    *   **Gemini AI:** Kullanıcı ruh haline (mood) göre dinamik etkinlik önerileri.
*   **Frontend:** ASP.NET Core MVC, Stitch AI Teması
*   **Validation:** FluentValidation

### 📁 Katmanlı Mimari Yapısı
*   **`TicketBox.Domain`**: Temel entity'ler ve Specification arayüzleri.
*   **`TicketBox.Application`**: Business logic, CQRS handler'lar ve validasyonlar.
*   **`TicketBox.Persistance`**: Veritabanı context'i, migration'lar ve dış servis (AI/Email) implementasyonları.
*   **`TicketBox.WebUI`**: Admin/Kullanıcı arayüzleri, Controller'lar ve View Model'lar.
## 💡 Öne Çıkan Özellikler

### 🤖 Tavily AI Veri Analitiği
Yönetici paneline entegre edilen yapay zeka modülü, satış verilerinizi anlık olarak analiz eder.

*   **Akıllı Sorgulama:** Satış performansı hakkında doğal dilde sorular sorun.
*   **Hiyerarşik Raporlama:** Verileri yüksekten düşüğe profesyonel bir dille raporlar.
*   **Veri Odaklılık:** Tavily AI gücüyle, karmaşık analiz raporlarına ihtiyaç duymadan hızlı içgörüler elde edin.

### 🎭 Gemini AI ile Kişiselleştirilmiş Etkinlik Önerileri
Sistem, kullanıcının o anki ruh halini (mood) analiz ederek, etkinlik veritabanından en uygun kategoriyi önerir.

*   **Duygu Analizi:** Kullanıcının 'enerjik', 'stresli' veya 'yorgun' olma durumuna göre akıllı öneriler.
*   **Kategori Bazlı Dinamik Eşleşme:** Gemini API ile entegre çalışarak, veritabanındaki kategorilerle %100 uyumlu (Birebir eşleşme) yanıtlar üretir.
*   **Samimi Etkileşim:** Kullanıcıya ruh haline uygun, emoji destekli ve teşvik edici geri bildirimler sunar.

## 💡 Proje Özellikleri ve Sayfaları

### 🌐 Kullanıcı Deneyimi ve Etkinlik Yönetimi
*   **Dinamik Etkinlik Keşfi:** Ana sayfa üzerinden popüler etkinliklere hızlı erişim ve kategori bazlı filtreleme.
*   **Gelişmiş Filtreleme:** Kategori, fiyat aralığı ve isim bazlı arama ile etkinliklere saniyeler içinde ulaşım.
*   **Gerçek Zamanlı Kontenjan Takibi:** Etkinlik detaylarında anlık satışa göre güncellenen "Kalan Koltuk" bilgisi ile şeffaf satın alma.
*   **Güvenli Satın Alma:** Üyelik sistemi zorunluluğu ile bilet alım süreçlerinde maksimum güvenlik.

### 👤 Kullanıcı Yönetimi ve Biletleme Süreçleri
*   **Güvenli Kimlik Doğrulama:** Modern Üye Ol / Giriş Yap panelleri.
*   **Kapsamlı Bilet Yönetimi:** "Biletlerim" paneli üzerinden tüm etkinliklerin tek bir yerden yönetimi.
    *   **Aktif Biletler:** Güncel biletlerin listelenmesi ve hızlı iptal seçeneği.
    *   **Geçmiş & İade Süreçleri:** İptal edilen biletlerin "Geçmiş & İptal Edilenler" sekmesinde şeffaf arşivlenmesi.
*   **Dijital Biletleme:** PNR kodlu ve QR destekli biletlerin anında görüntülenmesi ve otomatik e-posta bildirimi.


### 📊 Yönetici Paneli ve İleri Seviye Analitik
*   **KPI Paneli:** "Toplam Brüt Satış", "Aktif Etkinlikler" ve "Yeni Kullanıcılar" gibi temel performans göstergeleri.
*   **İşlem Takibi:** Sistemdeki son 5 işlem kaydını; kullanıcı bilgisi, etkinlik ID'si ve işlem durumu ile şeffaf görüntüleme.

### 🤖 Tavily AI ile Akıllı Satış Analitiği
*   **Akıllı Sorgulama:** Satış performansı hakkında doğal dilde sorular sorun.
*   **Stratejik Raporlama:** Verileri yüksekten düşüğe profesyonel bir dille raporlar.
*   **Veri Odaklı Kararlar:** Geliri maksimize edecek kategorileri (örn: "Workshop'lara odaklanın") hızlı içgörülerle belirleyin.

### ⚙️ Yönetici Etkinlik ve Kategori Yönetimi
*   **Etkinlik Yönetimi:** Dinamik izleme, gelişmiş arama, tam kapsamlı CRUD işlemleri (Ekleme/Düzenleme/Pasifize etme).
*   **Kategori Yönetimi:** Görsel kategori paneli, etkinlik sayısına göre ilişkisel filtreleme ve kolay düzenleme.

### 🎟️ Bilet Operasyon Merkezi
*   **Dinamik Kapasite:** Etkinlik bazlı kapasite artırma (bilet ekleme).
*   **Satılan Bilet Detayları:** Kullanıcı bilgileri, PNR, bilet kodu ve tutar bazlı detaylı tablo gösterimi.

### 👥 Kullanıcı Yönetimi
*   **Merkezi Denetim:** Sisteme kayıtlı kullanıcıların isim, e-posta ve etkinlik geçmişi (toplam bilet alım sayısı) ile yönetimi.


<img width="1374" height="3034" alt="localhost_7080_" src="https://github.com/user-attachments/assets/5566a9bc-862e-43f5-b5b8-ee65f1b947ce" />
<img width="1374" height="1824" alt="localhost_7080_Event_EventList (1)" src="https://github.com/user-attachments/assets/53a06ac3-0f79-4893-a603-d1ae8ab07ae3" />
<img width="1374" height="1398" alt="localhost_7080_Event_EventList_search= categoryId=9 maxPrice=360" src="https://github.com/user-attachments/assets/aa5b9bdd-8674-41bb-b13c-c34ded1ea43e" />
<img width="1374" height="2107" alt="localhost_7080_Event_EventDetail_18" src="https://github.com/user-attachments/assets/7680e49f-d995-4f89-86ca-7206b9921f67" />
<img width="458" height="671" alt="Ekran görüntüsü 2026-07-13 200021" src="https://github.com/user-attachments/assets/09e50177-d313-44ac-99d8-acd0e25625cc" />
<img width="819" height="864" alt="Ekran görüntüsü 2026-07-13 114808" src="https://github.com/user-attachments/assets/b03b6a86-19f5-4ba7-8458-d8b8bd40e9b5" />
ü<img width="1389" height="911" alt="localhost_7080_Auth_SignIn" src="https://github.com/user-attachments/assets/3e9c9917-a3e0-46d3-b03c-7b9327351494" />
<img width="1389" height="911" alt="localhost_7080_Auth_SignUp" src="https://github.com/user-attachments/assets/ade0eb6a-643e-44d3-8262-540e462c3502" />
<img width="1374" height="1060" alt="localhost_7080_Event_Confirmation_bookingId=28" src="https://github.com/user-attachments/assets/93f452f2-b243-460f-8c01-080c31490455" />
<img width="1229" height="559" alt="Ekran görüntüsü 2026-07-13 201939" src="https://github.com/user-attachments/assets/7e22d9ee-1003-4e5c-9103-428feda47aee" />
<img width="1374" height="4030" alt="localhost_7080_User_UserDashboard_MyTickets" src="https://github.com/user-attachments/assets/2131a486-7cce-4d60-a9a8-f8c1d1eef61f" />
<img width="1374" height="1013" alt="localhost_7080_Admin" src="https://github.com/user-attachments/assets/a83e9db0-edd7-4d47-927a-79ada619beca" />
<img width="1489" height="1231" alt="localhost_7080_Admin_Events" src="https://github.com/user-attachments/assets/3f118ea5-37db-439f-88af-89d82c011db7" />
<img width="716" height="808" alt="Ekran görüntüsü 2026-07-13 200312" src="https://github.com/user-attachments/assets/67026415-9354-44ea-a2da-86c3c429d6b4" />
<img width="686" height="796" alt="Ekran görüntüsü 2026-07-13 200321" src="https://github.com/user-attachments/assets/a1d674c3-fed5-4644-8bb6-cb109f6b74ac" />
<img width="1374" height="916" alt="localhost_7080_Admin_Categories" src="https://github.com/user-attachments/assets/50c677b4-1ca0-44de-b0e5-f0644545ea10" />
<img width="455" height="467" alt="Ekran görüntüsü 2026-07-13 200336" src="https://github.com/user-attachments/assets/12232142-eee9-4047-ab4b-52c98ea9f537" />
<img width="1374" height="1648" alt="localhost_7080_Admin_Tickets" src="https://github.com/user-attachments/assets/bb8f41fc-d11a-41f1-b54b-5bb23e1d3f70" />
<img width="1389" height="911" alt="localhost_7080_Admin_Tickets_SoldTickets_eventId=40" src="https://github.com/user-attachments/assets/6ee2ab8d-1a0e-4064-8c66-8e2e8a60cf81" />
<img width="1389" height="911" alt="localhost_7080_Admin_Users" src="https://github.com/user-attachments/assets/7ba2bfe9-212e-4b61-b4ca-b3e21f3540b1" />


