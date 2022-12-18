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
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                db.Users.Add(new User
                {
                    Name = "ben",
                    Email = "ben@gmail.com",
                    PasswordHash = "aaafht3x"
                });

                db.Groups.Add(new Group
                {
                    Name = "Test Group",
                    Description = "This is a test group."
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
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
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
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
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
