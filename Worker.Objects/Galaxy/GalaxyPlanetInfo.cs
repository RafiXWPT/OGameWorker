using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Worker.Objects.Resources;
using Worker.Objects.Structures.Buildings;
using Worker.Objects.Structures.Ships;

namespace Worker.Objects.Galaxy
{
    public enum ScanStatus
    {
        NotScanned,
        Scanning,
        Scanned
    }
    public class GalaxyPlanetInfo : INotifyPropertyChanged
    {
        public int PlanetId { get; set; }
        public PlanetType PlanetType { get; set; }
        public string PlanetName { get; set; }
        public string PlayerName { get; set; }
        public Planet.PlanetPosition PlanetPosition { get; set; }
        public Metal Metal { get; set; } = new Metal(0);
        public Crystal Crystal { get; set; } = new Crystal(0);
        public Deuterium Deuterium { get; set; } = new Deuterium(0);
        public List<ShipBase> Fleet { get; set; } = new List<ShipBase>();
        public List<BuildingBase> Buildings { get; set; } = new List<BuildingBase>();

        public string PlanetResources => $"M:{Metal.Amount} C:{Crystal.Amount} D:{Deuterium.Amount}";

        private ScanStatus _scanStatus;
        public ScanStatus ScanStatus
        {
            get => _scanStatus;
            set
            {
                _scanStatus = value;
                OnPropertyChanged();
                OnPropertyChanged("PlanetResources");
            }
        }

        public bool CanExecuteEspionage => PlanetType != PlanetType.Empty && PlanetType != PlanetType.Self;
        public bool CanExecuteAttack => PlanetType != PlanetType.Empty && PlanetType != PlanetType.Self;
        public bool CanExecuteTransport => PlanetType != PlanetType.Empty;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
