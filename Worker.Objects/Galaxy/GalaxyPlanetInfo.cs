using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker.Objects.Galaxy
{
    public class GalaxyPlanetInfo
    {
        public PlanetType PlanetType { get; set; }
        public string PlanetName { get; set; }
        public string PlayerName { get; set; }
        public Planet.PlanetPosition PlanetPosition { get; set; }
    }
}
