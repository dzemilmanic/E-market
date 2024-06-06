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
        private void LoadOrders()
        {
            using (var context = new sales_systemEntities2())
            {
                Narudzbine = new ObservableCollection<Narudzbina>(context.Narudzbina);
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
