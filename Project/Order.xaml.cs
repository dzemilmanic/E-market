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
        public ObservableCollection<NarudzbinaProizvodKupac> narudzbinaProizvodi { get; set; }
        public ObservableCollection<NarudzbinaDetalji> narudzbineDetalji { get; set; }
        public Order()
        {
            InitializeComponent();
            narudzbinaProizvodi = new ObservableCollection<NarudzbinaProizvodKupac>();
            narudzbineDetalji = new ObservableCollection<NarudzbinaDetalji>();
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
                            string kupac = Login.LoggedIn;

                            var novaStavka = new NarudzbinaProizvodKupac
                            {
                                ProizvodID = product.ProizvodID,
                                NazivProizvoda = product.Naziv,
                                Kolicina = inputQuantity,
                                Cena = (decimal)(product.Cena * inputQuantity),
                                KupacIme = kupac
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
                string nazivKupca = kupac.FizickoLice != null ? kupac.FizickoLice.Ime : kupac.Firma.Naziv;


                var prodavac = context.Prodavac.FirstOrDefault(p => p.Korisnici.KorisnikID == p.KorisnikID);
                var novaNarudzbina = new Narudzbina
                {
                    DatumNarudzbine = DateTime.Now,
                    KupacID = kupac.KorisnikID,
                    ProdavacID = prodavac.KorisnikID,
                };

                context.Narudzbina.Add(novaNarudzbina);
                context.SaveChanges();

                var narudzbinaDetalji = new NarudzbinaDetalji
                {
                    NarudzbinaID = novaNarudzbina.NarudzbinaID,
                    NazivKupca = nazivKupca,
                    DatumNarudzbine = (DateTime)novaNarudzbina.DatumNarudzbine
                };
                foreach (var stavka in narudzbinaProizvodi)
                {
                    var narudzbinaProizvod = new NarudzbinaProizvod
                    {
                        NarudzbinaID = novaNarudzbina.NarudzbinaID,
                        ProizvodID = stavka.ProizvodID,
                        Kolicina = stavka.Kolicina,
                        Cena = stavka.Cena
                    };

                    context.NarudzbinaProizvod.Add(narudzbinaProizvod);

                    var product = context.Proizvod.SingleOrDefault(p => p.ProizvodID == stavka.ProizvodID);
                    if (product != null)
                    {
                        product.Kolicina -= stavka.Kolicina;
                        narudzbinaDetalji.Proizvodi.Add(new NarudzbinaProizvodDetalji
                        {
                            NazivProizvoda = product.Naziv,
                            Kolicina = stavka.Kolicina,
                            Cena = stavka.Cena
                        });
                    }
                }

                context.SaveChanges();
                narudzbineDetalji.Add(narudzbinaDetalji);
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

