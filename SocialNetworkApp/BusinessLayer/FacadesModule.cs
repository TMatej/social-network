
using Autofac;
using BusinessLayer.Facades;

namespace BusinessLayer
{
    public class FacadesModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<UserFacade>()
                .As<IUserFacade>()
                .InstancePerLifetimeScope()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
        }
    }
}
