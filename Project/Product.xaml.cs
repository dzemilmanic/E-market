using System;
using System.Collections.Generic;
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
    public partial class Product : Page
    {
        public Product()
        {
            InitializeComponent();
        }
        SALES_SYSTEMEntities2 context = new SALES_SYSTEMEntities2();
        public string ProductName { get; private set; }
        public string ProductQuantity { get; private set; }
        public string ProductPrice { get; private set; }

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
            else if (String.IsNullOrEmpty(txtProductQuantity.Text))
            {
                MessageBox.Show("Morate uneti kolicinu proizvoda");
            }
            else
            {
                Product p = new Product
                {
                    ProductName = txtProductName.Text.Trim(),
                    ProductPrice = txtProductPrice.Text.Trim(),
                    ProductQuantity = txtProductQuantity.Text.Trim(),
                };
                context.Proizvod.Add(p);
                context.SaveChanges();
                MessageBox.Show("Uspesno ste dodali novi proizvod");

                // Pozivanje događaja kada je kategorija dodata
                CategoryAdded?.Invoke(this, EventArgs.Empty);

            }
        }
    }
}
