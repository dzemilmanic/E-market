//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class NarudzbinaProizvod
    {
        public int NarudzbinaID { get; set; }
        public int ProizvodID { get; set; }
        public Nullable<int> Kolicina { get; set; }
        public Nullable<decimal> Cena { get; set; }
        public string imeKupca { get; set; }
        public string imeProizvoda { get; set; }
        public string imeProdavca { get; set; }
    
        public virtual Narudzbina Narudzbina { get; set; }
        public virtual Proizvod Proizvod { get; set; }
    }
}
