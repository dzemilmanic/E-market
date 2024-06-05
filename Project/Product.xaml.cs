using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
                Proizvod p = new Proizvod
                {
                    Naziv = txtProductName.Text.Trim(),
                    Cena = cena,
                    Kolicina = kolicina
                };
                proizvodi.Add(p);

                using (var context = new sales_systemEntities())
                {
                    context.Proizvod.Add(p);
                    context.SaveChanges();
                }
                MessageBox.Show("Uspesno ste dodali novi proizvod");
            }
        }
        
        private void LoadProducts()
        {
            proizvodi = new ObservableCollection<Proizvod>();
            using (var context = new sales_systemEntities())
            {
                var products = context.Proizvod.ToList();
                foreach (var product in products)
                {
                    proizvodi.Add(product);
                }
            }
            lvProducts.ItemsSource = proizvodi;
        }


    }
}
