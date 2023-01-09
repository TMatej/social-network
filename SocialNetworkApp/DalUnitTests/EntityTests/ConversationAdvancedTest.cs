using DataAccessLayer.Entity;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalUnitTests.EntityTests
{
    public class ConversationAdvancedTest
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
        public void Delete_Conversation_Of_Message_Test()
        {
            // Arrange
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                db.Messages.Add(new Message
                {
                    Content = "Hello World!",
                    AuthorId = 1,
                    ReceiverId = 2,
                    ConversationId = 1
                });
                db.SaveChanges();
            }

            // Act
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                var conversation = db.Conversations.First();
                db.Conversations.Remove(conversation);
                db.SaveChanges();

                //Assert 
                var message_ret = db.Messages.FirstOrDefault();
                var user1_ret = db.Users.FirstOrDefault();
                conversation = db.Conversations.FirstOrDefault();
                var user2_ret = db.Users.OrderByDescending(u => u.Id).FirstOrDefault();
                Assert.That(message_ret, Is.Null);
                Assert.That(conversation, Is.Null);
                Assert.That(user1_ret, Is.Not.Null);
                Assert.That(user2_ret, Is.Not.Null);
            }
        }
    }
}
