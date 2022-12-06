using Autofac;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Autofac.Extensions.DependencyInjection;
using BusinessLayer;
using DataAccessLayer;
using Infrastructure.EFCore;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
  .ConfigureContainer<ContainerBuilder>(builder =>
  {
      builder.RegisterModule(new EFCoreModule());
      builder.RegisterModule(new DALModule());
      builder.RegisterModule(new ServicesModule());
      builder.RegisterModule(new FacadesModule());
  });
builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
    options => builder.Configuration.Bind("JwtSettings", options))
  .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
    options => builder.Configuration.Bind("CookieSettings", options));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
