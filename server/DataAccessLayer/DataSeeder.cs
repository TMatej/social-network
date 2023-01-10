using DataAccessLayer.Entity.Enum;
using DataAccessLayer.Entity.JoinEntity;
using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class DataSeeder
    {
        public static void Seed(this SocialNetworkDBContext dbContext)
        {
            SeedFileEntities(dbContext);
            SeedRoles(dbContext);
            SeedUsers(dbContext);
            SeedUserRoles(dbContext);
            SeedProfiles(dbContext);
            SeedContacts(dbContext);
            SeedGalleries(dbContext);
            SeedPosts(dbContext);
            SeedPhotos(dbContext);
            SeedParticipationTypes(dbContext);
            SeedConversations(dbContext);
            SeedMessages(dbContext);
            SeedGroups(dbContext);
            SeedGroupMembers(dbContext);
            SeedEvents(dbContext);
            SeedEventParticipants(dbContext);
            SeedConversationParticipants(dbContext);
            SeedComments(dbContext);
            SeedAttachments(dbContext);
        }

        private static void SeedAttachments(SocialNetworkDBContext dbContext)
        {
            var attachment = new Attachment
            {
                FileEntityId = 3,
                MessageId = 1
            };

            var another_attachment = new Attachment
            {
                FileEntityId = 4,
                MessageId = 2
            };

            dbContext.Attachments.Add(attachment);
            dbContext.Attachments.Add(another_attachment);
            dbContext.SaveChanges();
        }

        private static void SeedContacts(SocialNetworkDBContext dbContext)
        {
            var friendship = new Contact
            {
                User1Id = 1,
                User2Id = 2,
            };

            dbContext.Contacts.AddRange(friendship);
            dbContext.SaveChanges();
        }

        private static void SeedConversations(SocialNetworkDBContext dbContext)
        {
            var conversation = new Conversation // has 2 participants ->  Id=1, Id=2
            {
                UserId = 1,
            };
            var empty_conversation = new Conversation
            {
                UserId = 1,
            };

            dbContext.Conversations.AddRange(conversation, empty_conversation);
            dbContext.SaveChanges();
        }

        private static void SeedConversationParticipants(SocialNetworkDBContext dbContext)
        {
            var first_conversation_participant = new ConversationParticipant
            {
                UserId = 1,
                ConversationId = 1,
            };

            var second_conversation_participant = new ConversationParticipant
            {
                UserId = 2,
                ConversationId = 1,
            };

            dbContext.ConversationParticipants.AddRange(first_conversation_participant, second_conversation_participant);
            dbContext.SaveChanges();
        }

        private static void SeedEvents(SocialNetworkDBContext dbContext)
        {
            var groupId = 5;
            var event1 = new Event
            {
                UserId = 2,
                GroupId = groupId,
                Title = "Example Event",
                Description = $"This is an example event for Group {groupId}",
            };

            var eventWithNoParticipants = new Event
            {
                UserId = 2,
                GroupId = groupId,
                Title = "Example Userless Event",
                Description = $"This is an example event without participants for Group {groupId}",
            };

            dbContext.Events.AddRange(event1, eventWithNoParticipants);
            dbContext.SaveChanges();
        }

        private static void SeedEventParticipants(SocialNetworkDBContext dbContext)
        {
            var eventParticipant = new EventParticipant
            {
                EventId = 1,
                UserId = 1,
                ParticipationTypeId = 1,
            };

            var eventParticipant2 = new EventParticipant
            {
                EventId = 1,
                UserId = 2,
                ParticipationTypeId = 1,
            };

            dbContext.EventParticipants.AddRange(eventParticipant, eventParticipant2);
            dbContext.SaveChanges();
        }

        private static void SeedGalleries(SocialNetworkDBContext dbContext)
        {
            var galery = new Gallery
            {
                Title = "Example Galery",
                Description = "This is an example galery",
                ProfileId = 1
            };

            var empty_galery = new Gallery
            {
                Title = "Example Empty Galery",
                Description = "This is an example galery without content",
                ProfileId = 1
            };

            dbContext.Galleries.AddRange(galery, empty_galery); ;
            dbContext.SaveChanges();
        }

        private static void SeedProfiles(SocialNetworkDBContext dbContext)
        {
            var fullAddress = new Address
            {
                
                State = "Example State",
                Region = "Example Region",
                City = "Example City",
                Street = "Example Street",
                HouseNumber = "Example House number",
                PostalCode = "Example Postal Code"
            };

            var partialAdderss = new Address
            {
                State = "Example State",
                City = "Example City"
            };

            var adminprofile = new Profile
            {
                UserId = 1,
                Address = fullAddress
            };

            var userprofile = new Profile
            {
                UserId = 2,
                Address = partialAdderss
            };

            var jozoprofile = new Profile
            {
                UserId = 3,
                Address = fullAddress
            };

            var lokomotivarprofile = new Profile
            {
                UserId = 4,
                Address = partialAdderss
            };

            dbContext.Profiles.AddRange(adminprofile, userprofile, jozoprofile, lokomotivarprofile);
            dbContext.SaveChanges();
        }

        private static void SeedGroups(SocialNetworkDBContext dbContext)
        {
            var group1 = new Group
            {
                Name = "Example Group one",
                Description = "This is an example group",
            };

            var group2 = new Group
            {
                Name = "Example Group two",
                Description = "This is an example group",
            };

            dbContext.Groups.AddRange(group1, group2);
            dbContext.SaveChanges();
        }

        private static void SeedGroupMembers(SocialNetworkDBContext dbContext)
        {
            var groupmember = new GroupMember
            {
                GroupId = 5,
                UserId = 3,
                GroupRole = GroupRole.Author,
            };

            dbContext.GroupMembers.AddRange(groupmember);
            dbContext.SaveChanges();
        }

        private static void SeedMessages(SocialNetworkDBContext dbContext)
        {
            var message = new Message
            {
                Content = "I have two attachments!",
                AuthorId = 1,
                ReceiverId = 2,
                //ConversationId = 1
            };

            var message_without_attachment = new Message
            {
                Content = "I am just plain text",
                AuthorId = 2,
                ReceiverId = 1,
                //ConversationId = 1
            };

            dbContext.Messages.AddRange(message, message_without_attachment);
            dbContext.SaveChanges();
        }

        private static void SeedParticipationTypes(SocialNetworkDBContext dbContext)
        {
            var exampleType = new ParticipationType
            {
                Name = "Example Type"
            };

            dbContext.ParticipationTypes.AddRange(exampleType);
            dbContext.SaveChanges();
        }

        private static void SeedPhotos(SocialNetworkDBContext dbContext)
        {
            var photo1 = new Photo
            {
                Title = "My first photo",
                Description = "This is my first photo",
                FileEntityId = 1,
                GalleryId = 1
            };

            var photo2 = new Photo
            {
                Title = "My last photo",
                Description = "This is my last photo... No I didn't die",
                FileEntityId = 2,
                GalleryId = 1
            };

            dbContext.Photos.AddRange(photo1, photo2);
            dbContext.SaveChanges();
        }

        private static void SeedComments(SocialNetworkDBContext dbContext)
        {
            var post_comment = new Comment
            {
                CommentableId = 1,
                UserId = 1,
                Content = "Some content here!"
            };

            var photo_comment = new Comment
            {
                CommentableId = 3,
                UserId = 1,
                Content = "This photo is awful!"
            };

            var comment_comment = new Comment
            {
                CommentableId = 5,
                UserId = 2,
                Content = "This photo is beautifull you little prick!!!"
            };

            dbContext.Comments.Add(post_comment);
            dbContext.SaveChanges();
            dbContext.Comments.Add(photo_comment);
            dbContext.SaveChanges();
            dbContext.Comments.Add(comment_comment);
            dbContext.SaveChanges();
        }

        private static void SeedPosts(SocialNetworkDBContext dbContext)
        {
            var post1 = new Post
            {
                UserId = 1,
                PostableId = 1,
                Title = "Hello World!",
                Content = "This is my first post!",
            };

            var post2 = new Post
            {
                UserId = 1,
                PostableId = 1,
                Title = "Hello World!",
                Content = "This is my second post!",
            };

            dbContext.Posts.AddRange(post1, post2);
            dbContext.SaveChanges();
        }

        private static void SeedRoles(SocialNetworkDBContext dbContext)
        {
            var adminRole = new Role
            {
                Name = "Admin"
            };
            dbContext.Roles.Add(adminRole);
            dbContext.SaveChanges();

            var userRole = new Role
            {
                Name = "User"
            };
            dbContext.Roles.Add(userRole);
            dbContext.SaveChanges();
        }

        private static void SeedUsers(SocialNetworkDBContext dbContext)
        {
            var valid_admin = new User
            {
                Username = "admin",
                Email = "admin@gmail.com",
                PasswordHash = "$argon2id$v=19$m=4096,t=3,p=1$YmZ6eWcwZGc0cWIwMDAwMA$KTt1pbNZf0bwb4SlL3T83VdhMuRl/pZwJtlY/qgiUF4" // admin
            };
            dbContext.Users.Add(valid_admin);
            dbContext.SaveChanges();

            var valid_user = new User
            {
                Username = "user",
                Email = "user@gmail.com",
                PasswordHash = "$argon2id$v=19$m=4096,t=3,p=1$eWJhdTk1MGxqMjkwMDAwMA$VAxi71Fz2t4ft7j6dnKPSG16qeo/xtu5Ok4jgaZ01lc" // user
            };
            dbContext.Users.Add(valid_user);
            dbContext.SaveChanges();

            var userJozo = new User
            {
                Username = "jozkoVajda123",
                PasswordHash = "0123456789abcde0",
                Email = "JozoJeSuper@gmail.com",
            };

            var namelessUser = new User
            {
                Username = "lokomotivatomas123",
                Email = "cokoloko@gmail.com",
                PasswordHash = "0123456789abcde0"
            };
            dbContext.Users.AddRange(userJozo, namelessUser);
            dbContext.SaveChanges();
        }

        private static void SeedUserRoles(SocialNetworkDBContext dbContext)
        {
            var adminAdmin = new UserRole
            {
                UserId = 1,
                RoleId = 1
            };

            var userUser = new UserRole
            {
                UserId = 2,
                RoleId = 2
            };
            dbContext.UserRoles.AddRange(adminAdmin, userUser);
            dbContext.SaveChanges();
        }

        private static void SeedFileEntities(SocialNetworkDBContext dbContext)
        {
            var file1 = new FileEntity
            {
                Guid = Guid.NewGuid(),
                Name = "File",
                Data = new byte[] { },
                FileType = "image/jpg",
            };

            var file2 = new FileEntity
            {
                Guid = Guid.NewGuid(),
                Name = "File",
                Data = new byte[] { },
                FileType = "image/jpg",
            };

            var file3 = new FileEntity
            {
                Guid = Guid.NewGuid(),
                Name = "File",
                Data = new byte[] { },
                FileType = "image/jpg",
            };

            var file4 = new FileEntity
            {
                Guid = Guid.NewGuid(),
                Name = "File",
                Data = new byte[] { },
                FileType = "image/jpg",
            };

            dbContext.FileEntities.AddRange(file1, file2, file3, file4);
            dbContext.SaveChanges();

        }
    }
}
