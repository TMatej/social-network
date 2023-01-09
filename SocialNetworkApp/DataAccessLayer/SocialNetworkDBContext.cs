using DataAccessLayer.Data;
using DataAccessLayer.Entity;
using DataAccessLayer.Entity.JoinEntity;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class SocialNetworkDBContext : DbContext
    {
        private string connectionString { get; set; }
        private bool seedData { get; }

        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<ConversationParticipant> ConversationParticipants { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<FileEntity> FileEntities { get; set; }
        public DbSet<EventParticipant> EventParticipants { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ParticipationType> ParticipationTypes { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Like> Likes { get; set; }

        public SocialNetworkDBContext() { }

        public SocialNetworkDBContext(string database)
        {
            /* Constructor for tests */
            var host = Environment.GetEnvironmentVariable("POSTGRES_HOST") ?? "localhost";
            var userName = Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "postgres";
            var password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "postgres";
            var port = Environment.GetEnvironmentVariable("POSTGRES_PORT") ?? "5432";
            var mssqlport = Environment.GetEnvironmentVariable("MSSQL_PORT") ?? "1433";
            var mssqlhost = Environment.GetEnvironmentVariable("MSSQL_HOST") ?? "localhost";

            //connectionString = $"Host={host};Username={userName};Password={password};Port={port};Database={database};";
            connectionString = $"Server={mssqlhost},{mssqlport};Database={database};User Id=SA;Password=mySuper!password9;MultipleActiveResultSets=true;TrustServerCertificate=True;";
            seedData = false;
        }

        public SocialNetworkDBContext(string connectionString, bool seedData)
        {
            this.connectionString = connectionString;
            this.seedData = seedData;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(connectionString)
                //.UseNpgsql(connectionString)
                // logging of SQL commands into console
                /*
                .UseLoggerFactory(LoggerFactory.Create(
                    builder =>
                    {
                        builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name
                        && level == LogLevel.Information).AddConsole();
                    }))
                */
                .EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }


        /* Main rule -> Owner of entity is responsible for the deleting of the entity
           i.e. -> Owner (User) of conversation (Conversation) must delete all the conversation before owner is deleted.
                   Auhor (User) of message (Message) must delete all the messages before author is deleted.
                   User1 (User) in contact (Contact) must delete all the contacts where he is User1, before he is deleted.
         */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* Unique names for users */
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<EventParticipant>()
                .HasIndex(ep => ep.UserId)
                .IsUnique();

            modelBuilder.Entity<Contact>()
                .HasIndex(c => new { c.User1Id, c.User2Id })
                .IsUnique();

            modelBuilder.Entity<Contact>()
                .HasIndex(c => new { c.User2Id, c.User1Id })
                .IsUnique();

            /* Set owning property of Profile - Address */
            modelBuilder.Entity<Profile>()
                .OwnsOne(p => p.Address);

            /* Set One-To-Many relationship */
            modelBuilder.Entity<Gallery>()
                .HasOne(g => g.Profile)
                .WithMany(p => p.Galleries)
                .HasForeignKey(a => a.ProfileId);

            modelBuilder.Entity<Photo>()
                .HasOne(p => p.Gallery)
                .WithMany(g => g.Photos)
                .HasForeignKey(a => a.GalleryId);

            /* Commentable */
            modelBuilder.Entity<Comment>().ToTable("Comment");
            modelBuilder.Entity<Photo>().ToTable("Photo");
            modelBuilder.Entity<Post>().ToTable("Post");

            /* Postable */
            modelBuilder.Entity<Group>().ToTable("Group");
            modelBuilder.Entity<Profile>().ToTable("Profile");

            /* Service needs to ensure deleting of all appropriate
             * contacts before deleting User1 
               TESTED IN USER ADVANCED TESTS ???? */
            modelBuilder.Entity<Contact>()
                .HasOne(c => c.User1)
                .WithMany(u => u.Contacts)
                .HasForeignKey(c => c.User1Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Contact>()
                .HasOne(c => c.User2)
                .WithMany(u => u.ContactsOf)
                .HasForeignKey(c => c.User2Id);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            /* Note that in both cases you have to turn the delete cascade off 
             * for at least one of the relationships and manually delete the
             * related join entities before deleting the main entity, because 
             * self referencing relationships always introduce possible cycles 
             * or multiple cascade path issue, preventing the usage of cascade 
             * delete.
             * 
             * https://stackoverflow.com/questions/49214748/many-to-many-self-referencing-relationship/49219124#49219124
             */

            
            modelBuilder.Entity<User>()
                .HasMany(u => u.ConversationParticipants)
                .WithOne(c => c.User)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Avatar)
                .WithOne()
                .HasForeignKey<User>(u => u.AvatarId);

            /* Service needs to ensure deleting of all appropriate
             * messages before deleting Author 
               TESTED IN USER ADVANCED TESTS */
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Author)
                .WithMany()
                .HasForeignKey(m => m.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

            /* Service needs to ensure deleting of all appropriate
             * Conversations before deleting User (owner) 
               TESTED IN CONVERSATION ADVANCED TESTS */
            modelBuilder.Entity<Conversation>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            /* Service needs to ensure deleting of all appropriate
             * Event Participations before deleting User 
               WILL BE TESTED IN USER ADVANCED TESTS */
            modelBuilder.Entity<User>()
                .HasMany(u => u.EventParticipants)
                .WithOne(c => c.User)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Commentable)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.CommentableId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Like>()
                .HasOne(l => l.Post)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FileEntity>()
                .HasIndex(f => f.Guid)
                .IsUnique();

            modelBuilder.Entity<Role>()
                .HasIndex(r => r.Name)
                .IsUnique();

            // Set default values for timestamps
            modelBuilder.Entity<Comment>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Contact>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Conversation>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<ConversationParticipant>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Event>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<FileEntity>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<EventParticipant>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Gallery>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Group>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<GroupMember>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Message>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<ParticipationType>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Photo>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Post>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Profile>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<User>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            if (seedData) modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }
    }
}
