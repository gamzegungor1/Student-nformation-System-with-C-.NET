using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciEgitimApp.Models
{
    public class OgrenciEgitimDBContext : DbContext
    {
        public DbSet<Ogrenci> Ogrenciler { get; set; }
        public DbSet<Egitim> Egitimler { get; set; }

        public DbSet<Kayit> Kayitlar { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        {
            optionsBuilder.UseSqlServer("Server=localhost,1434;Database=OgrenciEgitimDB;User Id=sa;Password=Password1;TrustServerCertificate=True");
        }
   


    }
}

//Görsel kısımda veri tabanı işlemleri yapılmaz 