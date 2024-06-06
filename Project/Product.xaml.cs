using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
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
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class Product : Page, INotifyPropertyChanged
    {

        private ObservableCollection<Proizvod> proizvodi { get; set; }
        public Product()
        {
            InitializeComponent();
            LoadProducts();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void btnAddProduct(object sender, RoutedEventArgs e)
        {
            string loggedInUsername = Login.LoggedIn;

            using (var context = new sales_systemEntities2())
            {
                var korisnik = context.Korisnici.SingleOrDefault(u => u.Username == loggedInUsername);
                if (korisnik != null && context.Kupac.Any(k => k.KorisnikID == korisnik.KorisnikID))
                {
                    MessageBox.Show("Vi ste kupac i ne možete dodavati nove proizvode");
                    return;
                }

                if (String.IsNullOrEmpty(txtProductName.Text))
                {
                    MessageBox.Show("Morate uneti ime proizvoda");
                }
                else if (String.IsNullOrEmpty(txtProductPrice.Text))
                {
                    MessageBox.Show("Morate uneti cenu proizvoda");
                }
                else if (!decimal.TryParse(txtProductPrice.Text.Trim(), out decimal cena))
                {
                    MessageBox.Show("Cena proizvoda mora biti validan decimalni broj");
                    return;
                }
                else if (String.IsNullOrEmpty(txtProductQuantity.Text))
                {
                    MessageBox.Show("Morate uneti kolicinu proizvoda");
                }
                else if (!int.TryParse(txtProductQuantity.Text.Trim(), out int kolicina))
                {
                    MessageBox.Show("Količina proizvoda mora biti validan ceo broj");
                    return;
                }
                else
                {
                    if (context.Proizvod.Any(pr => pr.Naziv == txtProductName.Text.Trim()))
                    {
                        MessageBox.Show("Proizvod sa unetim imenom već postoji, ukoliko ste uneli novu cenu ili količinu pokušajte da ažurirate proizvod");
                        return;
                    }
                    var prodavac = context.Prodavac.FirstOrDefault(pr => pr.Korisnici.Username == loggedInUsername);
                    Proizvod p = new Proizvod
                    {
                        ProdavacID = prodavac.KorisnikID,
                        Naziv = txtProductName.Text.Trim(),
                        Cena = cena,
                        Kolicina = kolicina
                    };

                    context.Proizvod.Add(p);
                    context.SaveChanges();

                    proizvodi.Add(p);
                    MessageBox.Show("Uspesno ste dodali novi proizvod");
                }
            }
        }


        private void LoadProducts()
        {
            proizvodi = new ObservableCollection<Proizvod>();
            using (var context = new sales_systemEntities2())
            {
                var products = context.Proizvod.ToList();
                foreach (var product in products)
                {
                    proizvodi.Add(product);
                }
            }
            lvProducts.ItemsSource = proizvodi;
        }

        private void lvProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvProducts.SelectedItem != null)
            {
                Proizvod selectedProduct = (Proizvod)lvProducts.SelectedItem;

                txtProductName.Text = selectedProduct.Naziv;
                txtProductPrice.Text = selectedProduct.Cena.ToString();
                txtProductQuantity.Text = selectedProduct.Kolicina.ToString();
            }
        }
        private void btnUpdateProduct(object sender, RoutedEventArgs e)
        {
            string loggedInUsername = Login.LoggedIn;

            using (var context = new sales_systemEntities2())
            {
                var korisnik = context.Korisnici.SingleOrDefault(u => u.Username == loggedInUsername);
                if (korisnik != null && context.Kupac.Any(k => k.KorisnikID == korisnik.KorisnikID))
                {
                    MessageBox.Show("Kupci ne mogu ažurirati proizvode.");
                    return;
                }

                var prodavac = context.Prodavac.FirstOrDefault(pr => pr.Korisnici.Username == loggedInUsername);
                

                if (lvProducts.SelectedItem != null)
                {
                    Proizvod selectedProduct = (Proizvod)lvProducts.SelectedItem;

                    bool isProductOwnedByLoggedInVendor = selectedProduct.ProdavacID == prodavac.KorisnikID;

                    if (!isProductOwnedByLoggedInVendor)
                    {
                        MessageBox.Show("Ne možete ažurirati proizvod koji nije vaš");
                        return;
                    }

                    if (selectedProduct.Naziv != txtProductName.Text.Trim() ||
                        selectedProduct.Cena != decimal.Parse(txtProductPrice.Text.Trim()) ||
                        selectedProduct.Kolicina != int.Parse(txtProductQuantity.Text.Trim()))
                    {
                        selectedProduct.Naziv = txtProductName.Text.Trim();
                        if (decimal.TryParse(txtProductPrice.Text.Trim(), out decimal cena))
                        {
                            selectedProduct.Cena = cena;
                        }
                        else
                        {
                            MessageBox.Show("Cena proizvoda mora biti validan decimalni broj");
                            return;
                        }

                        if (int.TryParse(txtProductQuantity.Text.Trim(), out int kolicina))
                        {
                            selectedProduct.Kolicina = kolicina;
                        }
                        else
                        {
                            MessageBox.Show("Količina proizvoda mora biti validan ceo broj");
                            return;
                        }

                        context.Entry(selectedProduct).State = EntityState.Modified;
                        context.SaveChanges();

                        MessageBox.Show("Proizvod je uspešno ažuriran");
                        LoadProducts();
                    }
                    else
                    {
                        MessageBox.Show("Nije izmenjeno ništa. Morate izmeniti barem jedno polje.");
                    }
                }
                else
                {
                    MessageBox.Show("Niste odabrali proizvod za ažuriranje, pokušajte da dodate uneti proizvod");
                }
            }
        }

        private void btnDeleteProduct(object sender, RoutedEventArgs e)
        {
            if (lvProducts.SelectedItem != null)
            {
                Proizvod selectedProduct = (Proizvod)lvProducts.SelectedItem;

                string loggedInUsername = Login.LoggedIn;

                using (var context = new sales_systemEntities2())
                {
                    var kupac = context.Kupac.FirstOrDefault(k => k.Korisnici.Username == loggedInUsername);
                    if (kupac != null)
                    {
                        MessageBox.Show("Kupci ne mogu brisati proizvode.");
                        return;
                    }
                    var prodavac = context.Prodavac.FirstOrDefault(pr => pr.Korisnici.Username == loggedInUsername);
                    
                    var productToDelete = context.Proizvod.FirstOrDefault(p => p.ProizvodID == selectedProduct.ProizvodID);
                    if (productToDelete != null)
                    {
                        var existingOrder = context.NarudzbinaProizvod.Any(np => np.ProizvodID == selectedProduct.ProizvodID);
                        if (existingOrder)
                        {
                            MessageBox.Show("Ne možete obrisati proizvod jer je već dodat u neku narudžbinu.");
                            return;
                        }

                        MessageBoxResult result = MessageBox.Show($"Da li ste sigurni da želite da obrišete proizvod {selectedProduct.Naziv}?", "Potvrda brisanja", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                        if (result == MessageBoxResult.Yes)
                        {
                            context.Proizvod.Remove(productToDelete);
                            context.SaveChanges();

                            proizvodi.Remove(selectedProduct);
                            MessageBox.Show("Proizvod je uspešno obrisan");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Proizvod nije pronađen u bazi podataka");
                    }
                }
            }
            else
            {
                MessageBox.Show("Morate odabrati proizvod za brisanje");
            }
        }

    }
}

