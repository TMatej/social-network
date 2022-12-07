using DataAccessLayer;
using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace DalUnitTests.EntityTests
{
    public class GroupTest
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
                db.Groups.Add(new Group
                {
                    Name = "Example Group",
                    Description = "This is an example group",
                });
                db.SaveChanges();

                var group = db.Groups.OrderBy(x => x.Id).LastOrDefault();
                Assert.That(group, Is.Not.Null);
                Assert.That(group.Id, Is.EqualTo(5)); /* id's 3 and 4 have groups from initialization */
            }
        }

        [Test]
        public void Test_Add_Incomplete()
        {
            using (var db = new SocialNetworkDBContext())
            {
                db.Groups.Add(new Group
                {
                    Description = "This is an example group",
                });
                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }
        [Test]
        public void Test_Add_Long()
        {
            using (var db = new SocialNetworkDBContext())
            {

                db.Groups.Add(new Group
                {
                    Name = new String('l', 500),
                    Description = new String('l', 500),
                });
                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }
    }
}
