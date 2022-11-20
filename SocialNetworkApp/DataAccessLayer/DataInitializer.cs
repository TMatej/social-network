using DataAccessLayer.Entity;
using DataAccessLayer.Entity.JoinEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Reflection.Emit;

namespace DataAccessLayer.Data
{
    public static class DataInitializer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            SeedUsers(modelBuilder);
            SeedProfiles(modelBuilder);
            SeedPosts(modelBuilder);
            SeedPhotos(modelBuilder);
            SeedParticipationTypes(modelBuilder);
            SeedMessages(modelBuilder);
            SeedGroupRoles(modelBuilder);
            SeedGroups(modelBuilder);
            SeedGroupMembers(modelBuilder);
            SeedGaleries(modelBuilder);
            SeedEvents(modelBuilder);
            SeedEventParticipants(modelBuilder);
            SeedConversations(modelBuilder);
            SeedConversationParticipants(modelBuilder);
            SeedContacts(modelBuilder);
            SeedComments(modelBuilder);
            SeedAttachments(modelBuilder);
        }

        private static void SeedAttachments(ModelBuilder modelBuilder)
        {
            var attachment = new Attachment
            {
                Id = 1,
                Url = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png",
                MessageId = 1
            };

            var another_attachment = new Attachment
            {
                Id = 2,
                Url = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png",
                MessageId = 2
            };

            modelBuilder.Entity<Attachment>().HasData(attachment);
            modelBuilder.Entity<Attachment>().HasData(another_attachment);
        }

        private static void SeedContacts(ModelBuilder modelBuilder)
        {
            var friendship = new Contact
            {
                Id = 1,
                User1Id = 1,
                User2Id = 2,
                CreatedAt = DateTime.Now
            };

            modelBuilder.Entity<Contact>().HasData(friendship);
        }

        private static void SeedConversations(ModelBuilder modelBuilder)
        {
            var conversation = new Conversation // has 2 participants ->  Id=1, Id=2
            {
                Id = 1,
                UserId = 1,
                CreatedAt = DateTime.Now
            };
            var empty_conversation = new Conversation
            {
                Id = 2,
                UserId = 1,
                CreatedAt = DateTime.Now
            };

            modelBuilder.Entity<Conversation>().HasData(conversation);
            modelBuilder.Entity<Conversation>().HasData(empty_conversation);
        }

        private static void SeedConversationParticipants(ModelBuilder modelBuilder)
        {
            var first_conversation_participant = new ConversationParticipant
            {
                Id = 1,
                UserId = 1,
                ConversationId = 1,
                CreatedAt = DateTime.Now
            };

            var second_conversation_participant = new ConversationParticipant
            {
                Id = 2,
                UserId = 2,
                ConversationId = 1,
                CreatedAt = DateTime.Now
            };

            modelBuilder.Entity<ConversationParticipant>().HasData(first_conversation_participant);
            modelBuilder.Entity<ConversationParticipant>().HasData(second_conversation_participant);
        }

        private static void SeedEvents(ModelBuilder modelBuilder)
        {
            var groupId = 3;
            var event1 = new Event
            {
                Id = 1,
                UserId = 1,
                GroupId = groupId,
                Title = "Example Event",
                Description = $"This is an example event for Group {groupId}",
                CreatedAt = DateTime.Now
            };

            var eventWithNoParticipants = new Event
            {
                Id = 2,
                UserId = 1,
                GroupId = groupId,
                Title = "Example Userless Event",
                Description = $"This is an example event without participants for Group {groupId}",
                CreatedAt = DateTime.Now
            };

            modelBuilder.Entity<Event>().HasData(event1);
            modelBuilder.Entity<Event>().HasData(eventWithNoParticipants);
        }

        private static void SeedEventParticipants(ModelBuilder modelBuilder)
        {
            var eventParticipant = new EventParticipant
            {
                Id = 1,
                EventId = 1,
                UserId = 1,
                ParticipationTypeId = 1,
                CreatedAt = DateTime.Now
            };

            var eventParticipant2 = new EventParticipant
            {
                Id = 2,
                EventId = 1,
                UserId = 2,
                ParticipationTypeId = 1,
                CreatedAt = DateTime.Now
            };

            modelBuilder.Entity<EventParticipant>().HasData(eventParticipant);
            modelBuilder.Entity<EventParticipant>().HasData(eventParticipant2);
        }

        private static void SeedGaleries(ModelBuilder modelBuilder)
        {
            var galery = new Gallery
            {
                Id = 1,
                Title = "Example Galery",
                Description = "This is an example galery",
                CreatedAt = DateTime.Now,
                ProfileId = 1
            };

            var empty_galery = new Gallery
            {
                Id = 2,
                Title = "Example Empty Galery",
                Description = "This is an example galery without content",
                CreatedAt = DateTime.Now,
                ProfileId = 1
            };

            modelBuilder.Entity<Gallery>().HasData(galery);
            modelBuilder.Entity<Gallery>().HasData(empty_galery);
        }

        private static void SeedProfiles(ModelBuilder modelBuilder)
        {
            var profile1 = new Profile
            {
                Id = 1,
                UserId = 1,
                CreatedAt = DateTime.Now,
            };

            var profile2 = new Profile
            {
                Id = 2,
                UserId = 2,
                CreatedAt = DateTime.Now,
            };

            var fullAddress = new
            {
                ProfileId = 1,
                State = "Example State",
                Region = "Example Region",
                City = "Example City",
                Street = "Example Street",
                HouseNumber = "Example House number",
                PostalCode = "Example Postal Code"
            };

            var partialAdderss = new
            {
                ProfileId = 2,
                State = "Example State",
                City = "Example City"
            };

            modelBuilder.Entity<Profile>(p =>
            {
                p.HasData(profile1);
                p.OwnsOne(p => p.Address).HasData(fullAddress);
            });

            modelBuilder.Entity<Profile>(p =>
            {
                p.HasData(profile2);
                p.OwnsOne(p => p.Address).HasData(partialAdderss);
            });
        }

        private static void SeedGroups(ModelBuilder modelBuilder)
        {
            var group1 = new Group
            {
                Id = 3,
                Name = "Example Group one",
                Description = "This is an example group",
                CreatedAt = DateTime.Now
            };

            var group2 = new Group
            {
                Id = 4,
                Name = "Example Group two",
                Description = "This is an example group",
                CreatedAt = DateTime.Now
            };

            modelBuilder.Entity<Group>().HasData(group1);
            modelBuilder.Entity<Group>().HasData(group2);
        }

        private static void SeedGroupMembers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupMember>().HasData(
               new GroupMember
               {
                   Id = 1,
                   GroupId = 3,
                   UserId = 1,
                   GroupRoleId = 1,
                   CreatedAt = DateTime.Now
               }
               );
        }

        private static void SeedGroupRoles(ModelBuilder modelBuilder)
        {
            var groupRole = new GroupRole
            {
                Id = 1,
                Name = "Example Role"
            };

            modelBuilder.Entity<GroupRole>().HasData(groupRole);
        }

        private static void SeedMessages(ModelBuilder modelBuilder)
        {
            var message = new Message
            {
                Id = 1,
                Content = "I have two attchments!",
                ConversationId = 1,
                AuthorId = 1,
                Timestamp = DateTime.Now
            };

            var message_without_attachment = new Message
            {
                Id = 2,
                Content = "I am just plain text",
                ConversationId = 1,
                AuthorId = 2,
                Timestamp = DateTime.Now
            };

            modelBuilder.Entity<Message>().HasData(message);
            modelBuilder.Entity<Message>().HasData(message_without_attachment);
        }

        private static void SeedParticipationTypes(ModelBuilder modelBuilder)
        {
            var exampleType = new ParticipationType
            {
                Id = 1,
                Name = "Example Type"
            };

            modelBuilder.Entity<ParticipationType>().HasData(exampleType);
        }

        private static void SeedPhotos(ModelBuilder modelBuilder)
        {
            var photo1 = new Photo
            {
                Id = 3,
                Title = "My first photo",
                Description = "This is my first photo",
                CreatedAt = DateTime.Now,
                Url = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png",
                GaleryId = 1
            };

            var photo2 = new Photo
            {
                Id = 4,
                Title = "My last photo",
                Description = "This is my last photo... No I didn't die",
                CreatedAt = DateTime.Now,
                Url = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png",
                GaleryId = 1
            };

            modelBuilder.Entity<Photo>().HasData(photo1);
            modelBuilder.Entity<Photo>().HasData(photo2);
        }

        private static void SeedComments(ModelBuilder modelBuilder)
        {
            var post_comment = new Comment
            {
                Id = 5,
                CommentableId = 1,
                UserId = 1,
                CreatedAt = DateTime.Now,
                Content = "Some content here!"
            };

            var photo_comment = new Comment
            {
                Id = 6,
                CommentableId = 3,
                UserId = 1,
                CreatedAt = DateTime.Now,
                Content = "This photo is awful!"
            };

            var comment_comment = new Comment
            {
                Id = 7,
                CommentableId = 5,
                UserId = 2,
                CreatedAt = DateTime.Now,
                Content = "This photo is beautifull you little prick!!!"
            };

            modelBuilder.Entity<Comment>().HasData(post_comment);
            modelBuilder.Entity<Comment>().HasData(photo_comment);
            modelBuilder.Entity<Comment>().HasData(comment_comment);
        }

        private static void SeedPosts(ModelBuilder modelBuilder)
        {
            var post1 = new Post
            {
                Id = 1,
                UserId = 1,
                PostableId = 1,
                Title = "Hello World!",
                Content = "This is my first post!",
                CreatedDate = DateTime.Now
            };

            var post2 = new Post
            {
                Id = 2,
                UserId = 1,
                PostableId = 1,
                Title = "Hello World!",
                Content = "This is my second post!",
                CreatedDate = DateTime.Now
            };

            modelBuilder.Entity<Post>().HasData(post1);
            modelBuilder.Entity<Post>().HasData(post2);
        }

        private static void SeedUsers(ModelBuilder modelBuilder)
        {
            var userJozo = new User
            {
                Id = 1,
                Username = "jozkoVajda123",
                PasswordHash = "0123456789abcde0",
                PrimaryEmail = "JozoJeSuper@gmail.com",
                SecondaryEmail = "NajlepsieZaklinadlo@gmail.com"
            }; 

            var namelessUser = new User
            {
                Id = 2,
                Username = "lokomotivatomas123",
                PrimaryEmail = "cokoloko@gmail.com",
                PasswordHash = "0123456789abcde0"
            };

            modelBuilder.Entity<User>().HasData(userJozo);
            modelBuilder.Entity<User>().HasData(namelessUser);
        }
    }
}
