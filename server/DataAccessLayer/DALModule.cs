using Autofac;
namespace DataAccessLayer
{

    public class DALModule : Module
    {
        public string ConnectionString { get; set; }
        public bool SeedData { get; set; }
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.Register((ctx) => new SocialNetworkDBContext(connectionString: ConnectionString, seedData: SeedData));
        }
    }
}
