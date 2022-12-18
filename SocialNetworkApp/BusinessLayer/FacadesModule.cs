
using Autofac;
using BusinessLayer.Facades;
using BusinessLayer.Facades.Interfaces;

namespace BusinessLayer
{
    public class FacadesModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<CommentFacade>()
                .As<ICommentFacade>()
                .InstancePerLifetimeScope()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
            containerBuilder.RegisterType<EventFacade>()
                .As<IEventFacade>()
                .InstancePerLifetimeScope()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
            containerBuilder.RegisterType<GroupFacade>()
                .As<IGroupFacade>()
                .InstancePerLifetimeScope()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
            containerBuilder.RegisterType<PostFacade>()
                .As<IPostFacade>()
                .InstancePerLifetimeScope()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
            containerBuilder.RegisterType<ProfileFacade>()
                .As<IProfileFacade>()
                .InstancePerLifetimeScope()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
            containerBuilder.RegisterType<UserFacade>()
                .As<IUserFacade>()
                .InstancePerLifetimeScope()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
            containerBuilder.RegisterType<FileFacade>()
                .As<IFileFacade>()
                .InstancePerLifetimeScope()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
            containerBuilder.RegisterType<SearchFacade>()
                .As<ISearchFacade>()
                .InstancePerLifetimeScope()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
            containerBuilder.RegisterType<GalleryFacade>()
                .As<IGalleryFacade>()
                .InstancePerLifetimeScope()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
        }
    }
}
