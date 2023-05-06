﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciEgitimApp.Models
{
    public class Egitim
    {
        public int Id { get; set; }
        public string Ad { get; set; }

        public string Aciklama { get; set; }


        public override string ToString()
        {
            return $"Id:{Id} Ad:{Ad} Aciklama:{Aciklama}";
        }
    }
}
