using DataAccessLayer;
using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace DalUnitTests.EntityTests
{
    public class AttachmentTest
    {
        [SetUp]
        public void Setup()
        {
            using (var db = new SocialNetworkDBContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                db.Messages.Add(
                new Message
                {
                    Content = "Hello World!",
                    ConversationId = 1,
                    AuthorId = 1,
                });

                db.SaveChanges();
            }
        }

        [TearDown]
        public void TearDown()
        {
            using (var db = new SocialNetworkDBContext())
            {
                db.Database.EnsureDeleted();
                db.Dispose();
            }
        }
        [Test]
        public void Test_Add()
        {
            using (var db = new SocialNetworkDBContext())
            {
                db.Attachments.Add(new Attachment
                {
                    Url = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png",
                    MessageId = 3
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
            using (var db = new SocialNetworkDBContext())
            {
                db.Attachments.Add(new Attachment
                {
                    Url = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png",
                });
                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }
    }
}
