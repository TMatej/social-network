using Autofac;
using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs.Photo;
using BusinessLayer.DTOs.User;
using DalConsoleApp;
using DataAccessLayer;
using DataAccessLayer.Entity;
using Infrastructure.Query;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Profile = DataAccessLayer.Entity.Profile;

using var ioc = new Bootstrapper();
using var scope = ioc.Container.BeginLifetimeScope();

var userService = scope.Resolve<IUserService>();
var galleryService = scope.Resolve<IGalleryService>();
var mapper = scope.Resolve<IMapper>();
var serializerSettings = new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };

var user = new User
{
    Username = "lokomotiva123",
    PrimaryEmail = "cokoloko@gmail.com",
    PasswordHash = "0123456789abcde0"
};

using (var db = scope.Resolve<SocialNetworkDBContext>())
{
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();

    db.Users.Add(user);
    db.SaveChanges();
}

var profile = new Profile
{
    CreatedAt = DateTime.Now,
    UserId = user.Id,
    Name = "JOŽO VAJDA",
    Address = new Address
    {
        State = "Slovakia"
    }
};

using (var db_1 = scope.Resolve<SocialNetworkDBContext>())
{
    db_1.Profiles.Add(profile);
    db_1.SaveChanges();

    var photo = db_1.Photos.Find(3);
    db_1.Galeries.Add(
        new Gallery
        {
            Title = "My christmas gallery",
            Description = "This is my fav gallery of my christmas experience!",
            CreatedAt = DateTime.Now,
            ProfileId = 1,
            Photos = new List<Photo> { photo }
        });
    db_1.SaveChanges();

    var db_user = db_1.Users.Where(u => u.Username.Equals(user.Username)).FirstOrDefault();
    var db_profile = db_1.Profiles.Where(p => p.UserId == user.Id).FirstOrDefault();
    var db_gallery = db_1.Galeries.Where(g => g.ProfileId == profile.Id).FirstOrDefault();
    Console.WriteLine("db_user:");
    Console.WriteLine(JsonConvert.SerializeObject(db_user, Formatting.Indented, serializerSettings));
    /*
    {
      "Id": 3,
      "Username": "lokomotiva123",
      "PasswordHash": "0123456789abcde0",
      "PrimaryEmail": "cokoloko@gmail.com",
      "SecondaryEmail": null,
      "EventParticipants": null,
      "ConversationParticipants": null,
      "GroupMembers": null,
      "Contacts": null,
      "ContactsOf": null
    }*/
    Console.WriteLine();
    Console.WriteLine("db_profile:");
    Console.WriteLine(JsonConvert.SerializeObject(db_profile, Formatting.Indented, serializerSettings));
    /*
     {
      "Name": "JOŽO VAJDA",
      "Address": {
        "State": "Slovakia",
        "Region": null,
        "City": null,
        "Street": null,
        "HouseNumber": null,
        "PostalCode": null
      },
      "Sex": null,
      "PhoneNumber": null,
      "DateOfBirth": null,
      "CreatedAt": "2022-11-17T03:04:24.2354258+01:00",
      "UserId": 3,
      "User": {
        "Id": 3,
        "Username": "lokomotiva123",
        "PasswordHash": "0123456789abcde0",
        "PrimaryEmail": "cokoloko@gmail.com",
        "SecondaryEmail": null,
        "EventParticipants": null,
        "ConversationParticipants": null,
        "GroupMembers": null,
        "Contacts": null,
        "ContactsOf": null
      },
      "Id": 5,
      "Posts": null
    }*/
    Console.WriteLine();
    Console.WriteLine("db_gallery:");
    Console.WriteLine(JsonConvert.SerializeObject(db_gallery, Formatting.Indented, serializerSettings));
    /*
    {
      "Id": 2,
      "Title": "My christmas gallery",
      "Description": "This is my fav gallery of my christmas experience!",
      "CreatedAt": "2022-11-17T03:04:25.1602373+01:00",
      "Profile": {
        "Name": "JOŽO VAJDA",
        "Address": {
          "State": "Slovakia",
          "Region": null,
          "City": null,
          "Street": null,
          "HouseNumber": null,
          "PostalCode": null
        },
        "Sex": null,
        "PhoneNumber": null,
        "DateOfBirth": null,
        "CreatedAt": "2022-11-17T03:04:24.2354258+01:00",
        "UserId": 3,
        "User": {
          "Id": 3,
          "Username": "lokomotiva123",
          "PasswordHash": "0123456789abcde0",
          "PrimaryEmail": "cokoloko@gmail.com",
          "SecondaryEmail": null,
          "EventParticipants": null,
          "ConversationParticipants": null,
          "GroupMembers": null,
          "Contacts": null,
          "ContactsOf": null
        },
        "Id": 5,
        "Posts": null
      },
      "ProfileId": 5
    }*/
}

await userService.Register(new RegisterDTO
{
    Username = "serviceUser",
    Email = "service@gmail.com",
    Password = "skdfjbe33n2l",
});

var query = scope.Resolve<IQuery<User>>();
using (var db_2 = scope.Resolve<SocialNetworkDBContext>())
{
    db_2.Users.ToList().ForEach(user => Console.WriteLine(user.Username));
    var query_user = query
        .Where<string>(a => a == "serviceUser", "Username")
        .Execute();
    Console.WriteLine(JsonConvert.SerializeObject(query_user, Formatting.Indented));
}

var galleryDB = galleryService.GetByID(1);
Console.WriteLine("Galery:");
/*
    {
      "Title": "My christmas gallery",
      "Description": "This is my fav gallery of my christmas experience!",
      "CreatedAt": "2022-11-17T03:04:25.1602373",
      "ProfileId": 5,
      "Profile": null,
      "ProfileName": null,
      "UserName": null,
      "UserId": 0,
      "PhotosCount": 0
    }*/
Console.WriteLine(JsonConvert.SerializeObject(galleryDB, Formatting.Indented));

var profilePhoto = new PhotoInsertDTO
{
    Title = "My profile photo",
    Description = "This is my first profile photo!",
    CreatedAt = DateTime.Now,
    Url = "somewhere on the internet"
};

galleryService.UploadPhotoToGallery(profilePhoto, galleryDB.Id);

using (var db_3 = scope.Resolve<SocialNetworkDBContext>())
{
    var advancedGallery = db_3.Galeries
        .Include("Profile")
        .Where(g => g.Id == 1);
    Console.WriteLine(JsonConvert.SerializeObject(advancedGallery, Formatting.Indented, serializerSettings));
}

Console.WriteLine("Gallery service: Get By id with list of photos:");
var galleryWithPhotos = galleryService.GetByIdWithListOfPhotos(1);
Console.WriteLine(JsonConvert.SerializeObject(galleryWithPhotos, Formatting.Indented, serializerSettings));