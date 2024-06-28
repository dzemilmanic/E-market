using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Data.Entity;

namespace Project
{
    /// <summary>
    /// Interaction logic for SendMessage.xaml
    /// </summary>
    public partial class SendMessage : Page
    {
        public SendMessage()
        {
            InitializeComponent();
            LoadMessages();
            LoadRecipients();
        }
        private sales_systemEntities2 context = new sales_systemEntities2();
        private ObservableCollection<Poruka> poruke;
        private void LoadMessages()
        {
            string loggedInUsername = Login.LoggedIn;
            var currentUser = context.Korisnici.SingleOrDefault(u => u.Username == loggedInUsername);

            if (currentUser != null)
            {
                poruke = new ObservableCollection<Poruka>(context.Poruka
                    .Where(p => p.PosiljalacID == currentUser.KorisnikID || p.PrimalacID == currentUser.KorisnikID));
                lvMessages.ItemsSource = poruke;
            }
        }

        private void LoadRecipients()
        {
            string loggedInUsername = Login.LoggedIn;
            var currentUser = context.Korisnici.SingleOrDefault(u => u.Username == loggedInUsername);

            if (currentUser != null)
            {
                bool isCurrentUserCustomer = context.Kupac.Any(k => k.KorisnikID == currentUser.KorisnikID);

                if (isCurrentUserCustomer)
                {
                    cbRecipients.ItemsSource = context.Korisnici.Where(u => context.Prodavac.Any(p => p.KorisnikID == u.KorisnikID)).ToList();
                }
                else
                {
                    cbRecipients.ItemsSource = context.Korisnici.Where(u => context.Kupac.Any(k => k.KorisnikID == u.KorisnikID)).ToList();
                }
            }
        }

        private void btnSendMessage_Click(object sender, RoutedEventArgs e)
        {
            if (cbRecipients.SelectedItem != null && !string.IsNullOrWhiteSpace(tbMessageContent.Text))
            {
                int recipientID = (int)cbRecipients.SelectedValue;
                sendMessage(tbMessageContent.Text.Trim(), recipientID);
                tbMessageContent.Clear();
                LoadMessages();
            }
            else
            {
                MessageBox.Show("Morate odabrati primaoca i uneti sadržaj poruke.");
            }
        }

        private void sendMessage(string content, int recipientID)
        {
            string loggedInUsername = Login.LoggedIn;

            var sender = context.Korisnici.SingleOrDefault(u => u.Username == loggedInUsername);
            if (sender == null)
            {
                MessageBox.Show("Korisnik nije pronađen.");
                return;
            }

            var recipient = context.Korisnici.SingleOrDefault(u => u.KorisnikID == recipientID);
            if (recipient == null)
            {
                MessageBox.Show("Primalac nije pronađen.");
                return;
            }

            bool isSenderCustomer = context.Kupac.Any(k => k.KorisnikID == sender.KorisnikID);
            bool isRecipientCustomer = context.Kupac.Any(k => k.KorisnikID == recipient.KorisnikID);

            if ((isSenderCustomer && isRecipientCustomer) || (!isSenderCustomer && !isRecipientCustomer))
            {
                MessageBox.Show("Kupci mogu slati poruke samo prodavcima, a prodavci samo kupcima.");
                return;
            }

            Poruka newMessage = new Poruka
            {
                PosiljalacID = sender.KorisnikID,
                PrimalacID = recipient.KorisnikID,
                PosiljalacIme = sender?.Kupac != null ? (sender.Kupac.FizickoLice != null ? sender.Kupac.FizickoLice.Ime : sender.Kupac.Firma != null ? sender.Kupac.Firma.Naziv : null) :
                     sender?.Prodavac != null ? (sender.Prodavac.FizickoLice != null ? sender.Prodavac.FizickoLice.Ime : sender.Prodavac.Firma != null ? sender.Prodavac.Firma.Naziv : null) :
                     null,
                PrimalacIme = recipient?.Kupac != null ? (recipient.Kupac.FizickoLice != null ? recipient.Kupac.FizickoLice.Ime : recipient.Kupac.Firma != null ? recipient.Kupac.Firma.Naziv : null) :
                      recipient?.Prodavac != null ? (recipient.Prodavac.FizickoLice != null ? recipient.Prodavac.FizickoLice.Ime : recipient.Prodavac.Firma != null ? recipient.Prodavac.Firma.Naziv : null) :
                      null,
                Sadrzaj = content,
                DatumSlanja = DateTime.Now
            };

            context.Poruka.Add(newMessage);
            context.SaveChanges();

            MessageBox.Show("Poruka je uspešno poslata.");
        }
        private void btnSendEmail_Click(object sender, RoutedEventArgs e)
        {
            var composeWindow = new SendMail();
            composeWindow.Show();
        }

    }
}
