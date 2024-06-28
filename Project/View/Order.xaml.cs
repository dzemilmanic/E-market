using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class Order : Page, INotifyPropertyChanged
    {
        public ObservableCollection<NarudzbinaProizvod> narudzbinaProizvodi { get; set; }
        public Order()
        {
            InitializeComponent();
            narudzbinaProizvodi = new ObservableCollection<NarudzbinaProizvod>();
            LoadProducts();
            listViewNarudzbine.ItemsSource = narudzbinaProizvodi;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void LoadProducts()
        {
            using (var context = new sales_systemEntities2())
            {
                var products = context.Proizvod.ToList();
                comboBoxProizvodi.ItemsSource = products;
                comboBoxProizvodi.DisplayMemberPath = "Naziv";
                comboBoxProizvodi.SelectedValuePath = "Id";
            }

        }
        private void AddProduct(object sender, RoutedEventArgs e)
        {
            string loggedInUsername = Login.LoggedIn;
            using (var context = new sales_systemEntities2())
            {
                var user = context.Korisnici.SingleOrDefault(u => u.Username == loggedInUsername);
                if (user != null && context.Prodavac.Any(p => p.KorisnikID == user.KorisnikID))
                {
                    MessageBox.Show("Vi ste prodavac i ne mozete kupovati");
                    return;
                }
            }
            if (comboBoxProizvodi.SelectedItem == null)
            {
                MessageBox.Show("Izaberite artikal iz liste");
                return;
            }

            if (!int.TryParse(textBoxKolicina.Text, out int inputQuantity))
            {
                MessageBox.Show("Unesite ispravnu numeričku vrednost");
                return;
            }

            var selectedProduct = comboBoxProizvodi.SelectedItem as Proizvod;
            if (selectedProduct != null)
            {
                using (var context = new sales_systemEntities2())
                {
                    var product = context.Proizvod.SingleOrDefault(p => p.ProizvodID == selectedProduct.ProizvodID);
                    if (product != null)
                    {
                        if (inputQuantity > product.Kolicina)
                        {
                            MessageBox.Show("Uneta količina je veća od dostupne");
                        }
                        else
                        {
                            
                            var kupac = context.Kupac.FirstOrDefault(k => k.Korisnici.Username == loggedInUsername);
                            var novaStavka = new NarudzbinaProizvod
                            {
                                imeKupca = kupac.FizickoLice != null ? kupac.FizickoLice.Ime : kupac.Firma != null ? kupac.Firma.Naziv : null,
                                ProizvodID = product.ProizvodID,
                                imeProizvoda = product.Naziv,
                                Kolicina = inputQuantity,
                                Cena = (decimal)(product.Cena * inputQuantity),
                                imeProdavca = product.Prodavac.FizickoLice != null ? product.Prodavac.FizickoLice.Ime : product.Prodavac.Firma != null ? product.Prodavac.Firma.Naziv : null
                            };
                            narudzbinaProizvodi.Add(novaStavka);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Proizvod nije pronađen.");
                    }
                }
            }
        }
        private void btnSend(object sender, RoutedEventArgs e)
        {
            string loggedInUsername = Login.LoggedIn;
            using (var context = new sales_systemEntities2())
            {
                var kupac = context.Kupac.FirstOrDefault(k => k.Korisnici.Username == loggedInUsername);
                //string nazivKupca = kupac.FizickoLice != null ? kupac.FizickoLice.Ime : kupac.Firma.Naziv;

                var prodavac = context.Prodavac.FirstOrDefault(p => p.Korisnici.KorisnikID == p.KorisnikID);
                var novaNarudzbina = new Narudzbina
                {                    
                    KupacID = kupac.KorisnikID,
                    KupacIme = kupac.FizickoLice != null ? kupac.FizickoLice.Ime : kupac.Firma != null ? kupac.Firma.Naziv : null,
                    DatumNarudzbine = DateTime.Now,
                    Status = "Primljeno"
                };

                context.Narudzbina.Add(novaNarudzbina);
                context.SaveChanges();

                
                foreach (var stavka in narudzbinaProizvodi)
                {
                    var narudzbinaProizvod = new NarudzbinaProizvod
                    {
                        imeKupca = novaNarudzbina.KupacIme,
                        NarudzbinaID = novaNarudzbina.NarudzbinaID,
                        ProizvodID = stavka.ProizvodID,
                        imeProizvoda = stavka.imeProizvoda,
                        Kolicina = stavka.Kolicina,
                        Cena = stavka.Cena,
                        imeProdavca = stavka.imeProdavca,
                    };

                    context.NarudzbinaProizvod.Add(narudzbinaProizvod);

                    var product = context.Proizvod.SingleOrDefault(p => p.ProizvodID == stavka.ProizvodID);
                    if (product != null)
                    {
                        product.Kolicina -= stavka.Kolicina;
                        
                    }
                }

                context.SaveChanges();
                MessageBox.Show("Narudžbina je uspešno kreirana");
            }

            narudzbinaProizvodi.Clear();
        }

        private void btnCancel(object sender, RoutedEventArgs e)
        {
            narudzbinaProizvodi.Clear();

            MessageBox.Show("Narudžbina je otkazana");
        }
    }
}

