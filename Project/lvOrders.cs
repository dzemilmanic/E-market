using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class NarudzbinaDetalji : INotifyPropertyChanged
    {
        private int narudzbinaID;
        private string nazivKupca;
        private DateTime datumNarudzbine;
        private ObservableCollection<NarudzbinaProizvodDetalji> proizvodi;

        public int NarudzbinaID
        {
            get { return narudzbinaID; }
            set
            {
                if (narudzbinaID != value)
                {
                    narudzbinaID = value;
                    OnPropertyChanged(nameof(NarudzbinaID));
                }
            }
        }

        public string NazivKupca
        {
            get { return nazivKupca; }
            set
            {
                if (nazivKupca != value)
                {
                    nazivKupca = value;
                    OnPropertyChanged(nameof(NazivKupca));
                }
            }
        }

        public DateTime DatumNarudzbine
        {
            get { return datumNarudzbine; }
            set
            {
                if (datumNarudzbine != value)
                {
                    datumNarudzbine = value;
                    OnPropertyChanged(nameof(DatumNarudzbine));
                }
            }
        }

        public ObservableCollection<NarudzbinaProizvodDetalji> Proizvodi
        {
            get { return proizvodi; }
            set
            {
                if (proizvodi != value)
                {
                    proizvodi = value;
                    OnPropertyChanged(nameof(Proizvodi));
                }
            }
        }

        public NarudzbinaDetalji()
        {
            Proizvodi = new ObservableCollection<NarudzbinaProizvodDetalji>();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
    public class NarudzbinaProizvodDetalji : INotifyPropertyChanged
    {
        private string nazivProizvoda;
        private int kolicina;
        private decimal cena;

        public string NazivProizvoda
        {
            get { return nazivProizvoda; }
            set
            {
                if (nazivProizvoda != value)
                {
                    nazivProizvoda = value;
                    OnPropertyChanged(nameof(NazivProizvoda));
                }
            }
        }

        public int Kolicina
        {
            get { return kolicina; }
            set
            {
                if (kolicina != value)
                {
                    kolicina = value;
                    OnPropertyChanged(nameof(Kolicina));
                }
            }
        }

        public decimal Cena
        {
            get { return cena; }
            set
            {
                if (cena != value)
                {
                    cena = value;
                    OnPropertyChanged(nameof(Cena));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
