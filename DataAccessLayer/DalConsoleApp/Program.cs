using DataAccessLayer;
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

        db.Users.Add(new User { Username="lokomotiva123", PrimaryEmail="cokoloko@gmail.com", PasswordHash="0123456789abcde0"});
        db.SaveChanges();

        var user = db.Users.FirstOrDefault();

        Console.WriteLine(JsonConvert.SerializeObject(user, Formatting.Indented));     
    }
}

