using Autofac;
using Microsoft.Extensions.Configuration;
using MonsterAir.Core.Contexts;
using MonsterAir.Core.Repositories;
using MonsterAir.Core.Services;
using MonsterAir.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterAir.Core
{
    public class CoreModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;
        private readonly IConfiguration _configuration;

        public CoreModule(IConfiguration configuration, string connectionStringName, string migrationAssemblyName)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString(connectionStringName);
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AirContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                   .InstancePerLifetimeScope();

            builder.RegisterType<AirContext>().As<IAirContext>()
                     .WithParameter("connectionString", _connectionString)
                     .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                     .InstancePerLifetimeScope();

            builder.RegisterType<FlightUnitOfWork>().As<IFlightUnitOfWork>()
                  .WithParameter("connectionString", _connectionString)
                  .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                  .InstancePerLifetimeScope();

            builder.RegisterType<FlightRepository>().As<IFlightRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<FlightService>().As<IFlightService>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
