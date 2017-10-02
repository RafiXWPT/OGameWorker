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
        public LogInWindow()
        {
            InitializeComponent();
        }

        private async void LogInButton_OnClick(object sender, RoutedEventArgs e)
        {
            var server = Server.Text;
            var username = Username.Text;
            var password = Password.Text;
            var client = new OGameHttpClient(server);
            var result = await client.LogIn(username, password);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                new MainWindow(client).Show();
                this.Close();
            }
        }
    }
}
