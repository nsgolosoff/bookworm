using bookworm.Entities;
using MahApps.Metro.Controls;
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
namespace bookworm
{
    /// <summary>
    /// Interaction logic for NewOrder.xaml
    /// </summary>
    public partial class NewOrderWindow : MetroWindow
    {
        public NewOrderWindow()
        {
            InitializeComponent();
        }

        public bool IsSuccess { get; private set; }
        public string ClientName { get; private set; }
        public string Phone { get; private set; }
        public string Address { get; private set; }

        private void button_order_Click(object sender, RoutedEventArgs e)
        {
            if (textBox_client.Text.Trim().Length < 1 || textBox_phone.Text.Trim().Length < 1 || textBox_address.Text.Trim().Length < 1)
            {
                MessageBox.Show("Fill all data");
            }
            else
            {
                ClientName = textBox_client.Text.Trim();
                Phone = textBox_phone.Text.Trim();
                Address = textBox_address.Text.Trim();
                IsSuccess = true;
                Close();
            }
        }
    }
}
