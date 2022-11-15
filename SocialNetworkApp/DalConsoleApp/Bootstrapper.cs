using Autofac;
using Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalConsoleApp
{
    internal class Bootstrapper :IDisposable
    {
        public IContainer Container { get; private set; }
        public Bootstrapper()
        {
            var builder = new ContainerBuilder();
            builder.RegisterEFCore();
            Container = builder.Build();
        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }
}
