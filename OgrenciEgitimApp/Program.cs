//Öğrenci 
//Eğitim 
//Kayıt 

//DbFirst : Veritabanı varsa 
//CodeFirst
//Nesne oluşturma

using OgrenciEgitimApp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;


#region Eski Yöntem 
// SqlConnection connection = new SqlConnection("Server=localhost,1434;Database=OgrenciEgitimDBContext);


/*Ogrenci ogrenci1 = new Ogrenci();

ogrenci1.Ad = "Fatih";
ogrenci1.Soyad= "Baytar";
ogrenci1.Yas = 35;
ogrenci1.Id = 1;


Egitim egitim1 = new Egitim();
egitim1.Ad = "Temel C# Kursu ";
egitim1.Id = 1;
egitim1.Aciklama = "Başlangıçtan Uç Seviyeye C# Dili ";*/
#endregion

internal class Program
{

    static OgrenciEgitimDBContext db = new OgrenciEgitimDBContext();
    private static void Main(string[] args)
    {
        // ButunOgrencileriGetir();
        Console.WriteLine("İşlem Seçiniz(1:Öğrenci, 2:Eğitim,3:Kayıt,Q: Çıkış ):");
        var secim = Console.ReadLine();
        switch (secim)
        {
            case "1":
                //Öğrenci İşleri Çağırıldı 
                OgrenciIsleri();
                break;
            case "2":
                //Eğitim  İşleri Çağırıldı 
                EgitimIsleri();
                break;
            case "3":
                //Kayıt İşleri Çağırıldı 
                KayıtIsleri();
                break;
            case "Q":
               Console.WriteLine("Uygulamadan Çıkılıyor....");
                break;


        }

        //  var yeniOgrenci = new Ogrenci { Ad = "yusuf ", Soyad = "kaplan ", Yas = 22 };
        //  var yeniId = OgrenciKaydet(yeniOgrenci);
        //
        //  Console.WriteLine(yeniId);
        //  OgrenciKaydet (yeniOgrenci);
        //  | split özelliklerini en çok konsol uygulamalarında kullanırız
    }


    #region Kayıt İşlemleri 

    private static void KayıtIsleri()
    {
        Console.Write("İşlem Seçiniz(1:Tüm Kayıtlar  ,2: Yeni Kayıt , 3:Kayıt Sil):");
        var secim = Console.ReadLine();
        switch (secim)
        {
            case "1":
                ButunKayitlariGetir();
                break;
            case "2":
                KayitKaydet();
                break;
            case "3":
                if (KayitSil())
                    Console.WriteLine("Başarılı");
                else
                    Console.WriteLine("Başarısız");
                break;
            default:
                Console.WriteLine("Böyle Bir Seçim Yok ");
                break;
        }
    }

    private static bool KayitSil()
    {
        Console.Write("Silinecek Kaydın Id'ini yazın: ");
        var secim = Console.ReadLine();
        var silinecekKayitId = Convert.ToInt32(secim);//LINQ
        var silinecekKayit = db.Egitimler.FirstOrDefault(o => o.Id == silinecekKayitId);

        if (silinecekKayit != null)
        {
            db.Egitimler.Remove(silinecekKayit);
            var etkilenenSatirSayisi = db.SaveChanges();
            if (etkilenenSatirSayisi > 0)
            {
                return true;
            }

        }
        return false;
    }
    //Öğrencileri eğitimlerle eşleştirip kullanıcı ID girecek.

    private static bool KayitKaydet()
    {
        ButunOgrencileriGetir();
        ButunEgitimleriGetir();


        Console.WriteLine("Kayıt İçin Gerekli Idleri girin");
        var okunan = Console.ReadLine();
        //1|1  ---> böyle bişey bekleniyor
        var yeniKayit = new Kayit();
        yeniKayit.OgrenciId = Convert.ToInt32(okunan.Split("|")[0]);
        yeniKayit.EgitimId = Convert.ToInt32(okunan.Split("|")[1]);

        //// 'Soyad' değeri atanıyor
        var ogrenci = db.Ogrenciler.FirstOrDefault(o => o.Id == yeniKayit.OgrenciId);
        if(ogrenci != null)
        {
            yeniKayit.Soyad = ogrenci.Soyad;
        }

        db.Kayitlar.Add(yeniKayit);
        var sonuc = db.SaveChanges();

        return sonuc > 0;
        //bu şekilde gösterim yapılabilir.

    }

    private static void ButunKayitlariGetir()
    {
        var tumKayitlar = db.Kayitlar.Include(k=>k.Ogrenci).Include(k=>k.Egitim);
        foreach(var kayit in tumKayitlar)
        {
            Console.WriteLine($"Ogrenci Adı: {kayit.Ogrenci?.Ad} {kayit.Ogrenci?.Soyad} Eğitim Adı: {kayit.Egitim.Ad}");
        }
    }
    //Soru işareti ? bir null-conditional operatördür ve C# programlama dilinde kullanılır.
    //Bu operatör, bir nesne üzerinde işlem yapmadan önce nesnenin null olup olmadığını kontrol eder.
    //Yukarıdaki kod örneğinde, kayit.Ogrenci özelliği null olabilir. Bu durumda, kayit.Ogrenci?.Ad ifadesi null değer döndürecektir
    //ve kod hataya neden olmadan devam edecektir.
    //Yukarıdaki kod örneğinde, kayit.Ogrenci özelliği null olabilir. Bu durumda, kayit.Ogrenci?.Ad ifadesi
    //null değer döndürecektir ve kod hataya neden olmadan devam edecektir.
   

    //Null olan bir şeyin özelliğine erişmeye çalışırsak hata fırlatır.



    #endregion


    #region Eğitim İşleri 
    private static void EgitimIsleri()
    {
        Console.Write("İşlem Seçiniz(1:Tüm Eğitimler  ,2: Yeni Eğitim , 3:Eğitim Sil):");
        var secim = Console.ReadLine();
        switch (secim)
        {
            case "1":
                ButunEgitimleriGetir();
                break;
            case "2":
                EgitimKaydet();
                break;
            case "3":
                if (EgitimSil())
                    Console.WriteLine("Başarılı");
                else
                    Console.WriteLine("Başarısız");
                break;
            default:
                Console.WriteLine("Böyle Bir Seçim Yok ");
                break;
        }
    }

    private static bool EgitimSil()
    {
        Console.Write("Silinecek Eğitimin Id'ini yazın: ");
        var secim = Console.ReadLine();
        var silinecekEgitimId = Convert.ToInt32(secim);//LINQ
        var silinecekEgitim = db.Egitimler.FirstOrDefault(o => o.Id == silinecekEgitimId);

        if (silinecekEgitim != null)
        {
            db.Egitimler.Remove(silinecekEgitim);
            var etkilenenSatirSayisi = db.SaveChanges();
            if (etkilenenSatirSayisi > 0)
            {
                return true;
            }

        }
        return false;
    }

    private static void EgitimKaydet()
    {
        Console.Write("Yeni Eğitimin  Özelliklerini yaz:(Ad|Açıklama):");
        var okunan = Console.ReadLine();
        //C#| Temel C# Eğitimi
        var parcalar = okunan.Split('|');

        var yeniEgitim =new Egitim();
        yeniEgitim.Ad = parcalar[0];
        yeniEgitim.Aciklama = parcalar[1];
        EgitimKaydet(yeniEgitim);

    }

    private static int EgitimKaydet(Egitim yeniEgitim)
    {
        db.Egitimler.Add(yeniEgitim);
        db.SaveChanges(); // Hafızadaki değişikliklerini veri tabanına yansıtır.
                          //SaveChanges unutulursa veri tabanındaki değişiklikler görülemez.
        return yeniEgitim.Id;
    }

    private static void ButunEgitimleriGetir()
    {
        Console.WriteLine("------------ Bütün Eğitimler -------------------");
        foreach(var egitim in db.Egitimler)
        {
        Console.WriteLine(egitim.ToString());    
        }

    }
    #endregion


    #region Öğrenci İşleri
    private static void OgrenciIsleri()
    {
        Console.Write("İşlem Seçiniz(1:Tüm Öğrenciler ,2: Yeni Kayıt, 3:Öğrenci Sil):");
        var secim = Console.ReadLine(); 
        switch(secim)
        {
            case"1":
                ButunOgrencileriGetir();
                    break;
                case "2":
                OgrenciKaydet();
                break;
                case "3":
              if(OgrenciSil())
                    Console.WriteLine("Başarılı"); 
                else
                    Console.WriteLine("Başarısız");
                break;
            default:
                Console.WriteLine("Böyle Bir Seçim Yok ");
                break;
        }
       
    }

    private static bool OgrenciSil()
    {
        Console.Write("Silinecek Öğrencinin Id'ini yazın: ");
        var secim=Console.ReadLine();
        var silinecekOgrenciId = Convert.ToInt32(secim);//LINQ
        var silinecekOgrenci = db.Ogrenciler.FirstOrDefault(o => o.Id == silinecekOgrenciId);

        if (silinecekOgrenci != null)
        {
            db.Ogrenciler.Remove(silinecekOgrenci);
            var etkilenenSatirSayisi = db.SaveChanges();
            if (etkilenenSatirSayisi > 0)
            {
                return true;
            } 

        }
        return false;
        //Dil entegre edilmiş sorgular LINQ
        //Referans tiplerin varsayılan değeri null dır. 
    }

    static void OgrenciKaydet()
    {
        Console.Write("Yeni Öğrencinin Özelliklerini yaz:(Ad|Soyad|Yas):");
        var okunan = Console.ReadLine();
        //Gamze|Güngör|25
        var parcalar = okunan.Split('|');
        var yeniOgrenci = new Ogrenci();
        yeniOgrenci.Ad = parcalar[0];
        yeniOgrenci.Soyad = parcalar[1];
        var test = string.IsNullOrEmpty(parcalar[2]) ? " 0 " : parcalar[2];
        yeniOgrenci.Yas = Convert.ToInt32(test);
        OgrenciKaydet(yeniOgrenci);
    }
    // Artık sadece öğrencikaydet isimli parametresiz metodu çağırırsam 

    static int OgrenciKaydet(Ogrenci yeniOgrenci)

    {

        db.Ogrenciler.Add(yeniOgrenci);
        db.SaveChanges(); // Hafızadaki değişikliklerini veri tabanına yansıtır.
                          //SaveChanges unutulursa veri tabanındaki değişiklikler görülemez.
        return yeniOgrenci.Id;
    }
    static void ButunOgrencileriGetir()
    {

        var ogrenciler = db.Ogrenciler.ToList();
        Console.WriteLine("------------Bütün Öğrenciler ------------");
        foreach (var ogrenci in ogrenciler)
        {
            // Console.WriteLine($"{ogrenci.Ad} {ogrenci.Soyad} {ogrenci.Yas}");
            //Merkezi yönetimi sağlar
            //Bir çok yerde kullanıldığında sadece ekleme yağtığımız yerde deişikllik yapmaya yarar
            //Ekstra bir değişikliğe gerek kalmaz.
            //
            Console.WriteLine(ogrenci.ToString());
        }
    }
    #endregion
}



//metodlar arası 1 boşluk 
//regionlar arası 2 boşluk bırakılır.