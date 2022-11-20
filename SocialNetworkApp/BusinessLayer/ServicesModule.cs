using Autofac;
using AutoMapper;
using BusinessLayer.Config;
using BusinessLayer.Contracts;
using BusinessLayer.QueryObjects;
using BusinessLayer.Services;

namespace BusinessLayer
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<UserService>()
                .As<IUserService>()
                .InstancePerLifetimeScope()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
            containerBuilder.RegisterType<GalleryQueryObject>()
                .AsSelf()
                .InstancePerLifetimeScope()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}")); ;
            containerBuilder.RegisterType<GalleryService>()
                .As<IGalleryService>()
                .InstancePerLifetimeScope()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
            containerBuilder.RegisterGeneric(typeof(GenericQueryObject<>))
                .AsSelf()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
            containerBuilder.RegisterInstance(new Mapper(new MapperConfiguration(expression => expression.AddProfile<MappingProfile>())))
                .As<IMapper>()
                .SingleInstance()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
        }
    }
}