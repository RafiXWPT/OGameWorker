using Worker.Objects.Resources;

namespace Worker.Objects.Galaxy
{
    public struct Position
    {
        public int Galaxy { get; set; }
        public int System { get; set; }
        public int Planet { get; set; }

        public override string ToString() => $"[{Galaxy}:{System}:{Planet}]";
    }

    public abstract class GalaxyObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Position Position { get; set; }

        public Metal Metal { get; set; }
        public Crystal Crystal { get; set; }
        public Deuterium Deuterium { get; set; }
        public Energy Energy { get; set; }
        public int TotalResources => Metal.Amount + Crystal.Amount + Deuterium.Amount;

        protected GalaxyObject(int id, string name, Position position) : this(id, name, position, new Metal(0), new Crystal(0), new Deuterium(0)) { }

        protected GalaxyObject(int id, string name, Position position, Metal metal, Crystal crystal, Deuterium deuterium)
        {
            Id = id;
            Name = name;
            Position = position;
            Metal = metal;
            Crystal = crystal;
            Deuterium = deuterium;
        }
    }
}
