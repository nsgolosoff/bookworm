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
         //   _guiHelper.OpenFildeDialogAndUploadData();
        }

        private void window_main_Loaded(object sender, RoutedEventArgs e)
        {
           // _guiHelper.LoadBooksData();
        }

        string _searchText = string.Empty;

        private void textBox_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private bool FilterData(object item)
        {
            return true;
        }

        private async void button_book_Click(object sender, RoutedEventArgs e)
        {
            
           
        }


    }
}
