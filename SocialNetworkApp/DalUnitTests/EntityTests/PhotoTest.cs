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
    public class PhotoTest
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

                db.Galeries.Add(new Gallery
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
                db.Photos.Add(new Photo
                {
                    Title = "My first photo",
                    Description = "This is my first photo",
                    Url = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png",
                    GaleryId = 1
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
            using (var db = new SocialNetworkDBContext())
            {
                db.Photos.Add(new Photo
                {
                    Title = "My first photo",
                    Description = "This is my first photo",
                    GaleryId = 1
                });
                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }
        [Test]
        public void Test_Add_Long()
        {
            using (var db = new SocialNetworkDBContext())
            {
                db.Photos.Add(new Photo
                {
                    Title = new String('l', 500),
                    Description = new String('l', 500),
                    GaleryId = 1
                });
                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }
    }
}
