using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Net.Mail;
using System.Windows.Controls;
using System.Net;

namespace sendMail
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker bgWorker = new BackgroundWorker();
        private int count;
        private string mailSender;
        private string message;
    
        public MainWindow()
        {
            InitializeComponent();

            if (!Config.isExistXMLConfig()) Config.SaveConfig();
            else Config.ReadConfig();

            mailTo.Text = Config.toSMTPmail;

            bgWorker.WorkerReportsProgress = true;
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            bgWorker.ProgressChanged += new ProgressChangedEventHandler(bgWorker_ProgressChanged);
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            btn.Content = "Stop";
            Config.toSMTPmail = mailTo.Text;
            Config.SaveConfig();

            Label.Content = "";
            int j = 0;
            try
            {
                j = int.Parse(countMail.Text);
            }
            catch
            {
                countMail.Text = "1 -" + Int32.MaxValue;
            }

                if (mailTo.Text != "To:" || mailTo.Text != "")
                {
                count = int.Parse(countMail.Text);
                progressBar.Maximum = count;
                progressBar.Minimum = 0;
                mailSender = mailTo.Text;
                message = Message.Text;
                bgWorker.RunWorkerAsync();
                }
        }

        void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
                for (int i = count; i > 0; i--)
                {
                    SmtpClient client = new SmtpClient();
                    client.Host = Config.serverSMTP;
                    client.Port = 25;
                    client.UseDefaultCredentials = false;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.EnableSsl = false;
                    client.Credentials = new NetworkCredential(Config.UserSMTP, Config.PassSMTP);
                    MailMessage msg = new MailMessage(Config.fromSMTPmail, Config.toSMTPmail, "Test", message);
                    client.Send(msg);
                    bgWorker.ReportProgress(i - 1);
                }
        }
        void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // The progress percentage is a property of e          
            progressBar.Value = e.ProgressPercentage;
            countMail.Text = e.ProgressPercentage.ToString();
            btn.IsEnabled = true;
            if (e.ProgressPercentage == 0) btn.Content = "Send";
        }
        private void mailTo_GotFocus(object sender, RoutedEventArgs e)
        {
            if (mailTo.Text == "To:") mailTo.Text = "";          
        }

        private void Message_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Message.Text == "Msg:") Message.Text = "";
        }

        private void countMail_GotFocus(object sender, RoutedEventArgs e)
        {
            countMail.Text = "";
        }
    }
}
