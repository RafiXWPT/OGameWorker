using System.Runtime.Remoting.Channels;
using System.Linq;
using Worker.Objects;
using Worker.HttpModule.Clients;
using System.Windows;
using System.Threading.Tasks;
using System.Timers;
using System;

namespace OGameWorker.Views.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly OGameHttpClient client;
        private readonly Timer UpdateResourcesTimer = new Timer(1000);
        private bool _busy;

        public MainWindow(OGameHttpClient client)
        {
            InitializeComponent();
            UpdateResourcesTimer.Elapsed += async (sender, e) => await UpdateResources();
            this.client = client;
            UpdateResourcesTimer.Start();
        }

        private async Task UpdateResources()
        {
            if (_busy)
                return;

            _busy = true;
            await client.RefreshObjectContainer();
            SafeControlUpdate(() =>
            {
                Metal.Text = ObjectContainer.Instance.PlayerPlanets.First().Metal.Amount.ToString();
                Crystal.Text = ObjectContainer.Instance.PlayerPlanets.First().Crystal.Amount.ToString();
                Deuterium.Text = ObjectContainer.Instance.PlayerPlanets.First().Deuterium.Amount.ToString();
            });

            _busy = false;
        }

        private void SafeControlUpdate(Action action)
        {
            Dispatcher.Invoke(action);
        }
    }
}
