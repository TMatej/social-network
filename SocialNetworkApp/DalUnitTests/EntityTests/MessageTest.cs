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

                db.Conversations.Add(new Conversation
                {
                    UserId = 1
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
                    ConversationId = 1,
                    AuthorId = 1,
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
    }
}
