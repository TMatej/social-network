using Autofac;
using Autofac.Core;
using BusinessLayer;
using BusinessLayer.Contracts;
using BusinessLayer.Services;
using DataAccessLayer;
using Infrastructure.EFCore;

namespace DalConsoleApp
{
    internal class Bootstrapper : IDisposable
    {
        public IContainer Container { get; private set; }
        public Bootstrapper()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new EFCoreModule());
            builder.RegisterModule(new DALModule());
            builder.RegisterModule(new ServicesModule());
            Container = builder.Build();
        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }
}
