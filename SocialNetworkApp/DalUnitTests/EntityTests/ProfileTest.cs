using DataAccessLayer.Entity;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace DalUnitTests.EntityTests
{
    public class ProfileTest
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

                db.Users.Add(new User
                {
                    Name = "john",
                    Email = "john@gmail.com",
                    PasswordHash = "51df6545ecvd"
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
                db.Profiles.Add(new Profile
                {
                    UserId = 1,
                });
                db.SaveChanges();

                var profile = db.Profiles.FirstOrDefault();
                Assert.That(profile, Is.Not.Null);
                Assert.That(profile.Id, Is.EqualTo(1));
                Assert.That(profile.UserId, Is.EqualTo(1));
            }
        }

        [Test]
        public void Test_Add_Duplicate()
        {
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                db.Profiles.Add(new Profile
                {
                    UserId = 3,
                });
                db.Profiles.Add(new Profile
                {
                    UserId = 3,
                });

                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }
        [Test]
        public void Test_Add_Incomplete()
        {
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                db.Profiles.Add(new Profile { });
                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }
        [Test]
        public void Test_Add_Long()
        {
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                db.Profiles.Add(new Profile
                {
                    Name = new String('l', 100),
                    UserId = 1,
                });

                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }
    }
}

