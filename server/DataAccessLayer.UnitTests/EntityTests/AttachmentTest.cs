﻿using DataAccessLayer;
using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace DalUnitTests.EntityTests
{
    public class AttachmentTest
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
                    UserId = 1
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
                db.Attachments.Add(new Attachment
                {
                    FileEntity = new FileEntity
                    {
                        Guid = Guid.NewGuid(),
                        Name = "Photo name",
                        Data = new byte[] {},
                        FileType = "image/jpg",
                    },
                    MessageId = 1
                });
                db.SaveChanges();

                var attachment = db.Attachments.FirstOrDefault();
                Assert.That(attachment, Is.Not.Null);
                Assert.That(attachment.Id, Is.EqualTo(1));
            }
        }

        [Test]
        public void Test_Add_Incomplete()
        {
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                db.Attachments.Add(new Attachment
                {
                    FileEntity = new FileEntity
                    {
                        Guid = Guid.NewGuid(),
                        Name = "Photo name",
                        Data = new byte[] {},
                        FileType = "image/jpg",
                    },
                });
                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }
    }
}
