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
    public static class EFCoreModule
    {
        public static ContainerBuilder RegisterEFCore(this ContainerBuilder containerBuilder)
        {
            var connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            containerBuilder.Register((ctx) => new SocialNetworkDBContext(connectionString));
            containerBuilder.RegisterType<EFUnitOfWork>()
                .As<IUnitOfWork>()
                .AsSelf()
                .InstancePerLifetimeScope();
            containerBuilder.RegisterGeneric(typeof(EFGenericRepository<>)).As(typeof(IRepository<>));
            containerBuilder.RegisterGeneric(typeof(EntityFrameworkQuery<>)).As(typeof(IQuery<>));
            return containerBuilder;
        }
    }
}
