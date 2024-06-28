using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project
{
    /// <summary>
    /// Interaction logic for Raport.xaml
    /// </summary>
    public partial class Raport : Page, INotifyPropertyChanged
    {
        string loggedInUsername;
        int userID;

        private decimal totalSales;
        private string statusText;
        private string raportText;

        private ObservableCollection<NarudzbinaProizvod> narudzbineProizvodi;
        public decimal TotalSales
        {
            get { return totalSales; }
            set { if (totalSales != value)
                    {
                        totalSales = value;
                        OnPropertyChanged(nameof(TotalSales));
                    }
                }
        }

        public string StatusText
        {
            get { return statusText; }
            set { if (statusText != value)
                    {
                        statusText = value;
                        OnPropertyChanged(nameof(StatusText));
                    }
                }
        }
        public string RaportText
        {
            get { return raportText; }
            set { if (raportText != value)
                    {
                        raportText = value;
                        OnPropertyChanged(nameof(RaportText));
                    }
                }
        }
        public Raport()
        {
            InitializeComponent();
            loggedInUsername = Login.LoggedIn;
            userID = GetUserIDByUsername(loggedInUsername);
            decimal TotalSales = CalculateTotalSales(userID);
            txtTotalSales.Text = TotalSales.ToString();
            LoadOrderedProducts();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private decimal CalculateTotalSales(int userID)
        {
            using (var context = new sales_systemEntities2())
            {
                var isCustomer = context.Kupac.Any(k => k.KorisnikID == userID);
                var isVendor = context.Prodavac.Any(p => p.KorisnikID == userID);

                if (isCustomer)
                {
                    RaportText = "Pregled vaše potrošnje";
                    StatusText = "Ukupno ste potrošili: ";
                    TotalSales = (decimal)context.NarudzbinaProizvod
                        .Where(np => np.Narudzbina.KupacID == userID)
                        .Sum(np => np.Proizvod.Cena * np.Kolicina).GetValueOrDefault();
                }
                else if (isVendor)
                {
                    RaportText = "Pregled vašeg prometa";
                    StatusText = "Prodali ste proizvode u vrednosti: ";
                    TotalSales = (decimal)context.NarudzbinaProizvod
                        .Where(np => np.Proizvod.ProdavacID == userID)
                        .Sum(np => np.Proizvod.Cena * np.Kolicina).GetValueOrDefault();
                }
                else
                {
                    RaportText = "Greška prilikom prepoznavanja korisnika";
                    StatusText = "";
                    TotalSales = 0;
                }
            }

            return totalSales;
        }
        private void LoadOrderedProducts()
        {
            string loggedInUsername = Login.LoggedIn;
            int userID = GetUserIDByUsername(loggedInUsername);

            if (userID == -1)
            {
                MessageBox.Show("Korisnik nije pronađen.");
                return;
            }

            using (var context = new sales_systemEntities2())
            {
                var orderedProducts = context.NarudzbinaProizvod
                    .Where(np => np.Narudzbina.KupacID == userID || np.Proizvod.ProdavacID == userID)
                        .Select(np => new
                {
                    NarudzbinaID = np.NarudzbinaID,
                    ProizvodID = np.ProizvodID,
                    Kolicina = np.Kolicina,
                    Cena = np.Cena,
                    imeKupca = np.Narudzbina.Kupac.FizickoLice != null ? np.Narudzbina.Kupac.FizickoLice.Ime : np.Narudzbina.Kupac.Firma.Naziv,
                    imeProdavca = np.Proizvod.Prodavac.FizickoLice != null ? np.Proizvod.Prodavac.FizickoLice.Ime : np.Proizvod.Prodavac.Firma.Naziv,
                    imeProizvoda = np.Proizvod.Naziv
                }).ToList();

                var orderedProductsList = orderedProducts
                    .Select(op => new NarudzbinaProizvod
                    {
                        NarudzbinaID = op.NarudzbinaID,
                        ProizvodID = op.ProizvodID,
                        Kolicina = op.Kolicina,
                        Cena = op.Cena,
                        imeKupca = op.imeKupca,
                        imeProdavca = op.imeProdavca,
                        imeProizvoda = op.imeProizvoda
                    }).ToList();

                narudzbineProizvodi = new ObservableCollection<NarudzbinaProizvod>(orderedProductsList);
                lvOrderedProducts.ItemsSource = narudzbineProizvodi;
            }
        }
        private void btnGenerateReport(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                string filename = saveFileDialog.FileName;
                WriteReportToFile(filename);
                MessageBox.Show("Izveštaj je uspešno generisan i sačuvan.");
            }
        }
        private void WriteReportToFile(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine($"Izveštaj: {RaportText}");
                writer.WriteLine($"{StatusText} {TotalSales:C}");
                writer.WriteLine("Detalji:");
                foreach (var np in narudzbineProizvodi)
                {
                    writer.WriteLine($"- Narudžbina ID: {np.NarudzbinaID}, Proizvod: {np.imeProizvoda}, Količina: {np.Kolicina}, Cena: {np.Cena:C}, Kupac: {np.imeKupca}, Prodavac: {np.imeProdavca}");
                }
            }
        }
        private int GetUserIDByUsername(string username)
        {
            int userID = 0;

            using (var context = new sales_systemEntities2())
            {
                var user = context.Korisnici.FirstOrDefault(u => u.Username == username);
                if (user != null)
                {
                    userID = user.KorisnikID;
                }
            }

            return userID;
        }
    }
}
