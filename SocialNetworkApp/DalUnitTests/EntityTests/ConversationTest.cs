using DataAccessLayer;
using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace DalUnitTests.EntityTests
{
    internal class ConversationTest
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
                db.Conversations.Add(new Conversation
                {
                    UserId = 1
                });
                db.SaveChanges();

                var conversation = db.Conversations.FirstOrDefault();
                Assert.That(conversation, Is.Not.Null);
                Assert.That(conversation.Id, Is.EqualTo(1));
            }
        }

        [Test]
        public void Test_Add_Incomplete()
        {
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                db.Conversations.Add(new Conversation { });
                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }
    }
}
