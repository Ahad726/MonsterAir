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
    public interface IFlightUnitOfWork : IUnitOfWork<AirContext>
    {
        IFlightRepository FlightRepository { get; set; }
    }
}
