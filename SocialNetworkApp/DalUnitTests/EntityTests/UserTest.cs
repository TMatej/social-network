using DataAccessLayer;
using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace DalUnitTests.EntityTests
{
    public class UserTest
    {
        [SetUp]
        public void Setup()
        {
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
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
                db.Users.Add(new User { Username = "lokomotiva123", Email = "cokoloko@gmail.com", PasswordHash = "0123456789abcde0" });
                db.SaveChanges();

                var user = db.Users.FirstOrDefault();
                Assert.That(user, Is.Not.Null);
                Assert.That(user.Id, Is.EqualTo(1));
            }
        }
        [Test]
        public void Test_Add_Incomplete()
        {
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                db.Users.Add(new User { Username = "lokomotiva123" });

                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }
        [Test]
        public void Test_Add_Duplicate()
        {
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                db.Users.Add(new User { Username = "lokomotiva123", Email = "cokoloko@gmail.com", PasswordHash = "0123456789abcde0" });

                db.Users.Add(new User { Username = "lokomotiva123", Email = "cokolokojine@gmail.com", PasswordHash = "ffffffffffff" });

                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }
        [Test]
        public void Test_Add_Long()
        {
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                db.Users.Add(new User { Username = new String('l', 100), Email = new String('l', 500), PasswordHash = "0" });


                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }
    }
}
