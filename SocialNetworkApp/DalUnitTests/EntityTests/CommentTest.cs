using DataAccessLayer;
using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace DalUnitTests.EntityTests
{
    internal class CommentTest
    {
        [SetUp]
        public void Setup()
        {
            using (var db = new SocialNetworkDBContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
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
                db.Comments.Add(new Comment
                {
                    Content = "This is an example comment",
                    CommentableId = 2,
                    UserId = 1,
                });
                db.SaveChanges();

                var comment = db.Comments.FirstOrDefault();
                Assert.That(comment, Is.Not.Null);
                Assert.That(comment.Id, Is.EqualTo(3));
            }
        }

        [Test]
        public void Test_Add_Incomplete()
        {
            using (var db = new SocialNetworkDBContext())
            {
                db.Comments.Add(new Comment
                {
                    Content = "This is an example comment",
                }); Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }

        [Test]
        public void Test_Add_Long()
        {
            using (var db = new SocialNetworkDBContext())
            {
                db.Comments.Add(new Comment
                {
                    Content = new String('l', 1000),
                    CommentableId = 2,
                    UserId = 1,
                });
                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }
    }
}
