using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Worker.Objects.Galaxy;
using Worker.Objects.Helpers;

namespace Worker.Objects.Research
{
    public enum TechnologyType
    {
        EnergyTechnology = 113,
        LaserTechnology = 120,
        IonTechnology = 121,
        HyperspaceTechnology = 114,
        PlasmaTechnology = 122,
        EspionageTechnology = 106,
        ComputerTechnology = 108,
        Astrophysics = 124,
        IntergalacticResearchNetwork = 123,
        GravitonTechnology = 199,
        WeaponTechnology = 109,
        ShieldingTechnology = 110,
        ArmourTechnology = 111,
        CombusionDrive = 115,
        ImpulseDrive = 117,
        HyperspaceDrive = 118
    }

    public static class BasicTechnologies
    {
        public static List<TechnologyType> List => new List<TechnologyType>
        {
            TechnologyType.EnergyTechnology,
            TechnologyType.LaserTechnology,
            TechnologyType.IonTechnology,
            TechnologyType.HyperspaceTechnology,
            TechnologyType.PlasmaTechnology
        };
    }

    public static class AdvancedTechnologies
    {
        public static List<TechnologyType> List => new List<TechnologyType>
        {
            TechnologyType.EspionageTechnology,
            TechnologyType.ComputerTechnology,
            TechnologyType.Astrophysics,
            TechnologyType.IntergalacticResearchNetwork,
            TechnologyType.GravitonTechnology
        };
    }

    public static class DriveTechnologies
    {
        public static List<TechnologyType> List => new List<TechnologyType>
        {
            TechnologyType.CombusionDrive,
            TechnologyType.ImpulseDrive,
            TechnologyType.HyperspaceDrive
        };
    }

    public static class CombatTechnologies
    {
        public static List<TechnologyType> List => new List<TechnologyType>
        {
            TechnologyType.WeaponTechnology,
            TechnologyType.ShieldingTechnology,
            TechnologyType.ArmourTechnology
        };
    }

    public abstract class TechnologyBase
    {
        public abstract TechnologyType TechnologyType { get; }
        public abstract int BaseMetalCost { get; }
        public abstract int BaseCrystalCost { get; }
        public abstract int BaseDeuteriumCost { get; }

        public int UniverseSpeed { get; } = Convert.ToInt32(ConfigurationManager.AppSettings["UNIVERSE_SPEED"]);
        public double CostIncreaseFactor => 2.0;
        public int CurrentLevel { get; }
        public int MetalCost => (int)(BaseMetalCost * Math.Pow(CostIncreaseFactor, CurrentLevel));
        public int CrystalCost => (int)(BaseCrystalCost * Math.Pow(CostIncreaseFactor, CurrentLevel));
        public int DeuteriumCost => (int)(BaseDeuteriumCost * Math.Pow(CostIncreaseFactor, CurrentLevel));
        public Planet BelongsTo { get; set; }

        public bool TechReached { get; set; }
        private bool _canBuild;

        public bool CanBuild
        {
            get => TechReached && _canBuild;
            set => _canBuild = value;
        }

        public TimeSpan UpgradeTimeDuration => ObjectContainer.Instance.PlayerBuildings.Any()
            ? TimeSpan.FromHours((double)(MetalCost + CrystalCost) / (UniverseSpeed * 1000 * (1 + PlanetCoreBuildingsHelper.GetPlanetResearchLabolatory(BelongsTo).CurrentLevel)))
            : TimeSpan.MaxValue;

        protected TechnologyBase(Planet belongsTo, int currentLevel, bool techReached, bool canBuild)
        {
            BelongsTo = belongsTo;
            CurrentLevel = currentLevel;
            TechReached = techReached;
            _canBuild = canBuild;
        }
    }
}
