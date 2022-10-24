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
    public class ProfileTest
    {
        private const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=PV179-SocialNetworkDB";

        [SetUp]
        public void Setup()
        {
            using (var db = new SocialNetworkDBContext(connectionString))
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                db.Users.Add(new User
                {
                    Username = "ben",
                    PrimaryEmail = "ben@gmail.com",
                    PasswordHash = "aaafht3x"
                });

                db.Users.Add(new User
                {
                    Username = "john",
                    PrimaryEmail = "john@gmail.com",
                    PasswordHash = "51df6545ecvd"
                });
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
                db.Profiles.Add(new Profile
                {
                    CreatedAt = DateTime.Now,
                    UserId = 2,
                });
                db.SaveChanges();

                var profile = db.Profiles.FirstOrDefault();
                Assert.That(profile, Is.Not.Null);
                Assert.That(profile.Id, Is.EqualTo(1));
            }
        }

        [Test]
        public void Test_Add_Duplicate()
        {
            using (var db = new SocialNetworkDBContext(connectionString))
            {
                db.Profiles.Add(new Profile
                {
                    CreatedAt = DateTime.Now,
                    UserId = 3,
                });
                db.Profiles.Add(new Profile
                {
                    CreatedAt = DateTime.Now,
                    UserId = 3,
                });

                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }
        [Test]
        public void Test_Add_Incomplete()
        {
            using (var db = new SocialNetworkDBContext(connectionString))
            {
                db.Profiles.Add(new Profile
                {
                    CreatedAt = DateTime.Now,
                });
                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }
        [Test]
        public void Test_Add_Long()
        {
            using (var db = new SocialNetworkDBContext(connectionString))
            {
                db.Profiles.Add(new Profile
                {
                    Name = new String('l', 100),
                    CreatedAt = DateTime.Now,
                    UserId = 1,
                });

                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }
    }
}

