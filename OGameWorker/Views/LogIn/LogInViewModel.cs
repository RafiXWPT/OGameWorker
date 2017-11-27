using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using OGameWorker.Code;
using OGameWorker.Code.ConfigurationHelper;
using OGameWorker.Views.Main;
using OGameWorker.Views.Main.Galaxy.PlanetInfo;
using ReactiveUI;
using Worker.HttpModule.Clients;
using Worker.HttpModule.Clients.OGameClient;

namespace OGameWorker.Views.LogIn
{
    public class LogInViewModel : ReactiveObject
    {
        private readonly Window _logInWindow;
        private string _password;
        private string _server;
        private string _username;

        public LogInViewModel() { }

        public LogInViewModel(Window logInWindow)
        {
            _logInWindow = logInWindow;
            LogIn = ReactiveCommand.CreateFromTask(t => LogInTask());

            Server = OGameConfigurationReader.ReadValue("SAVED_SERVER");
            Username = OGameConfigurationReader.ReadValue("SAVED_USERNAME");
            Password = OGameConfigurationReader.ReadValue("SAVED_PASSWORD");
        }

        public string Username
        {
            get => _username;
            set => this.RaiseAndSetIfChanged(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        public string Server
        {
            get => _server;
            set => this.RaiseAndSetIfChanged(ref _server, value);
        }

        public ReactiveCommand LogIn { get; protected set; }

        private async Task LogInTask()
        {
            var server = Server;
            var username = Username;
            var password = Password;
            var client = new OGameHttpClient(server, username, password);
            var result = await client.LogIn(username, password);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                OGameConfigurationReader.SaveValue("SAVED_SERVER", server);
                OGameConfigurationReader.SaveValue("SAVED_USERNAME", username);
                OGameConfigurationReader.SaveValue("SAVED_PASSWORD", password);
                new MainWindow(client).Show();
                _logInWindow.Close();
            }
        }
    }
}