
using Autofac;
using BusinessLayer.Facades;
using BusinessLayer.Facades.Interfaces;

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
            containerBuilder.RegisterType<ProfileFacade>()
                .As<IProfileFacade>()
                .InstancePerLifetimeScope()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
        }
    }
}
