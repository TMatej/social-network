using Autofac;
using DataAccessLayer;
using Infrastructure.EFCore.Query;
using Infrastructure.EFCore.Repository;
using Infrastructure.EFCore.UnitOfWork;
using Infrastructure.Query;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using System.Configuration;

namespace Infrastructure.EFCore
{
    public class EFCoreModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            var connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            containerBuilder.Register((ctx) => new SocialNetworkDBContext(connectionString))
                .InstancePerDependency()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
            containerBuilder.RegisterType<EFUnitOfWork>()
                .As<IUnitOfWork>()
                .AsSelf()
                .InstancePerLifetimeScope()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
            containerBuilder.RegisterGeneric(typeof(EFGenericRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
            containerBuilder.RegisterGeneric(typeof(EntityFrameworkQuery<>))
                .As(typeof(IQuery<>))
                .InstancePerLifetimeScope()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
        }
    }
}
