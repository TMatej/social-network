﻿using DataAccessLayer;
using DataAccessLayer.Entity;
using Newtonsoft.Json;
using System.Configuration;

var connectionString = ConfigurationManager.AppSettings["ConnectionString"];

if (connectionString != null)
{
    using (var db = new SocialNetworkDBContext(connectionString))
    {
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();

        var user = new User
        {
            Username = "lokomotiva123",
            PrimaryEmail = "cokoloko@gmail.com",
            PasswordHash = "0123456789abcde0"
        };

        db.Users.Add(user);

        
        db.SaveChanges();

        db.Profiles.Add(
            new Profile
            {
                CreatedAt = DateTime.Now,
                UserId = user.Id,
                Address = new Address 
                { 
                    State = "Slovakia"
                }
            });

        db.SaveChanges();
        var db_user = db.Users.Where(u => u.Username.Equals(user.Username)).FirstOrDefault();
        var db_profile = db.Profiles.Where(p => p.UserId == user.Id).FirstOrDefault();

        Console.WriteLine(JsonConvert.SerializeObject(db_user, Formatting.Indented));
        Console.WriteLine(JsonConvert.SerializeObject(db_profile, Formatting.Indented));
    }
}

