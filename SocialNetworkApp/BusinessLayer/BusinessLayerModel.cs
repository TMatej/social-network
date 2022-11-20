using Autofac;
using AutoMapper;
using BusinessLayer.Config;
using BusinessLayer.Contracts;
using BusinessLayer.QueryObjects;
using BusinessLayer.Services;

namespace BusinessLayer
{
    public static class BusinessLayerModel
    {
        public static ContainerBuilder RegisterBusinessLayer(this ContainerBuilder containerBuilder)
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
            containerBuilder.RegisterInstance(new Mapper(new MapperConfiguration(expression => expression.AddProfile<MappingProfile>())))  // should be registered in BL level
                .As<IMapper>()
                .SingleInstance()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
            return containerBuilder;
        }
    }
}
