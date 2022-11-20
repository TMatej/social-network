using Autofac;
using AutoMapper;
using BusinessLayer.Config;
using BusinessLayer.Contracts;
using BusinessLayer.QueryObjects;
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
                .InstancePerLifetimeScope()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
            builder.RegisterType<GalleryQueryObject>()
                .AsSelf()
                .InstancePerLifetimeScope()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}")); ;
            builder.RegisterType<GalleryService>()
                .As<IGalleryService>()
                .InstancePerLifetimeScope()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
            builder.RegisterInstance(new Mapper(new MapperConfiguration(expression => expression.AddProfile<MappingProfile>())))  // should be registered in BL level
                .As<IMapper>()
                .SingleInstance()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));

            Container = builder.Build();
        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }
}
