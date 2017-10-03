using System.Windows;

namespace OGameWorker.Views.LogIn
{
    /// <summary>
    ///     Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        public LogInWindow()
        {
            ViewModel = new LogInViewModel(this);
            InitializeComponent();
            DataContext = ViewModel;
        }

        private LogInViewModel ViewModel { get; }
    }
}