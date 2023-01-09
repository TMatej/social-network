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

                db.Profiles.Add(new Profile
                {
                    UserId = 1,
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
        public void Test_AddComment_ToPhoto()
        {
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                db.Galleries.Add(new Gallery
                {
                    Title = "Test Gallery",
                    Description = "This is a test gallery.",
                    ProfileId = 1
                });
                db.SaveChanges();

                db.Photos.Add(new Photo
                {
                    Title = "My first photo",
                    Description = "This is my first photo",
                    FileEntity = new FileEntity
                    {
                        //Id = 99,
                        Guid = Guid.NewGuid(),
                        Name = "Photo name",
                        Data = new byte[] {},
                        FileType = "image/jpg",
                    },
                    GalleryId = 1
                });
                db.SaveChanges();

                db.Comments.Add(new Comment
                {
                    Content = "This is an example comment",
                    CommentableId = 1,
                    UserId = 1,
                });
                db.SaveChanges();

                var comment = db.Comments.FirstOrDefault();
                Assert.That(comment, Is.Not.Null);
                Assert.That(comment.Id, Is.EqualTo(2));
                Assert.That(comment.UserId, Is.EqualTo(1));
            }
        }

        [Test]
        public void Test_Add_Incomplete()
        {
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
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
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
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
