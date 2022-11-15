using Autofac;
using DataAccessLayer;
using Infrastructure.EFCore.Query;
using Infrastructure.EFCore.Repository;
using Infrastructure.EFCore.UnitOfWork;
using Infrastructure.Query;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EFCore
{
    public static class EFCoreModule
    {
        public static ContainerBuilder RegisterEFCore(this ContainerBuilder containerBuilder)
        {
            var connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            containerBuilder.Register((ctx) => new SocialNetworkDBContext(connectionString));
            containerBuilder.RegisterType<EFUnitOfWork>().InstancePerLifetimeScope().As<IUnitOfWork>();
            containerBuilder.RegisterGeneric(typeof(EFGenericRepository<>)).As(typeof(IRepository<>));
            containerBuilder.RegisterGeneric(typeof(EntityFrameworkQuery<>)).As(typeof(IQuery<>));
            return containerBuilder;
        }
    }
}
