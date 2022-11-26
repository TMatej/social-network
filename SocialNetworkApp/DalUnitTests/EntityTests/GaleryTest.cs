﻿using DataAccessLayer;
using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalUnitTests.EntityTests
{
    public class GaleryTest
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
                db.Galleries.Add(new Gallery
                {
                    Title = "Example Galery",
                    Description = "This is an example galery",
                    CreatedAt = DateTime.Now,
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
            using (var db = new SocialNetworkDBContext(connectionString))
            {
                db.Galleries.Add(new Gallery
                {
                    Description = "This is an example galery",
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
                db.Galleries.Add(new Gallery
                {
                    Title = new String('l', 500),
                    Description = new String('l', 5000),
                    CreatedAt = DateTime.Now,
                    ProfileId = 1
                });
                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }
    }
}
