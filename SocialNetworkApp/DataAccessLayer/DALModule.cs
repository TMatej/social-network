using Autofac;
using System.Configuration;
namespace DataAccessLayer
{

    public class DALModule : Module
    {

        protected override void Load(ContainerBuilder containerBuilder)
        {
            var connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            containerBuilder.Register((ctx) => new SocialNetworkDBContext(connectionString));
        }
    }
}
