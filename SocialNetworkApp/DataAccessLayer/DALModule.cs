using Autofac;
using System.Configuration;
namespace DataAccessLayer
{

    public class DALModule : Module
    {

        protected override void Load(ContainerBuilder containerBuilder)
        {
            //var connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            /* connection string is created fro menv variables in DBContext */
            containerBuilder.Register((ctx) => new SocialNetworkDBContext(seedData: true));
        }
    }
}
