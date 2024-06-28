using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
    /// Interaction logic for SendMail.xaml
    /// </summary>
    public partial class SendMail : Window
    {
        public SendMail()
        {
            InitializeComponent();
        }
        private void SendEmail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string recipientEmail = tbRecipientEmail.Text.Trim();
                string subject = tbSubject.Text.Trim();
                string body = tbBody.Text.Trim();

                if (string.IsNullOrWhiteSpace(recipientEmail) || string.IsNullOrWhiteSpace(subject) || string.IsNullOrWhiteSpace(body))
                {
                    MessageBox.Show("Popunite formu");
                    return;
                }

                string senderEmail = "saljemporuku@outlook.com";
                string senderPassword = "Outlook1";

                using (var client = new SmtpClient("smtp-mail.outlook.com", 587))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                    client.EnableSsl = true;

                    var mailMessage = new MailMessage(senderEmail, recipientEmail, subject, body);
                    client.Send(mailMessage);
                }

                MessageBox.Show("Email je poslat");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greska pri slanju. Greska: {ex.Message}");
            }
        }
    }
}
