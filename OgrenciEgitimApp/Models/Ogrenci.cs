using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciEgitimApp.Models
{

    public class Ogrenci
    {
        public int Id { get; set; }
        [StringLength(50,ErrorMessage ="En Fazla 50 Karaketer Girebilirsiniz")]
        public string  Ad { get; set; }
        [StringLength(50)]

        public string Soyad{ get; set; }
        //? işareti propertiyi nullable yapar.
        public int? Yas { get; set; }


        public override string ToString()
        {
            return $"{Id} {Ad} {Soyad} {Yas}";


        }

    }
}
