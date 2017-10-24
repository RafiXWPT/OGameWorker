using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Worker.Objects.Structures.Buildings;
using Worker.Objects.Structures.Defenses;
using Worker.Objects.Structures.Ships;

namespace Worker.Objects.Galaxy
{
    public enum ScanStatus
    {
        NotScanned,
        Scanning,
        Scanned
    }

    public class EnemyPlanet : Planet, INotifyPropertyChanged
    {
        public string PlayerName { get; set; }

        private List<BuildingBase> _buildings;
        private List<ShipBase> _ships;
        private List<DefenseBase> _defenses;
        public override List<BuildingBase> Buildings => _buildings;
        public override List<ShipBase> Ships => _ships;
        public override List<DefenseBase> Defenses => _defenses;

        public bool CanExecuteEspionage => Type != PlanetType.Empty && Type != PlanetType.Self;
        public bool CanExecuteAttack => Type != PlanetType.Empty && Type != PlanetType.Self;
        public bool CanExecuteTransport => Type != PlanetType.Empty;

        public string PlanetResources => $"M:{Metal.Amount} C:{Crystal.Amount} D:{Deuterium.Amount}";

        private ScanStatus _scanStatus;
        public ScanStatus ScanStatus
        {
            get => _scanStatus;
            set
            {
                _scanStatus = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PlanetResources));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public EnemyPlanet(int id, string name, Position position) : base(id, name, position)
        {
            Type = PlanetType.EnemyUnknown;
        }

        public void Update(List<BuildingBase> buildings, List<ShipBase> ships, List<DefenseBase> defenses)
        {
            _buildings = buildings;
            _ships = ships;
            _defenses = defenses;
        }
    }
}
