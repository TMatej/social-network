﻿using Autofac;
using BusinessLayer.Services;
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
            builder.RegisterType<UserService>().InstancePerLifetimeScope();
            Container = builder.Build();
        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }
}
