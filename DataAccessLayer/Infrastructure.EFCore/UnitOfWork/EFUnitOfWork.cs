using DataAccessLayer;
using DataAccessLayer.Entity;
using Infrastructure.EFCore.Repository;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace Infrastructure.EFCore.UnitOfWork
{
    public class EFUnitOfWork : IUnitOfWork
    {
        public SocialNetworkDBContext Context { get; } = new();

        private IRepository<Address> addressRepository;
        private IRepository<Attachment> attachmentRepository;
        private IRepository<Comment> commentRepository;
        private IRepository<Conversation> conversationRepository;
        private IRepository<Event> eventRepository;
        private IRepository<EventParticipant> eventParticipantRepository;
        private IRepository<Galery> galeryRepository;
        private IRepository<Group> groupRepository;
        private IRepository<GroupMember> groupMemberRepository;
        private IRepository<GroupRole> groupRoleRepository;
        private IRepository<Message> messageRepository;
        private IRepository<ParticipationType> participationTypeRepository;
        private IRepository<Photo> photoRepository;
        private IRepository<Post> postRepository;
        private IRepository<Profile> profileRepository;
        private IRepository<User> userRepository;

        public EFUnitOfWork(SocialNetworkDBContext dbContext)
        {
            Context = dbContext;
        }

        public IRepository<Address> AddressRepository
        {
            get
            {
                if (this.addressRepository == null)
                {
                    this.addressRepository = new EFGenericRepository<Address>(Context);
                }
                return addressRepository;
            }
        }

        public IRepository<Attachment> AttachmentRepository
        {
            get
            {
                if (this.attachmentRepository == null)
                {
                    this.attachmentRepository = new EFGenericRepository<Attachment>(Context);
                }
                return attachmentRepository;
            }
        }

        public IRepository<Comment> CommentRepository
        {
            get


            {
                if (this.commentRepository == null)
                {
                    this.commentRepository = new EFGenericRepository<Comment>(Context);
                }
                return commentRepository;
            }
        }

        public IRepository<Conversation> ConversationRepository
        {
            get
            {
                if (this.conversationRepository == null)
                {
                    this.conversationRepository = new EFGenericRepository<Conversation>(Context);
                }
                return conversationRepository;
            }
        }

        public IRepository<Event> EventRepository
        {
            get
            {
                if (this.eventRepository == null)
                {
                    this.eventRepository = new EFGenericRepository<Event>(Context);
                }
                return eventRepository;
            }
        }

        public IRepository<EventParticipant> EventParticipantRepository
        {
            get
            {
                if (this.eventParticipantRepository == null)
                {
                    this.eventParticipantRepository = new EFGenericRepository<EventParticipant>(Context);
                }
                return eventParticipantRepository;
            }
        }

        public IRepository<Galery> GaleryRepository
        {
            get
            {
                if (this.galeryRepository == null)
                {
                    this.galeryRepository = new EFGenericRepository<Galery>(Context);
                }
                return galeryRepository;
            }
        }

        public IRepository<Group> GroupRepository
        {
            get
            {
                if (this.groupRepository == null)
                {
                    this.groupRepository = new EFGenericRepository<Group>(Context);
                }
                return groupRepository;
            }
        }


        public IRepository<GroupMember> GroupMemberRepository
        {
            get
            {
                if (this.groupMemberRepository == null)
                {
                    this.groupMemberRepository = new EFGenericRepository<GroupMember>(Context);
                }
                return groupMemberRepository;
            }
        }

        public IRepository<GroupRole> GroupRoleRepository
        {
            get
            {
                if (this.groupRoleRepository == null)
                {
                    this.groupRoleRepository = new EFGenericRepository<GroupRole>(Context);
                }
                return groupRoleRepository;
            }
        }

        public IRepository<Message> MessageRepository
        {
            get
            {
                if (this.messageRepository == null)
                {
                    this.messageRepository = new EFGenericRepository<Message>(Context);
                }
                return messageRepository;
            }
        }

        public IRepository<ParticipationType> ParticipationTypeRepository
        {
            get
            {
                if (this.participationTypeRepository == null)
                {
                    this.participationTypeRepository = new EFGenericRepository<ParticipationType>(Context);
                }
                return participationTypeRepository;
            }
        }

        public IRepository<Photo> PhotoRepository
        {
            get
            {
                if (this.photoRepository == null)
                {
                    this.photoRepository = new EFGenericRepository<Photo>(Context);
                }
                return photoRepository;
            }
        }

        public IRepository<Post> PostRepository
        {
            get
            {
                if (this.postRepository == null)
                {
                    this.postRepository = new EFGenericRepository<Post>(Context);
                }
                return postRepository;
            }
        }

        public IRepository<Profile> ProfileRepository
        {
            get
            {
                if (this.profileRepository == null)
                {
                    this.profileRepository = new EFGenericRepository<Profile>(Context);
                }
                return profileRepository;
            }
        }

        public IRepository<User> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new EFGenericRepository<User>(Context);
                }
                return userRepository;
            }
        }

        public async Task Commit()
        {
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
