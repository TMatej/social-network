using DataAccessLayer.Entity;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalUnitTests.EntityTests
{
    public class PostTest
    {
        private const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=PV179-SocialNetworkDB";

        [SetUp]
        public void Setup()
        {
            using (var db = new SocialNetworkDBContext(connectionString))
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
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
                db.Posts.Add(new Post
                {
                    UserId = 1,
                    PostableId = 1,
                    Title = "Hello World!",
                    Content = "This is my first post!",
                    CreatedDate = DateTime.Now
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
            using (var db = new SocialNetworkDBContext(connectionString))
            {
                db.Posts.Add(new Post
                {
                    UserId = 1,
                    Title = "Hello World!",
                    Content = "This is my first post!",
                    CreatedDate = DateTime.Now
                });
                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }
        [Test]
        public void Test_Add_Long()
        {
            using (var db = new SocialNetworkDBContext(connectionString))
            {
                db.Posts.Add(new Post
                {
                    UserId = 1,
                    Title = new String('l', 500),
                    Content = new String('l', 500),
                    CreatedDate = DateTime.Now
                });

                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }
    }
    
}
