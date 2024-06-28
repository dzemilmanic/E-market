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
using System.Windows.Shapes;

namespace Project
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        public static string LoggedIn { get; private set; }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!LoginAuth.IsValidUser(tbUsername.Text, tbPassword.Password))
            {
                MessageBox.Show("Uneli ste pogrešno korisničko ime ili lozinku");
            }
            else
            {
                MainWindow mw = new MainWindow();
                LoggedIn = tbUsername.Text;
                mw.Show();
                this.Close();
            }
        }
                
    }
}
