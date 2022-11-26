using DataAccessLayer;
using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace DalUnitTests.EntityTests
{
    internal class CommentTest
    {
        private const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=PV179-SocialNetworkDB";

        [SetUp]
        public void Setup()
        {
            using (var db = new SocialNetworkDBContext(connectionString))
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                db.SaveChanges();
            }
        }

        [TearDown]
        public void TearDown()
        {
            using (var db = new SocialNetworkDBContext(connectionString))
            {
                db.Database.EnsureDeleted();
                db.Dispose();
            }
        }
        [Test]
        public void Test_Add()
        {
            using (var db = new SocialNetworkDBContext(connectionString))
            {
                db.Comments.Add(new Comment
                {
                    Content = "This is an example comment",
                    CommentableId = 2,
                    UserId = 1,
                    CreatedAt = DateTime.Now
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
            using (var db = new SocialNetworkDBContext(connectionString))
            {
                db.Comments.Add(new Comment
                {
                    Content = "This is an example comment",
                    CreatedAt = DateTime.Now
                }); Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }

        [Test]
        public void Test_Add_Long()
        {
            using (var db = new SocialNetworkDBContext(connectionString))
            {
                db.Comments.Add(new Comment
                {
                    Content = new String('l', 1000),
                    CommentableId = 2,
                    UserId = 1,
                    CreatedAt = DateTime.Now
                });
                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }
    }
}
