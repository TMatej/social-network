using Autofac;
using Microsoft.AspNetCore.Authentication.Cookies;
using Autofac.Extensions.DependencyInjection;
using BusinessLayer;
using DataAccessLayer;
using Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

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
          ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection"),
          SeedData = builder.Configuration.GetSection("Properties").GetValue<bool>("SeedData")
      });
      containerBuilder.RegisterModule(new ServicesModule());
      containerBuilder.RegisterModule(new FacadesModule());
  });
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
  .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
    options => { options.Cookie.Name = "auth"; options.Cookie.SameSite = SameSiteMode.None; options.Cookie.SecurePolicy = CookieSecurePolicy.Always; builder.Configuration.Bind("CookieSettings", options); });

var app = builder.Build();

/* NOT VERY SAFE - probably find better way */
using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetService<SocialNetworkDBContext>();
    context.Database.Migrate();
}
    

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();
app.UseCors(builder => {
  builder.WithOrigins("http://localhost:5173", "https://social-network-ksnxjqxm9-ciza99.vercel.app", "http://social-network-ksnxjqxm9-ciza99.vercel.app")
    .AllowCredentials()
    .AllowAnyHeader()
    .AllowAnyMethod();
  });
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
