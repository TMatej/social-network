using Autofac;
using BusinessLayer.Contracts;
using BusinessLayer.Services;

namespace BusinessLayer
{
    public class ServicesModule : Module
    {

        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
        }
    }
}