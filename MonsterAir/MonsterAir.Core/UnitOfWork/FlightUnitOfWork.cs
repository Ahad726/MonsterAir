using MonsterAir.Core.Contexts;
using MonsterAir.Core.Repositories;
using MonsterAir.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterAir.Core.UnitOfWork
{
    public class FlightUnitOfWork : UnitOfWork<AirContext>, IFlightUnitOfWork
    {
        public IFlightRepository FlightRepository { get; set; }
        public FlightUnitOfWork(string connectionString, string migrationAssemblyName)
            : base(connectionString, migrationAssemblyName)
        {
            FlightRepository = new FlightRepository(_dbContext);

        }

    }
}
