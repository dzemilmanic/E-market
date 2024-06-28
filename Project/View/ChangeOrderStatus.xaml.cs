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
    /// Interaction logic for ChangeOrderStatus.xaml
    /// </summary>
    public partial class ChangeOrderStatus : Window
    {
        private Narudzbina selectedOrder;
        public ChangeOrderStatus(Narudzbina order)
        {
            InitializeComponent();
            selectedOrder = order;
        }

        private void changeOrderStatus(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)cbStatus.SelectedItem;
            string newStatus = selectedItem.Content.ToString();

            using (var context = new sales_systemEntities2())
            {
                var orderToUpdate = context.Narudzbina.FirstOrDefault(o => o.NarudzbinaID == selectedOrder.NarudzbinaID);
                if (orderToUpdate != null)
                {
                    orderToUpdate.Status = newStatus;
                    context.SaveChanges();
                    MessageBox.Show("Status narudžbine je promenjen");
                    //ne moze Orders.LoadOrders() jer nije static
                    //pronalazenje Orders pagea preko MainWindowa
                    Orders ordersWindow = null;

                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window is MainWindow main)
                        {
                            Frame frame = main.FindName("showPage") as Frame;

                            if (frame != null)
                            {
                                ordersWindow = frame.Content as Orders;
                                break;
                            }
                        }
                    }
                    if (ordersWindow != null)
                    {
                        ordersWindow.LoadOrders();
                    }
                    else
                    {
                        MessageBox.Show("Prozor Orders nije pronađen.");
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Narudžbina nije pronađena.");
                }
            }
        }
    }
}
