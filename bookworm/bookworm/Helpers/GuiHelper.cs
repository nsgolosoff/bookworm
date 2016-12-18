using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows;
using bookworm.Entities;
using System.Net.Mail;

namespace bookworm.Helpers
{
  public  class GuiHelper
    {
        private const string YANDEX_USERNAME = "otecisyn2014"; //for sending email from address username@yandex.ru
        private const string YANDEX_PASSWORD = "19980217";
        private readonly string _email = "nik-golosov@yandex.ru";

        private static GuiHelper guiHelper;
        public static GuiHelper GetGuiHelper()
        {
            if (guiHelper == null)
            {
                guiHelper = new GuiHelper();
            }
            return guiHelper;
        }

        private GuiHelper() { }


        public ProgressBar ProgressBar = null;
        public DataGrid DataGrid = null;

        private Dictionary<string, bool> _progressBarStatues = new Dictionary<string, bool>();


       

        private void UpdateProgressBar()
        {
            if (ProgressBar == null) return;
            foreach (var status in _progressBarStatues)
            {
                if (status.Value)
                {
                    ProgressBar.IsIndeterminate = true;
                    return;
                }
            }
            ProgressBar.IsIndeterminate = false;
        }
        private void SafeStatusChange(string processName, bool value)
        {
            _progressBarStatues[processName] = value;
            UpdateProgressBar();
        }
        public async void LoadBooksData(Func<Book, bool> condition = null)
        {
            try
            {
                var processName = DateTime.Today.ToBinary().ToString();
                SafeStatusChange(processName, true);
                var task = Task.Factory.StartNew(() => DataBaseHelper.GetBooks(condition));
                await task;
                SafeStatusChange(processName, false);
                if (DataGrid != null)
                {
                    DataGrid.ItemsSource = task.Result;
                }
            }
            catch
            {
                MessageBox.Show("Can't load data from DataBase");
            }
        }

        public async void OpenFildeDialogAndUploadData()
        {
            var processName = DateTime.Today.ToBinary().ToString();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    SafeStatusChange(processName, true);
                    var task = Task.Factory.StartNew(() => DataBaseHelper.LoadDataFromCSV(openFileDialog.FileName));
                    await task;
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
                finally
                {
                    SafeStatusChange(processName, false);
                }
                LoadBooksData();
            }
        }

        public async void RegisterOrder(Book book, string name, string phone, string address)
        {
            var processName = DateTime.Today.ToBinary().ToString();
            SafeStatusChange(processName, true);
            var task = Task.Factory.StartNew(() => DataBaseHelper.RegisterOrder(book, name, phone, address));
            await task;
            SafeStatusChange(processName, false);

            SmtpClient emailClient = new SmtpClient
            {
                Port = 587,
                Host = "smtp.yandex.ru",
                EnableSsl = true,
                Timeout = 20000, //20 seconds
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(YANDEX_USERNAME, YANDEX_PASSWORD)
            };

            string messageText = $"A new order occured. {Environment.NewLine + Environment.NewLine} Book ID:{book.Id} {Environment.NewLine} Book Author:{book.Autor} {Environment.NewLine} Name: {name} {Environment.NewLine} Phone: {phone} {Environment.NewLine} Address: {address}";

            emailClient.Send(new MailMessage($"{YANDEX_USERNAME}@yandex.ru", _email, "New order", messageText)
            {
                BodyEncoding = Encoding.UTF8,
                DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
            });



            MessageBox.Show(string.Format("Order number is {0}", task.Result));

            LoadBooksData();
        }
    }
}
