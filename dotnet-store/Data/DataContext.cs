using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dotnet_store.Models;

public class DataContext : IdentityDbContext<AppUser, AppRole, int>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
    public DbSet<Urun> Urunler { get; set; }
    public DbSet<Kategori> Kategori { get; set; }
    public DbSet<Slider> Sliderlar { get; set; }
    public DbSet<Cart> Carts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Slider>().HasData(
    new List<Slider> {
        new Slider { Id=1, Baslik="Slider 1 Başlık", Aciklama="Slider 1 Açıklama", Resim="slider3.jpg", Aktif=true, Index=0},
        new Slider { Id=2, Baslik="Slider 2 Başlık", Aciklama="Slider 2 Açıklama", Resim="slider7.jpg", Aktif=true, Index=1},
        new Slider { Id=3, Baslik="Slider 3 Başlık", Aciklama="Slider 3 Açıklama", Resim="slider8.png", Aktif=true, Index=2}
    }
);

        modelBuilder.Entity<Kategori>().HasData(
            new List<Kategori> {
                new Kategori {Id=1, KategoriAdi="Konferans", Url="konferans"},
                new Kategori {Id=2, KategoriAdi="Spor", Url="spor"},
                new Kategori {Id=3, KategoriAdi="Muzik", Url="muzik"},
            }
        );

        modelBuilder.Entity<Urun>().HasData(
            new List<Urun>()
            {
                new Urun() {
                    Id = 1,
                    UrunAdi="Global HR Summit 2025",
                    Fiyat=5000,
                    Aktif=true,
                    Resim="konferans1.jpg",
                    Anasayfa=true,
                    Aciklama="27-28 Mayıs 2025 tarihlerinde “The Age of AI : Technology and Human” temasıyla Mandarin Oriental Bosphorus Istanbul’da fiziki olarak gerçekleştirilecektir. Ayrıca, İstanbul dışı farklı şehirlerden ve yurtdışı katılımcılarımız için online olarak da muhteşem içeriklerimizi yine izleyenlerle paylaşıyor olacağız.",
                    KategoriId=1

                },
                new Urun() {
                    Id = 2,
                    UrunAdi="Darüşşafaka Lassa 2024-2025 Sezonu Kombinesi",
                    Fiyat=3000,
                    Aktif=true,
                    Resim="spor1.jpg",
                    Anasayfa=true,
                    Aciklama="Etkinlik Kuralları – Müsabakalara sadece ev sahibi takımın seyircileri alınacak ve misafir takımın seyircilerinin salona girişlerine izin verilmeyecektir. – Etkinlik alanına giriş yapan seyircilerin alandan çıkış yapmaları halinde yeni bilet satın almaları gerekmektedir. – Etkinlik alanı içinde yiyecek ve içecek satışı yapılacağı için, dışarıdan getirilen yiyecek ve içecek Etkinlik alanına alınmayacaktır.",
                    KategoriId=2
                },
                new Urun() {
                    Id = 3,
                    UrunAdi="Dedublüman, 16 Mayıs akşamı Jolly Joker Ankara sahnesinde.",
                    Fiyat=3500,
                    Aktif=true,
                    Resim="music2.jpg",
                    Anasayfa=true,
                    Aciklama=" 18 Yaş sınırı vardır. Belirtilen saat kapı açılış saatidir.Satın alınan biletlerde iptal, iade ve değişiklik yapılmamaktadır.Organizasyon firması etkinlik için uygun görmediği kişileri, bilet ücretini iade ederek etkinlik mekanına almama hakkına sahiptir.",
                    KategoriId=3
                },

            }
        );
    }

}
