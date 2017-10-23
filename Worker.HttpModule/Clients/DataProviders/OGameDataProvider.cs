using Worker.HttpModule.Clients.DataProviders.Providers;

namespace Worker.HttpModule.Clients.DataProviders
{
    public class OGameDataProvider
    {
        public MessagesProvider MessagesProvider { get; } = new MessagesProvider();
        public MissionsProvider MissionsProvider { get; } = new MissionsProvider();
        public PlanetProvider PlanetProvider { get; } = new PlanetProvider();
        public FleetMovementProvider FleetMovementProvider { get; } = new FleetMovementProvider();
        public BuildingsProvider BuildingsProvider { get; } = new BuildingsProvider();
        public GalaxyProvider GalaxyProvider { get; } = new GalaxyProvider();
        public ResourcesProvider ResourcesProvider { get; } = new ResourcesProvider();
        public FleetProvider FleetProvider { get; } = new FleetProvider();
        public DefenseProvider DefenseProvider { get; } = new DefenseProvider();
        public TechnologiesProvider TechnologiesProvider { get; } = new TechnologiesProvider();
    }
}