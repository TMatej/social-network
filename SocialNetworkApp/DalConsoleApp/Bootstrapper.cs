using Autofac;
using AutoMapper;
using BusinessLayer.Config;
using BusinessLayer.Contracts;
using BusinessLayer.Services;
using Infrastructure.EFCore;

namespace DalConsoleApp
{
    internal class Bootstrapper : IDisposable
    {
        public IContainer Container { get; private set; }
        public Bootstrapper()
        {
            var builder = new ContainerBuilder();
            builder.RegisterEFCore();
            builder.RegisterType<UserService>()
                .As<IUserService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<GalleryService>()
                .As<IGalleryService>()
                .InstancePerLifetimeScope();
            builder.RegisterInstance(new Mapper(new MapperConfiguration(expression => expression.AddProfile<MappingProfile>())))  // should be registered in BL level
                .As<IMapper>()
                .SingleInstance();
            Container = builder.Build();
        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }
}
