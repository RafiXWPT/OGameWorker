using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using OGameWorker.Code;
using Worker.Objects.Galaxy.Planet;
using Worker.Objects.Structures;

namespace OGameWorker.Views.Main.Galaxy.PlanetInfo.FleetInfo
{
    public class GalaxyPlanetFleetInfoViewModel
    {
        private readonly EnemyPlanet _planet;
        public bool HasData => _planet.Ships.Any();

        public BitmapImage SmallCargoImage => ImageHelper.SmallCargo;
        public int SmallCargoAmount => _planet.Ships.FirstOrDefault(s => s.Type == ShipType.SmallCargo)?.Quantity ?? 0;

        public BitmapImage LargeCargoImage => ImageHelper.LargeCargo;
        public int LargeCargoAmount => _planet.Ships.FirstOrDefault(s => s.Type == ShipType.LargeCargo)?.Quantity ?? 0;

        public BitmapImage LightFighterImage => ImageHelper.LightFighter;
        public int LightFighterAmount => _planet.Ships.FirstOrDefault(s => s.Type == ShipType.LightFighter)?.Quantity ?? 0;

        public BitmapImage HeavyFighterImage => ImageHelper.HeavyFighter;
        public int HeavyFighterAmount => _planet.Ships.FirstOrDefault(s => s.Type == ShipType.HeavyFighter)?.Quantity ?? 0;

        public BitmapImage CruiserImage => ImageHelper.Cruiser;
        public int CruiserAmount => _planet.Ships.FirstOrDefault(s => s.Type == ShipType.Cruiser)?.Quantity ?? 0;

        public BitmapImage BattleshipImage => ImageHelper.Battleship;
        public int BattleshipAmount => _planet.Ships.FirstOrDefault(s => s.Type == ShipType.Battleship)?.Quantity ?? 0;

        public BitmapImage BattlecruiserImage => ImageHelper.Battlecruiser;
        public int BattlecruiserAmount => _planet.Ships.FirstOrDefault(s => s.Type == ShipType.Battlecruiser)?.Quantity ?? 0;

        public BitmapImage BomberImage => ImageHelper.Bomber;
        public int BomberAmount => _planet.Ships.FirstOrDefault(s => s.Type == ShipType.Bomber)?.Quantity ?? 0;

        public BitmapImage DestroyerImage => ImageHelper.Destroyer;
        public int DestroyerAmount => _planet.Ships.FirstOrDefault(s => s.Type == ShipType.Destroyer)?.Quantity ?? 0;

        public BitmapImage DeathstarImage => ImageHelper.Deathstar;
        public int DeathstarAmount => _planet.Ships.FirstOrDefault(s => s.Type == ShipType.Deathstar)?.Quantity ?? 0;

        public BitmapImage RecyclerImage => ImageHelper.Recycler;
        public int RecyclerAmount => _planet.Ships.FirstOrDefault(s => s.Type == ShipType.Recycler)?.Quantity ?? 0;

        public BitmapImage ColonyShipImage => ImageHelper.ColonyShip;
        public int ColonyShipAmount => _planet.Ships.FirstOrDefault(s => s.Type == ShipType.ColonyShip)?.Quantity ?? 0;

        public BitmapImage EspionageProbeImage => ImageHelper.EspionageProbe;
        public int EspionageProbeAmount => _planet.Ships.FirstOrDefault(s => s.Type == ShipType.EspionageProbe)?.Quantity ?? 0;

        public BitmapImage SolarSatelliteImage => ImageHelper.SolarSatellite;
        public int SolarSatelliteAmount => _planet.Ships.FirstOrDefault(s => s.Type == ShipType.SolarSatellite)?.Quantity ?? 0;

        public GalaxyPlanetFleetInfoViewModel() { }

        public GalaxyPlanetFleetInfoViewModel(EnemyPlanet planet)
        {
            _planet = planet;
        }
    }
}
