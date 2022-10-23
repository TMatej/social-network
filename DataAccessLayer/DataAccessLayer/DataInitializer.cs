using DataAccessLayer.Entity;
using DataAccessLayer.Entity.JoinEntity;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data
{
    public static class DataInitializer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData
            (
                new User
                {
                    Id = 1,
                    Username = "lokomotivatomas123",
                    PrimaryEmail = "cokoloko@gmail.com",
                    PasswordHash = "0123456789abcde0"
                }
            );
            modelBuilder.Entity<Profile>().HasData
            (
                new Profile
                {
                    Id = 1,
                    CreatedAt = DateTime.Now,
                    UserId = 1,
                    Address = new Address
                    {
                        State = "Example State",
                        Street = "Example Street",
                        City = "Example City",
                        PostalCode = "Example Postal Code",
                        Region = "Example Region"
                    }
                }
            );
            modelBuilder.Entity<Post>().HasData
            (
                new Post
                {
                    Id = 2,
                    UserId = 1,
                    PostableId = 1,
                    Title = "Hello World!",
                    Content = "This is my first post!",
                    CreatedDate = DateTime.Now
                }
            );
            modelBuilder.Entity<Photo>().HasData
            (
                new Photo
                {
                    Id = 1,
                    Title = "My first photo",
                    Description = "This is my first photo",
                    CreatedAt = DateTime.Now,
                    Url = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png",
                    GaleryId = 1
                }
            );
            modelBuilder.Entity<ParticipationType>().HasData(
                new ParticipationType
                {
                    Id = 1,
                    Name = "Example Type"
                }
                );
            modelBuilder.Entity<Message>().HasData(
                new Message
                {
                    Id = 1,
                    Content = "Hello World!",
                    ConversationId = 1,
                    AuthorId = 1,
                    Timestamp = DateTime.Now

                }
                ) ;
            modelBuilder.Entity<GroupRole>().HasData(
                new GroupRole
                {
                    Id = 1,
                    Name = "Example Role"
                }
                );
            modelBuilder.Entity<GroupMember>().HasData(
                new GroupMember
                {
                    Id = 1,
                    GroupId = 1,
                    UserId = 1,
                    GroupRoleId = 1,
                    CreatedAt = DateTime.Now
                }
                );
            modelBuilder.Entity<Group>().HasData(
                new Group
                {
                    Id = 1,
                    Name = "Example Group",
                    Description = "This is an example group",
                    CreatedAt = DateTime.Now
                }
                );
            modelBuilder.Entity<Galery>().HasData(
                new Galery
                {
                    Id = 1,
                    Title = "Example Galery",
                    Description = "This is an example galery",
                    CreatedAt = DateTime.Now
                }
                );
            modelBuilder.Entity<EventParticipant>().HasData(
                new EventParticipant
                {
                    Id = 1,
                    EventId = 1,
                    UserId = 1,
                    ParticipationTypeId = 1,
                    CreatedAt = DateTime.Now
                }
                );
            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    Id = 1,
                    UserId = 1,
                    GroupId = 1,
                    Title = "Example Event",
                    Description = "This is an example event",
                    CreatedAt = DateTime.Now
                }
                ) ;
            /*modelBuilder.Entity<Conversation>().HasData(
                new Conversation
                {
                    Id = 1,
                    UserId = 1
                }
                );*/
            modelBuilder.Entity<Attachment>().HasData(
                new Attachment
                {
                    Id = 1,
                    Url = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png",
                    MessageId = 1
                }
                );
        }
    }
}
