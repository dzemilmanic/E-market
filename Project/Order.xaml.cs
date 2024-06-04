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
        public ObservableCollection<NarudzbinaProizvodKupac> narudzbinaProizvodi {  get; set; }
        public Order()
        {
            InitializeComponent();
            narudzbinaProizvodi = new ObservableCollection<NarudzbinaProizvodKupac>();
            LoadProducts();
            listViewNarudzbine.ItemsSource = narudzbinaProizvodi;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void LoadProducts()
        {
            using (var context = new SALES_SYSTEMEntities2())
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
                using (var context = new SALES_SYSTEMEntities2())
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
                                Cena = product.Cena * inputQuantity,
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
            string kupac = Login.LoggedIn;

            using (var context = new SALES_SYSTEMEntities2())
            {
                var novaNarudzbina = new Narudzbina
                {
                    DatumNarudzbine = DateTime.Now,
                    KupacID = 1,
                    ProdavacID = 1
                };

                context.Narudzbina.Add(novaNarudzbina);
                context.SaveChanges();

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

