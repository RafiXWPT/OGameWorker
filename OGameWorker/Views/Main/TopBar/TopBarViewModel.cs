using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using OGameWorker.Code;
using OGameWorker.Views.Main.Resources;
using ReactiveUI;
using Worker.Objects;
using Worker.Objects.Galaxy;

namespace OGameWorker.Views.Main.TopBar
{
    public class TopBarViewModel : OGameWorkerBaseViewModel
    {
        private readonly ResourcesViewModel _resourcesViewModel;

        public ObservableCollection<Planet> PlayerPlanets { get; set; } = new ObservableCollection<Planet>();
        public bool ReadOnly { get; set; } = true;
        private Planet _selectedPlanet;
        public Planet SelectedPlanet
        {
            get => _selectedPlanet;
            set
            {
                if (value == null || value == _selectedPlanet)
                    return;

                _selectedPlanet = value;
                ObjectContainer.Instance.CurrentSelectedPlanet = _selectedPlanet;
                _resourcesViewModel.Synchronize(_selectedPlanet);
            }
        }

        public TopBarViewModel()
        {

        }

        public TopBarViewModel(ResourcesViewModel resourcesViewModel)
        {
            _resourcesViewModel = resourcesViewModel;
        }
       
        public void UpdatePlayerPlanets(List<Planet> playerPlanets)
        {
            RunOnDispatcher(() =>
            {
                PlayerPlanets.Clear();
                foreach (var planet in playerPlanets)
                {
                    PlayerPlanets.Add(planet);
                }
            });
        }
    }
}
