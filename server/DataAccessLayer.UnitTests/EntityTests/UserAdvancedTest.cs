using DataAccessLayer.Entity;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Entity.JoinEntity;

namespace DalUnitTests.EntityTests
{
    public class UserAdvancedTest
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

        /*[Test]
        public void Delete_User1_Of_Contact_Test()
        {
            // Arrange
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                db.Contacts.Add(new Contact
                {
                    User1Id = 1,
                    User2Id = 2
                });
            }

            // Act
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                db.Users.Remove(new User { Id = 1 }); // using stub = only one call to db
                db.SaveChanges();
                // Assert
                 Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }

            // Assert - should not work
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                var contact_ret = db.Contacts.FirstOrDefault();
                var user1_ret = db.Users.First();
                var user2_ret = db.Users.OrderByDescending(u => u.Id).First();
                Assert.That(contact_ret, Is.Null);
                Assert.That(user1_ret.Id, Is.EqualTo(2));
                Assert.That(user2_ret.Id, Is.EqualTo(user1_ret.Id));
            }
        }*/

        [Test]
        public void Delete_User2_Of_Contact_Test()
        {
            // Arrange
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                db.Contacts.Add(new Contact
                {
                    User1Id = 1,
                    User2Id = 2
                });
            }

            // Act
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                db.Users.Remove(new User { Id = 2 }); // using stub = only one call to db
                db.SaveChanges();
            }

            // Assert
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                var contact_ret = db.Contacts.FirstOrDefault();
                var user1_ret = db.Users.First();
                var user2_ret = db.Users.OrderByDescending(u => u.Id).First();
                Assert.That(contact_ret, Is.Null);
                Assert.That(user1_ret.Id, Is.EqualTo(1));
                Assert.That(user2_ret.Id, Is.EqualTo(user1_ret.Id));
            }
        }

        [Test]
        public void Delete_Author_Of_Message_Test()
        {
            // Arrange
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                db.Conversations.Add(new Conversation
                {
                    UserId = 1 // author of message must be also the owner of conversation (each owner is responsible for handling its entities)
                });
                db.SaveChanges();

                db.Messages.Add(new Message
                {
                    Content = "Hello World!",
                    AuthorId = 1,
                    ReceiverId = 2,
                    //ConversationId = 1
                });
                db.SaveChanges();
            }

            
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                // Act
                db.Users.Remove(new User { Id = 1 });

                //Assert 
                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }

        [Test]
        public void Delete_Receiver_Of_Message_Test()
        {
            // Arrange
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                db.Conversations.Add(new Conversation
                {
                    UserId = 1 // author of message must be also the owner of conversation (each owner is responsible for handling its entities)
                });
                db.SaveChanges();

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
                db.Users.Remove(new User { Id = 2 });
                db.SaveChanges();

                //Assert 
                var message_ret = db.Messages.FirstOrDefault();
                var user1_ret = db.Users.First();
                var user2_ret = db.Users.OrderByDescending(u => u.Id).First();
                Assert.That(message_ret, Is.Null);
                Assert.That(user2_ret.Id, Is.EqualTo(user1_ret.Id));
            }
        }


    }
}
