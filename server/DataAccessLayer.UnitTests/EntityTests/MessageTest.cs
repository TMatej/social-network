using DataAccessLayer.Entity;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;




namespace DalUnitTests.EntityTests
{
    public class MessageTest
    {
        [SetUp]
        public void Setup()
        {
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                db.Users.Add(new User
                {
                    Username = "ben",
                    Email = "ben@gmail.com",
                    PasswordHash = "aaafht3x"
                });
                db.SaveChanges();

                db.Users.Add(new User
                {
                    Username = "ten",
                    Email = "ten@gmail.com",
                    PasswordHash = "aaafht3x"
                });
                db.SaveChanges();

                db.Conversations.Add(new Conversation
                {
                    UserId = 1 // author of message must be also the owner of conversation (each owner is responsible for handling its entities)
                });
                db.SaveChanges();
            }
        }

        [TearDown]
        public void TearDown()
        {
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                db.Database.EnsureDeleted();
                db.Dispose();
            }
        }

        [Test]
        public void Test_Add()
        {
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                db.Messages.Add(new Message
                {
                    Content = "Hello World!",
                    AuthorId = 1,
                    ReceiverId = 2,
                    //ConversationId = 1
                });
                db.SaveChanges();

                var message = db.Messages.FirstOrDefault();
                Assert.That(message, Is.Not.Null);
                Assert.That(message.Id, Is.EqualTo(1));
            }
        }

        [Test]
        public void Test_Add_Incomplete()
        {
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                db.Messages.Add(new Message
                {
                    Content = "Hello World!",
                    AuthorId = 1,
                });
                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }

        [Test]
        public void Delete_Message_Test()
        {
            // Arrange
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                db.Messages.Add(new Message
                {
                    Content = "Hello World!",
                    AuthorId = 1,
                    ReceiverId = 2,
                    //ConversationId = 1
                });
                db.SaveChanges();
            }

            // Act
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                var message_ret = db.Messages.FirstOrDefault();
                db.Messages.Remove(message_ret);
                db.SaveChanges();
            }

            // Assert
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                var message_ret = db.Messages.FirstOrDefault();
                var user1_ret = db.Users.FirstOrDefault();
                var user2_ret = db.Users.OrderByDescending(u => u.Id).FirstOrDefault();
                Assert.That(message_ret, Is.Null);
                Assert.That(user1_ret, Is.Not.Null);
                Assert.That(user2_ret, Is.Not.Null);
            }
        }

        [Test]
        public void Delete_Message_With_Attachment_Test()
        {
            // Arrange
            var message = new Message
            {
                Content = "Hello World!",
                AuthorId = 1,
                ReceiverId = 2,
                //ConversationId = 1
            };

            var fileEntity = new FileEntity
            {
                Guid = Guid.NewGuid(),
                Name = "Photo name",
                Data = new byte[] { },
                FileType = "image/jpg",
            };

            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                db.Messages.Add(message);
                db.FileEntities.Add(fileEntity);
                db.SaveChanges();
            }

            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                db.Attachments.Add(new Attachment
                {
                    MessageId = message.Id,
                    FileEntityId = fileEntity.Id
                });
                db.SaveChanges();
            }

            // Act
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                var message_ret = db.Messages.FirstOrDefault();
                db.Messages.Remove(message_ret);
                db.SaveChanges();
            }

            // Assert
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                var message_ret = db.Messages.FirstOrDefault();
                var user1_ret = db.Users.FirstOrDefault();
                var user2_ret = db.Users.OrderByDescending(u => u.Id).FirstOrDefault();
                var convers_ret = db.Conversations.FirstOrDefault();
                var attach_ret = db.Attachments.FirstOrDefault();
                var file_ret = db.FileEntities.FirstOrDefault();
                Assert.That(message_ret, Is.Null);
                Assert.That(attach_ret, Is.Null);
                Assert.That(file_ret, Is.Not.Null);
                Assert.That(convers_ret, Is.Not.Null);
                Assert.That(user1_ret, Is.Not.Null);
                Assert.That(user2_ret, Is.Not.Null);
            }
        } 
    }
}
