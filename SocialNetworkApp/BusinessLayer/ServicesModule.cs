using Autofac;
using AutoMapper;
using BusinessLayer.Config;
using BusinessLayer.Contracts;
using BusinessLayer.Services;

namespace BusinessLayer
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<CommentService>()
                .As<ICommentService>()
                .InstancePerLifetimeScope()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
            containerBuilder.RegisterType<ContactService>()
                .As<IContactService>()
                .InstancePerLifetimeScope()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
            containerBuilder.RegisterType<ConversationService>()
                .As<IConversationService>()
                .InstancePerLifetimeScope()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
            containerBuilder.RegisterType<EventService>()
                .As<IEventService>()
                .InstancePerLifetimeScope()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
            containerBuilder.RegisterType<FileService>()
                .As<IFileService>()
                .InstancePerLifetimeScope()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
            containerBuilder.RegisterType<GalleryService>()
                .As<IGalleryService>()
                .InstancePerLifetimeScope()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
            containerBuilder.RegisterGeneric(typeof(GenericService<>))
                .As(typeof(IGenericService<>))
                .InstancePerLifetimeScope()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
            containerBuilder.RegisterType<GroupService>()
                .As<IGroupService>()
                .InstancePerLifetimeScope()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
            containerBuilder.RegisterType<LikeService>()
                .As<ILikeService>()
                .InstancePerLifetimeScope()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
            containerBuilder.RegisterType<PhotoService>()
                .As<IPhotoService>()
                .InstancePerLifetimeScope()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
            containerBuilder.RegisterType<PostService>()
                .As<IPostService>()
                .InstancePerLifetimeScope()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
            containerBuilder.RegisterType<ProfileService>()
                .As<IProfileService>()
                .InstancePerLifetimeScope()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
            containerBuilder.RegisterType<UserService>()
                .As<IUserService>()
                .InstancePerLifetimeScope()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
            containerBuilder.RegisterInstance(new Mapper(new MapperConfiguration(expression => expression.AddProfile<MappingProfile>())))
                .As<IMapper>()
                .SingleInstance()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
        }
    }
}