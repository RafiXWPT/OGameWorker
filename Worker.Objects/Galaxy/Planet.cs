using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Objects.Resources;

namespace Worker.Objects.Galaxy
{
    public class Planet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Metal Metal { get; set; }
        public Crystal Crystal { get; set; }
        public Deuterium Deuterium { get; set; }

        public Planet(int id, string name) : this(id, name, new Metal(0), new Crystal(0), new Deuterium(0)) { }

        public Planet(int id, string name, Metal metal, Crystal crystal, Deuterium deuterium)
        {
            Id = id;
            Name = name;
            Metal = metal;
            Crystal = crystal;
            Deuterium = deuterium;
        }
    }
}
