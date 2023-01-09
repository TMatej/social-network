using Autofac;
using Microsoft.AspNetCore.Authentication.Cookies;
using Autofac.Extensions.DependencyInjection;
using BusinessLayer;
using DataAccessLayer;
using Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);
builder.Configuration.AddEnvironmentVariables();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
  .ConfigureContainer<ContainerBuilder>(containerBuilder =>
  {
      containerBuilder.RegisterModule(new EFCoreModule());
      containerBuilder.RegisterModule(new DALModule 
      { 
          ConnectionString = builder.Configuration.GetConnectionString("mssql"),
          SeedData = builder.Configuration.GetSection("Properties").GetValue<bool>("SeedData")
      });
      containerBuilder.RegisterModule(new ServicesModule());
      containerBuilder.RegisterModule(new FacadesModule());
  });
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
  .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
    options => { options.Cookie.Name = "auth"; options.Cookie.SameSite = SameSiteMode.None; options.Cookie.SecurePolicy = CookieSecurePolicy.Always; builder.Configuration.Bind("CookieSettings", options); });
/*builder.Services.AddHttpsRedirection(options =>
{
    options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
    options.HttpsPort = 5001;
});*/

var app = builder.Build();

var seeding = app.Configuration
    .GetSection("Properties")
    .GetValue<bool>("SeedData");
// this should be used only in DEV mode. PROD mode should use sql scripts for db creating and population.
using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<SocialNetworkDBContext>();
    if (seeding) context.Database.EnsureDeleted();
    //context.Database.EnsureCreated();
    context.Database.Migrate();
    if (seeding) context.Seed();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection(); // use this if HTTPS is required (in cluster we use HTTPS on in ingress)
app.UseStaticFiles();
app.UseRouting();
app.UseCors(builder => {
  builder
    .SetIsOriginAllowedToAllowWildcardSubdomains()
    .WithOrigins("http://localhost:3000", "https://*.vercel.app")
    .AllowCredentials()
    .AllowAnyHeader()
    .AllowAnyMethod();
  });
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
