using DataAccessLayer.Data;
using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class SocialNetworkDBContext : DbContext
    {
        private string connectionString { get; set; }

        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<ConversationParticipant> ConversationParticipants { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventParticipant> EventParticipants { get; set; }
        public DbSet<Galery> Galeries { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<GroupRole> GroupRoles { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ParticipationType> ParticipationTypes { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<User> Users { get; set; }

        public SocialNetworkDBContext() { }

        public SocialNetworkDBContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* Unique names for users */
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            /* Set owning property of Profile - Address */
            modelBuilder.Entity<Profile>().OwnsOne(a => a.Address);

            /* Set One-To-Many relationship */
            modelBuilder.Entity<Galery>()
                .HasOne(g => g.Profile)
                .WithMany(p => p.Galeries)
                .HasForeignKey(a => a.ProfileId);

            modelBuilder.Entity<Photo>()
                .ToTable("Photo")
                .HasOne(p => p.Galery)
                .WithMany(g => g.Photos)
                .HasForeignKey(a => a.GaleryId);

            /* Set One-To-One relationship */
            modelBuilder.Entity<User>()
                .HasOne<Profile>(u => u.Profile)
                .WithOne(o => o.User)
                .HasForeignKey<Profile>(p => p.UserId);

            modelBuilder.Entity<Post>().ToTable("Post");
            modelBuilder.Entity<Comment>().ToTable("Comment");

            modelBuilder.Entity<Profile>()
                .ToTable("Profile");

            modelBuilder.Entity<Photo>()
                .Property(p => p.Id)
                .HasColumnName("PhotoId");

            /* Set Many-To-Many relationship for User <-> Event */
            modelBuilder.Entity<EventParticipant>()
                .HasKey(ep => new { ep.UserId, ep.EventId });

            /* Set Many-To-Many relationship for User <-> Conversation */
            modelBuilder.Entity<ConversationParticipant>()
                .HasKey(cp => new { cp.UserId, cp.ConversationId });

            /* Set Many-To-Many relationship for User <-> Group */
            modelBuilder.Entity<GroupMember>()
                .HasKey(gm => new { gm.UserId, gm.GroupId });

            /* Set Many-To-Many relationship for User <-> User */
            modelBuilder.Entity<Contact>()
                .HasKey(c => new { c.User1Id, c.User2Id });

            modelBuilder.Entity<Contact>()
                .HasOne(c => c.User1)
                .WithMany(u => u.ContactsOf)
                .HasForeignKey(c => c.User1Id)
                .OnDelete(DeleteBehavior.Restrict);

            /* Note that in both cases you have to turn the delete cascade off 
             * for at least one of the relationships and manually delete the
             * related join entities before deleting the main entity, because 
             * self referencing relationships always introduce possible cycles 
             * or multiple cascade path issue, preventing the usage of cascade 
             * delete.
             * 
             * https://stackoverflow.com/questions/49214748/many-to-many-self-referencing-relationship/49219124#49219124
             */

            modelBuilder.Entity<Contact>()
                .HasOne(c => c.User2)
                .WithMany(u => u.Contacts)
                .HasForeignKey(c => c.User2Id);

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }
    }
}
