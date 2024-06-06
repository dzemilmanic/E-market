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
    /// Interaction logic for Orders.xaml
    /// </summary>
    public partial class Orders : Page, INotifyPropertyChanged
    {
        private ObservableCollection<Narudzbina> _narudzbine;
        public ObservableCollection<Narudzbina> Narudzbine
        {
            get { return _narudzbine; }
            set
            {
                _narudzbine = value;
                OnPropertyChanged(nameof(Narudzbine));
            }
        }


        public Orders()
        {
            InitializeComponent();
            DataContext = this;

            LoadOrders();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void LoadOrders()
        {
            string loggedInUsername = Login.LoggedIn;
            using (var context = new sales_systemEntities2())
            {
                if (loggedInUsername != null)
                {
                    var kupac = context.Kupac.FirstOrDefault(k => k.Korisnici.Username == loggedInUsername);
                    if (kupac != null)
                    {
                        Narudzbine = new ObservableCollection<Narudzbina>(context.Narudzbina.Where(n => n.KupacID == kupac.KorisnikID));
                    }
                    else
                    {
                        var prodavac = context.Prodavac.FirstOrDefault(p => p.Korisnici.Username == loggedInUsername);
                        if (prodavac != null)
                        {
                            Narudzbine = new ObservableCollection<Narudzbina>(context.Narudzbina.Where(n => n.NarudzbinaProizvod.Any(np => np.Proizvod.ProdavacID == prodavac.KorisnikID)));
                        }
                    }
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void listViewNarudzbine_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string loggedInUsername = Login.LoggedIn;

            using (var context = new sales_systemEntities2()) { 
                if (listViewNarudzbine.SelectedItem != null)
                {
                    Narudzbina selectedOrder = (Narudzbina)listViewNarudzbine.SelectedItem;

                    if (loggedInUsername != null)
                    {
                        var prodavac = context.Prodavac.FirstOrDefault(p => p.Korisnici.Username == loggedInUsername);
                        if (prodavac != null)
                        {
                            // Otvaranje prozora za promenu statusa samo ako je prodavac ulogovan
                            ChangeOrderStatus changeStatusWindow = new ChangeOrderStatus(selectedOrder);
                            changeStatusWindow.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Kupci nemaju pristup promeni statusa narudžbine.");
                        }
                    }
                }
            }
        }
        
    }
}
