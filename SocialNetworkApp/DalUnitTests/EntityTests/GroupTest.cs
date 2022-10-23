using DataAccessLayer;
using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalUnitTests.EntityTests
{
    public class GroupTest
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
                db.Groups.Add(new Group
                {
                    Name = "Example Group",
                    Description = "This is an example group",
                    CreatedAt = DateTime.Now
                });
                db.SaveChanges();

                var group = db.Groups.FirstOrDefault();
                Assert.That(group, Is.Not.Null);
                Assert.That(group.Id, Is.EqualTo(2));
            }
        }

        [Test]
        public void Test_Add_Incomplete()
        {
            using (var db = new SocialNetworkDBContext(connectionString))
            {
                db.Groups.Add(new Group
                {
                    Description = "This is an example group",
                    CreatedAt = DateTime.Now
                });
                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }
        [Test]
        public void Test_Add_Long()
        {
            using (var db = new SocialNetworkDBContext(connectionString))
            {

                db.Groups.Add(new Group
                {
                    Name = new String('l', 500),
                    Description = new String('l', 500),
                    CreatedAt = DateTime.Now
                });
                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }
    }
}
