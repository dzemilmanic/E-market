using System;
using System.Collections.Generic;
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
    public partial class Order : Page
    {
        public Order()
        {
            InitializeComponent();
            LoadProducts();
        }
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
                    }
                    else
                    {
                        MessageBox.Show("Proizvod nije pronađen.");
                    }
                }
            }
        }
    }
}
