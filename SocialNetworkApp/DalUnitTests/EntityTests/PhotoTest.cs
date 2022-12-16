using DataAccessLayer.Entity;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace DalUnitTests.EntityTests
{
    public class PhotoTest
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

                db.Galleries.Add(new Gallery
                {
                    Title = "Test Gallery",
                    Description = "This is a test gallery.",
                    ProfileId = 1
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
                db.Photos.Add(new Photo
                {
                    Title = "My first photo",
                    Description = "This is my first photo",
                    FileEntity = new FileEntity
                    {
                        Id = 99,
                        Guid = Guid.NewGuid(),
                        Name = "Photo name",
                        Data = new byte[] {},
                    },
                    GalleryId = 1
                });
                db.SaveChanges();

                var photo = db.Photos.FirstOrDefault();
                Assert.That(photo, Is.Not.Null);
                Assert.That(photo.Id, Is.EqualTo(1));
                Assert.That(photo.Title, Is.EqualTo("My first photo"));
            }
        }

        [Test]
        public void Test_Add_Incomplete()
        {
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                db.Photos.Add(new Photo
                {
                    Title = "My first photo",
                    Description = "This is my first photo",
                    GalleryId = 1
                });
                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }
        [Test]
        public void Test_Add_Long()
        {
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                db.Photos.Add(new Photo
                {
                    Title = new String('l', 500),
                    Description = new String('l', 500),
                    GalleryId = 1
                });
                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }
    }
}
