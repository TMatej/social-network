using Autofac;
using BusinessLayer.Services;
using DalConsoleApp;
using DataAccessLayer;
using DataAccessLayer.Entity;
using Newtonsoft.Json;

using var ioc = new Bootstrapper();
using var scope = ioc.Container.BeginLifetimeScope();
var userService = scope.Resolve<UserService>();
using (var db = scope.Resolve<SocialNetworkDBContext>())
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

    await userService.Register(new BusinessLayer.DTOs.RegisterDTO
    {
        Username = "serviceUser",
        Email = "service@gmail.com",
        Password = "skdfjbe33n2l",
    });


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

    db.Users.ToList().ForEach(user => Console.WriteLine(user.Username));
}

