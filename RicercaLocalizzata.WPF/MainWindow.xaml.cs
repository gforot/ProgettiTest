using RicercaLocalizzata.Data;
using RicercaLocalizzata.WPF.ViewModels;
using System.Windows;

namespace RicercaLocalizzata.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowsViewModel(MyItemDataProvider.GetSampleData());
        }
    }
}
