using Autofac;
using BusinessLayer;
using Infrastructure.EFCore;

namespace DalConsoleApp
{
    internal class Bootstrapper : IDisposable
    {
        public IContainer Container { get; private set; }
        public Bootstrapper()
        {
            var builder = new ContainerBuilder();
            builder.RegisterEFCore();
            builder.RegisterBusinessLayer();
            Container = builder.Build();
        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }
}
