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
using bookworm.Helpers;
using bookworm.Entities;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace bookworm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            _guiHelper = GuiHelper.GetGuiHelper();
            _guiHelper.ProgressBar = progreesbar_main;
            _guiHelper.DataGrid = dataGrid_main;
        }

        private GuiHelper _guiHelper;

        private void menuItem_books_loadFromCSV_Click(object sender, RoutedEventArgs e)
        {
            _guiHelper.OpenFildeDialogAndUploadData();
        }

        private void window_main_Loaded(object sender, RoutedEventArgs e)
        {
            _guiHelper.LoadBooksData();
        }

        string _searchText = string.Empty;

        private void textBox_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = sender as TextBox;
            _searchText = t.Text.ToString();
            dataGrid_main.Items.Filter = FilterData;
            dataGrid_main.Items.Refresh();
        }

        private bool FilterData(object item)
        {
            var value = (Book)item;
            if (value == null || value.Id < 0)
                return false;
            return value.Title.ToLower().Contains(_searchText.ToLower())
                || value.ISBN.ToLower().Contains(_searchText.ToLower())
                || value.Autor.ToLower().ToString().Contains(_searchText.ToLower())
                || string.Format("{0} {1} {2}", value.Autor, value.Title, value.ISBN).ToLower().Contains(_searchText.ToLower());
        }

        private async void button_book_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var select = (Book)dataGrid_main.SelectedItem;
                if (select == null)
                {
                    throw new Exception("Select a book.");
                }
                if (select.Count < 1)
                {
                    throw new Exception("The item is out of stock.");
                }
                var newOrderWindow = new NewOrderWindow();
                newOrderWindow.ShowDialog();
                if (newOrderWindow.IsSuccess)
                {
                    _guiHelper.RegisterOrder(select, newOrderWindow.ClientName, newOrderWindow.Phone, newOrderWindow.Address);
                }
            }
            catch (Exception error)
            {
                await this.ShowMessageAsync("Error", error.Message);
            }
        }


    }
}
