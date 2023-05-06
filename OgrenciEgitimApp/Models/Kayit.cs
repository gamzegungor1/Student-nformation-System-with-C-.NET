using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciEgitimApp.Models
{
    public class Kayit
    {
        public int Id { get; set; }
        public int OgrenciId { get; set; }
        public string Soyad { get; set; }
        public int EgitimId { get; set; }

        public Ogrenci Ogrenci { get; set; }
        public Egitim  Egitim { get; set; }


        //amacımız sıçrayış yapmak 

        //Veri tabanında is identity yes yapınca onun birer birer artacağını söylüyoruz 
    }
} 
