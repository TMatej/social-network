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
    public class EventTest
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

                db.Groups.Add(new Group
                {
                    Name = "Example Group",
                    Description = "This is an example group",
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
                db.Events.Add(new Event
                {
                    UserId = 1,
                    GroupId = 1,
                    Title = "Example Event",
                    Description = "This is an example event",
                });
                db.SaveChanges();

                var _event = db.Events.FirstOrDefault();
                Assert.That(_event, Is.Not.Null);
                Assert.That(_event.Id, Is.EqualTo(1));
            }
        }

        [Test]
        public void Test_Add_Incomplete()
        {
            using (var db = new SocialNetworkDBContext())
            {
                db.Events.Add(new Event
                {
                    Title = "Example Event",
                    Description = "This is an example event",
                });
                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }
        [Test]
        public void Test_Add_Long()
        {
            using (var db = new SocialNetworkDBContext())
            {
                db.Events.Add(new Event
                {
                    UserId = 1,
                    GroupId = 2,
                    Title = new String('l', 1000),
                    Description = new String('l', 1000),
                });
                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }
    }
}
