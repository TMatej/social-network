using DataAccessLayer;
using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace DalUnitTests.EntityTests
{
    public class GaleryTest
    {
        [SetUp]
        public void Setup()
        {
            using (var db = new SocialNetworkDBContext())
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
                db.Galleries.Add(new Gallery
                {
                    Title = "Example Galery",
                    Description = "This is an example galery",
                    ProfileId = 1
                });
                db.SaveChanges();

                var galery = db.Galleries.FirstOrDefault();
                Assert.That(galery, Is.Not.Null);
                Assert.That(galery.Id, Is.EqualTo(1));
            }
        }

        [Test]
        public void Test_Add_Incomplete()
        {
            using (var db = new SocialNetworkDBContext())
            {
                db.Galleries.Add(new Gallery
                {
                    Description = "This is an example galery",
                });
                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }
        [Test]
        public void Test_Add_Long()
        {
            using (var db = new SocialNetworkDBContext())
            {
                db.Galleries.Add(new Gallery
                {
                    Title = new String('l', 500),
                    Description = new String('l', 5000),
                    ProfileId = 1
                });
                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }
    }
}
