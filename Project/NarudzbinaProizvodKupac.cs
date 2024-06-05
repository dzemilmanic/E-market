using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class NarudzbinaProizvodKupac : INotifyPropertyChanged
    {
        private int proizvodID;
        private string nazivProizvoda;
        private int kolicina;
        private decimal cena;
        private string kupacIme;

        public int ProizvodID { get => proizvodID; set
            {
                if (proizvodID != value)
                {
                    proizvodID = value;
                    OnPropertyChanged(nameof(ProizvodID));
                }
            }
        }
        public string NazivProizvoda { get => nazivProizvoda; set
            {
                if (nazivProizvoda != value)
                {
                    nazivProizvoda = value;
                    OnPropertyChanged(nameof(NazivProizvoda));
                }
            }
        }
        public int Kolicina { get => kolicina; set
            {
                if (kolicina != value)
                {
                    kolicina = value;
                    OnPropertyChanged(nameof(Kolicina));
                }
            }
        }
        public decimal Cena { get => cena; set
            {
                if (cena != value)
                {
                    cena = value;
                    OnPropertyChanged(nameof(Cena));
                }
            }
        }
        public string KupacIme { get => kupacIme; set
            {
                if (kupacIme != value)
                {
                    kupacIme = value;
                    OnPropertyChanged(nameof(KupacIme));
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
