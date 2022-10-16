using DataAccessLayer.Data;
using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class SocialNetworkDBContext : DbContext
    {
        private string connectionString { get; set; }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
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

        public SocialNetworkDBContext() {}

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
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<Profile>().OwnsOne(a => a.Address);

            /* setup One-To-Many relationship */
            modelBuilder.Entity<Galery>()
                .HasOne(g => g.Profile)
                .WithMany(p => p.Galeries)
                .HasForeignKey(a => a.ProfileId);

            modelBuilder.Entity<Photo>()
                .ToTable("Photo")
                .HasOne(p => p.Galery)
                .WithMany(g => g.Photos)
                .HasForeignKey(a => a.GaleryId);

            modelBuilder.Entity<User>()
                .HasOne<Profile>(u => u.Profile)
                .WithOne(o => o.User)
                .HasForeignKey<Profile>(p => p.UserId);

            modelBuilder.Entity<Post>().ToTable("Post");
            modelBuilder.Entity<Comment>().ToTable("Comment");

            modelBuilder.Entity<Profile>()
                .ToTable("Profile")
                .Property(p => p.Id)
                .HasColumnName("ProfileId");

            modelBuilder.Entity<Photo>()
                .Property(p => p.Id)
                .HasColumnName("PhotoId");

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }
    }
}
