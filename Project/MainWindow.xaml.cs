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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            tbOption.Text = "";
            Home h = new Home();
            showPage.Content = h;
        }

        private void btnHome(object sender, RoutedEventArgs e)
        {
            tbOption.Text = Login.LoggedIn;

            if (showPage.Content != null)
            {
                ((Page)showPage.Content).NavigationService.RemoveBackEntry();
            }

            Home h = new Home();
            showPage.Content = h;
        }
        private void btnOrder(object sender, RoutedEventArgs e)
        {
            tbOption.Text = Login.LoggedIn;

            if (showPage.Content != null)
            {
                ((Page)showPage.Content).NavigationService.RemoveBackEntry();
            }

            Order o = new Order();
            showPage.Content = o;
        }
        private void btnOrders(object sender, RoutedEventArgs e)
        {
            tbOption.Text = Login.LoggedIn;

            if (showPage.Content != null)
            {
                ((Page)showPage.Content).NavigationService.RemoveBackEntry();
            }

            Orders h = new Orders();
            showPage.Content = h;
        }
        private void btnProduct(object sender, RoutedEventArgs e)
        {
            tbOption.Text = Login.LoggedIn;

            if (showPage.Content != null)
            {
                ((Page)showPage.Content).NavigationService.RemoveBackEntry();
            }

            Product p = new Product();
            showPage.Content = p;
        }
        private void btnSendMess(object sender, RoutedEventArgs e)
        {
            tbOption.Text = Login.LoggedIn;

            if (showPage.Content != null)
            {
                ((Page)showPage.Content).NavigationService.RemoveBackEntry();
            }

            SendMessage s = new SendMessage();
            showPage.Content = s;
        }
        private void btnRaport(object sender, RoutedEventArgs e)
        {
            tbOption.Text = Login.LoggedIn;

            if (showPage.Content != null)
            {
                ((Page)showPage.Content).NavigationService.RemoveBackEntry();
            }

            Raport r = new Raport();
            showPage.Content = r;
        }
        private void btnOdjava(object sender, RoutedEventArgs e)
        {
            Login l = new Login();
            l.Show();
            this.Close();
           
        }
    }
    
}
