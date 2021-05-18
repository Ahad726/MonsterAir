using Microsoft.EntityFrameworkCore;
using MonsterAir.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterAir.Core.Contexts
{
    public class AirContext : DbContext , IAirContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public AirContext(string connectionString, string migrationAssemblyName)
        {
            this._connectionString = connectionString;
            this._migrationAssemblyName = migrationAssemblyName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString, m => m.MigrationsAssembly(_migrationAssemblyName));
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Flight> Flights { get; set; }
    }
}
