using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class NarudzbinaProizvodKupac
    {
        public int ProizvodID { get; set; }
        public string NazivProizvoda { get; set; }
        public int Kolicina { get; set; }
        public decimal Cena { get; set; }
        public string KupacIme { get; set; }
        public string KupacPrezime { get; set; }
    }
}
