using Autofac;
using Autofac.Extensions.DependencyInjection;
using BusinessLayer;
using DataAccessLayer;
using Infrastructure.EFCore;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
  .ConfigureContainer<ContainerBuilder>(builder => {
    builder.RegisterModule(new EFCoreModule());
    builder.RegisterModule(new DALModule());
    builder.RegisterModule(new ServicesModule());
  });
builder.Services.AddControllers();

var app = builder.Build();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
