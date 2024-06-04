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

        public int ProizvodID { get => proizvodID; set => SetProperty(ref proizvodID, value); }
        public string NazivProizvoda { get => nazivProizvoda; set => SetProperty(ref nazivProizvoda, value); }
        public int Kolicina { get => kolicina; set => SetProperty(ref kolicina, value); }
        public decimal Cena { get => cena; set => SetProperty(ref cena, value); }
        public string KupacIme { get => kupacIme; set => SetProperty(ref kupacIme, value); }

        public event PropertyChangedEventHandler PropertyChanged;
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
