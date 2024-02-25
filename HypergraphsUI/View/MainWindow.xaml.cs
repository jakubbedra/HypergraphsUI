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
using HypergraphsUI.ViewModel;

namespace HypergraphsUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            ((MainWindowViewModel)DataContext).RefreshUIRequested += MainWindow_RefreshUIRequested;
        }
        
        private void MainWindow_RefreshUIRequested(object sender, EventArgs e)
        {
            // Force a refresh of the UI
            InvalidateVisual();
        }
        
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dataContext = ((FrameworkElement)sender).DataContext as MainWindowViewModel;
            if (dataContext != null)
            {
                // Call the command
                dataContext.ChangeSizesListCommand.Execute(null); // Optionally pass command parameter
            }

        }

    }
}