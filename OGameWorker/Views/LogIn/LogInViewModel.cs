using System.Net;
using System.Threading.Tasks;
using System.Windows;
using OGameWorker.Views.Main;
using ReactiveUI;
using Worker.HttpModule.Clients;

namespace OGameWorker.Views.LogIn
{
    public class LogInViewModel : ReactiveObject
    {
        private readonly Window _logInWindow;
        private string _username;
        private string _password;
        private string _server;

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

        public LogInViewModel() { }

        public LogInViewModel(Window logInWindow)
        {
            _logInWindow = logInWindow;
            LogIn = ReactiveCommand.CreateFromTask(t => LogInTask());
        }

        private async Task LogInTask()
        {
            var server = Server;
            var username = Username;
            var password = Password;
            var client = new OGameHttpClient(server);
            var result = await client.LogIn(username, password);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                new MainWindow(client).Show();
                _logInWindow.Close();
            }
        }
    }
}
