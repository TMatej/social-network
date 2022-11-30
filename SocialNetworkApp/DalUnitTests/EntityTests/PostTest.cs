using DataAccessLayer;
using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace DalUnitTests.EntityTests
{
    public class PostTest
    {
        [SetUp]
        public void Setup()
        {
            using (var db = new SocialNetworkDBContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
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
                db.Posts.Add(new Post
                {
                    UserId = 1,
                    PostableId = 1,
                    Title = "Hello World!",
                    Content = "This is my first post!",
                });
                db.SaveChanges();

                var post = db.Posts.FirstOrDefault();
                Assert.That(post, Is.Not.Null);
                Assert.That(post.Title, Is.EqualTo("Hello World!"));
            }
        }

        [Test]
        public void Test_Add_Incomplete()
        {
            using (var db = new SocialNetworkDBContext())
            {
                db.Posts.Add(new Post
                {
                    UserId = 1,
                    Title = "Hello World!",
                    Content = "This is my first post!",
                });
                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }
        [Test]
        public void Test_Add_Long()
        {
            using (var db = new SocialNetworkDBContext())
            {
                db.Posts.Add(new Post
                {
                    UserId = 1,
                    Title = new String('l', 500),
                    Content = new String('l', 500),
                });

                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }
    }

}
