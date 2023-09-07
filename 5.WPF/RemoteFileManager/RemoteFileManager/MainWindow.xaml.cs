using RemoteFileManager.ViewModels;
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

namespace RemoteFileManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ItemsViewModel itemsViewModel;
        public MainWindow()
        {
            InitializeComponent();
            itemsViewModel = new ItemsViewModel();
            Init();
        }

        private void Init()
        {
            CbDirectories.ItemsSource = itemsViewModel.Directories;
            LbItems.ItemsSource = itemsViewModel.Items;
        }

        private void CbDirectories_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void CbDirectories_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void LbItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnUpload_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnUpload_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
