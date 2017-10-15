using System;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using OGameWorker.Views.Main.Resources;
using Worker.HttpModule.Clients;
using Worker.HttpModule.Clients.OGameClient;
using Worker.Objects;

namespace OGameWorker.Views.Main
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel ViewModel { get; }

        public MainWindow(OGameHttpClient client)
        {
            ViewModel = new MainWindowViewModel(client);
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}