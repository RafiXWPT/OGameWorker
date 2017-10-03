using System.Net;
using System.Threading.Tasks;
using System.Windows;
using OGameWorker.Views.Main;
using Worker.HttpModule.Clients;

namespace OGameWorker.Views.LogIn
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        private LogInViewModel ViewModel { get; }
        public LogInWindow()
        {
            ViewModel = new LogInViewModel(this);
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
