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
                db.Photos.Add(new Photo
                {
                    Title = "My first photo",
                    Description = "This is my first photo",
                    CreatedAt = DateTime.Now,
                    Url = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png",
                    GaleryId = 1
                });
                db.SaveChanges();

                var photo = db.Profiles.FirstOrDefault();
                Assert.That(photo, Is.Not.Null);
                Assert.That(photo.Id, Is.EqualTo(1));
            }
        }

        [Test]
        public void Test_Add_Incomplete()
        {
            using (var db = new SocialNetworkDBContext(connectionString))
            {
                db.Photos.Add(new Photo
                {
                    Title = "My first photo",
                    Description = "This is my first photo",
                    CreatedAt = DateTime.Now,
                    GaleryId = 1
                });
                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }
        [Test]
        public void Test_Add_Long()
        {
            using (var db = new SocialNetworkDBContext(connectionString))
            {
                db.Photos.Add(new Photo
                {
                    Title = new String('l', 500),
                    Description = new String('l', 500),
                    CreatedAt = DateTime.Now,
                    GaleryId = 1
                });
                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }
    }
}
