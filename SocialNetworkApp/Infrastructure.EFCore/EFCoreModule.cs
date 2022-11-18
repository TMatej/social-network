using Autofac;
using Infrastructure.EFCore.Query;
using Infrastructure.EFCore.Repository;
using Infrastructure.EFCore.UnitOfWork;
using Infrastructure.Query;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace Infrastructure.EFCore
{
    public class EFCoreModule : Module
    {

        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<EFUnitOfWork>()
                .As<IUnitOfWork>()
                .AsSelf()
                .InstancePerLifetimeScope();
            containerBuilder.RegisterGeneric(typeof(EFGenericRepository<>)).As(typeof(IRepository<>));
            containerBuilder.RegisterGeneric(typeof(EntityFrameworkQuery<>)).As(typeof(IQuery<>));
        }
    }
}
